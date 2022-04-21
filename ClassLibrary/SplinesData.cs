using System.Linq;
using System.Runtime.InteropServices;

namespace ClassLibrary
{
    public class SplinesData
    {
        // Properties
        public SplineParameters SplParams { get; set; }
        public MeasuredData Measured { get; set; }
        public double[] CubicSpline { get; set; }
        public double[] Integrals { get; set; } = new double[2];
        public double[] Derivatieves { get; set; } = new double[4];

        // Constructor
        public SplinesData(MeasuredData md, SplineParameters sp)
        {
            Measured = md;
            SplParams = sp;
        }

        // MKL functions
        [DllImport("..\\..\\..\\..\\x64\\Debug\\MKL_DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern double InterpolateMKL(int length1, int length2, double[] points, double[] func, double[] res);
        [DllImport("..\\..\\..\\..\\x64\\Debug\\MKL_DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern double IntegrateMKL(int length, double[] points, double[] func, double[] limits, double[] integrals);

        // Interpolate
        public double Interpolate()
        {
            // Array for resault
            double[] InterpRes = new double[3 * SplParams.UniformLength];

            // Interpolate
            double status = InterpolateMKL(Measured.Length, SplParams.UniformLength, Measured.Grid, Measured.Measures, InterpRes);

            // Check for errors
            if (status == 0)
            {
                // Get nessasary values
                double[] resault = new double[SplParams.UniformLength];
                for (int i = 0; i < SplParams.UniformLength; i++)
                {
                    resault[i] = InterpRes[0 + (3 * i)];
                }
                CubicSpline = resault;

                // Derivatieves
                Derivatieves[0] = InterpRes[1];
                Derivatieves[1] = InterpRes[(3 * SplParams.UniformLength) - 2];
                Derivatieves[2] = InterpRes[2];
                Derivatieves[3] = InterpRes[(3 * SplParams.UniformLength) - 1];

                return 0;
            }
            else
            {
                return status;
            }
        }

        // Integrate
        public double Integrate()
        {
            double[] Integral = new double[Measured.Length];

            // First one
            double status = IntegrateMKL(Measured.Length, Measured.Grid, Measured.Measures, new double[] { SplParams.x1, SplParams.x2 }, Integral);

            // Check for errors
            if (status == 0)
            {
                // Get value for whole segment
                Integrals[0] = Integral.Sum();

                // Second one
                status = IntegrateMKL(Measured.Length, Measured.Grid, Measured.Measures, new double[] { SplParams.x2, SplParams.x3 }, Integral);

                // Check for errors
                if (status == 0)
                {
                    // Get value for whole segment
                    Integrals[1] = Integral.Sum();

                    return 0;
                }
                else
                {
                    return status;
                }
            }
            else
            {
                return status;
            }
        }
    }
}
