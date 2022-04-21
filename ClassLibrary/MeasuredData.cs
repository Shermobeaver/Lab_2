using System;
using System.Collections.ObjectModel;

namespace ClassLibrary
{
    public class MeasuredData
    {
        // Properties
        public int Length { get; set; }
        public double Left { get; set; }
        public double Right { get; set; }
        public SPf Function { get; set; }
        public double[] Grid { get; set; }
        public double[] Measures { get; set; }
        public ObservableCollection<string> Collection { get; set; }

        // Constructor
        public MeasuredData(InputParams input)
        {
            Length = input.Length;
            Left = input.Left;
            Right = input.Right;
            Function = input.Function;

            Collection = new();
        }

        // Constructor like thing that modifies class entity
        public void Updater(InputParams input)
        {
            Length = input.Length;
            Left = input.Left;
            Right = input.Right;
            Function = input.Function;
        }

        // Init grid
        public void CreateGrid()
        {
            // Init array
            Grid = new double[Length];

            // Set right and left nodes
            Grid[0] = Left;
            Grid[Length - 1] = Right;

            // Random class
            var rand = new Random();

            // Set values
            for (int i = 1; i < Length - 1; i++)
            {
                double randval = Left; 
                while(randval <= Left)
                {
                    randval = Right * rand.NextDouble();
                }
                Grid[i] = randval;
            }

            // Sort values
            Array.Sort(Grid);
        }

        // Measure function values on grid
        public void Measure()
        {
            // Init array
            Measures = new double[Length];

            // Function choice
            if (Function == SPf.Linear)
            {
                for (int i = 0; i < Length; i++)
                {
                    Measures[i] = Grid[i];
                }
            }
            else if (Function == SPf.Cubic)
            {
                for (int i = 0; i < Length; i++)
                {
                    Measures[i] = Math.Pow(Grid[i], 3);
                }
            }
            else if (Function == SPf.Random)
            {
                var rand = new Random();
                for (int i = 0; i < Length; i++)
                {
                    Measures[i] = 20 * rand.NextDouble();
                }
            }

            // Fill Collection
            Collection.Clear();
            for (int i = 0; i < Length; i++)
            {
                Collection.Add($"Point: {Grid[i]}\nFunction Value: {Measures[i]}\n");
            }
        }
    }
}
