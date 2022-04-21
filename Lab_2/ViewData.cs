using System;
using System.ComponentModel;
using ClassLibrary;

namespace Lab_2
{
    public class ViewData
    {
        // Properties
        public InputParams InputData { get; set; }
        public SplinesData SpData { get; set; }
        public ChartData Graphics { get; set; }

        // Constructor
        public ViewData()
        {
            InputData = new(20, 20 * 10, 0, 20, SPf.Cubic, 1, 10.5, 15.8);
            InputData.Error1 = false;
            InputData.Error2 = false;

            SpData = new(new(InputData), new(InputData));

            Graphics = new(SpData.Measured.Grid);
        }

        // Updated data in subclasses
        public void UpdateData()
        {
            SpData.Measured.Updater(InputData);
            SpData.SplParams.Updater(InputData);
        }

        // Call functions from subclasses
        public double Interpolate()
        {
            return SpData.Interpolate();
        }

        public double Integrate()
        {
            return SpData.Integrate();
        }
    }
}
