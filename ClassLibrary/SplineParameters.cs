using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class SplineParameters
    {
        // Properties
        public int UniformLength { get; set; }
        public double x1 { get; set; }
        public double x2 { get; set; }
        public double x3 { get; set; }

        // Constructor
        public SplineParameters(InputParams input)
        {
            UniformLength = input.UniformLength;
            x1 = input.x1;
            x2 = input.x2;
            x3 = input.x3;
        }

        // Constructor like thing that modifies class entity
        public void Updater(InputParams input)
        {
            UniformLength = input.UniformLength;
            x1 = input.x1;
            x2 = input.x2;
            x3 = input.x3;
        }
    }
}
