using System;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Defaults;

namespace Lab_2
{
    public class ChartData
    {
        // Properties
        public SeriesCollection SeriesCollection { get; set; }
        public Func<double, string> Formatter { get; set; }

        // Constructor
        public ChartData(double[] inLables)
        {
            SeriesCollection = new SeriesCollection();

            // Values Formatter
            Formatter = value => value.ToString("F4");
        }

        // Add plot series
        public void AddSeries(double[] points, double[] values, string title, int mode)
        {
            // Custom array
            ChartValues<ObservablePoint> Values = new ChartValues<ObservablePoint>();
            for (int i = 0; i < values.Length; i++)
            {
                Values.Add(new(points[i], values[i]));
            }

            // Measured data
            if(mode == 0)
            {
                SeriesCollection.Add(new ScatterSeries
                {
                    Title = title,
                    Values = Values,
                    Fill = Brushes.Red,
                    MinPointShapeDiameter = 5,
                    MaxPointShapeDiameter = 5
                });
            }
            // Splines
            else if(mode == 1)
            {
                SeriesCollection.Add(new LineSeries
                {
                    Title = title,
                    Values = Values,
                    Fill = Brushes.Transparent,
                    Stroke = Brushes.Green,
                    PointGeometry = null, // Line without markers
                    LineSmoothness = 0
                });
            }
        }

        // Clear plot
        public void ClearCollection()
        {
            SeriesCollection.Clear();
        }
    }
}
