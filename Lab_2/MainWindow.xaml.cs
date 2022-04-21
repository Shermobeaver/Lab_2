using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClassLibrary;

namespace Lab_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        // # Properties
        public ViewData Data { get; set; }
        public bool IsMeasured { get; set; }
        public bool IsSplined { get; set; }

        // # MainWindow
        public MainWindow()
        {
            try
            {
                Data = new();

                DataContext = this;
            }
            catch (Exception error)
            {
                MessageBox.Show($"Unexpected error: {error.Message}.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            InitializeComponent();

            // Combobox
            Func.ItemsSource = Enum.GetValues(typeof(ClassLibrary.SPf));
        }

        // Command handling
        private void MeasuredData_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !Data.InputData.Error1;
        }
        private void MeasuredData_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                // Clear some text
                textBlock_Der_1rst.Text = "";
                textBlock_Der_2nd.Text = "";
                textBlock_Integ1.Text = "";
                textBlock_Integ2.Text = "";
                textBlock_Spl1.Text = "";
                textBlock_Spl2.Text = "";

                // Update data from InputParameters
                Data.UpdateData();

                // Set grid and measure values
                Data.SpData.Measured.CreateGrid();
                Data.SpData.Measured.Measure();

                // Set flag for Splines command
                IsMeasured = true;
                IsSplined = false;

                // Add to plot
                Data.Graphics.ClearCollection();
                Data.Graphics.AddSeries(Data.SpData.Measured.Grid, Data.SpData.Measured.Measures, "Measured", 0);
            }
            catch (Exception error)
            {
                MessageBox.Show($"Unexpected error: {error.Message}.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Splines_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (!Data.InputData.Error2) && IsMeasured && (!IsSplined);
        }

        private void Splines_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                // Set flag so we can'te reexecute
                IsSplined = true;

                // Interpolation
                double status = Data.Interpolate();

                if (status == 0)
                {
                    textBlock_Der_1rst.Text = $"1rst Der: {Data.SpData.Derivatieves[0]:0.00}; {Data.SpData.Derivatieves[1]:0.00}";
                    textBlock_Der_2nd.Text = $"2nd Der: {Data.SpData.Derivatieves[2]:0.00}; {Data.SpData.Derivatieves[3]:0.00}";
                    textBlock_Spl1.Text = $"Spline-Left: {Data.SpData.CubicSpline[0]:0.00}";
                    textBlock_Spl2.Text = $"Spline-Right: {Data.SpData.CubicSpline[Data.SpData.Measured.Length - 1]:0.00}";

                    // Uniform Grid
                    double[] GridUniform = new double[Data.SpData.SplParams.UniformLength];
                    double step = (Data.SpData.Measured.Right - Data.SpData.Measured.Left) / (Data.SpData.SplParams.UniformLength - 1);
                    for (int i = 0; i < Data.SpData.SplParams.UniformLength; i++)
                    {
                        GridUniform[i] = Data.SpData.Measured.Left + (i * step);
                    }

                    // Add to plot
                    Data.Graphics.AddSeries(GridUniform, Data.SpData.CubicSpline, "Spline", 1);

                    //Integrals
                    status = Data.Integrate();

                    if (status == 0)
                    {
                        textBlock_Integ1.Text = $"1rst Integral: {Data.SpData.Integrals[0]:0.00}";
                        textBlock_Integ2.Text = $"2nd Integral: {Data.SpData.Integrals[1]:0.00}";
                    }
                    else
                    {
                        MessageBox.Show($"Error in Integrate(): {status}.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show($"Error in Interpolate(): {status}.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show($"Unexpected error: {error.Message}.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    // Custom Commands
    public static class CustomCommands
    {
        public static readonly RoutedUICommand MeasuredData = new
            (
                "MeasuredData",
                "MeasuredData",
                typeof(CustomCommands),
                new InputGestureCollection()
                {
                    new KeyGesture(Key.D1, ModifierKeys.Control)
                }
            );

        public static readonly RoutedUICommand Splines = new
            (
                "Splines",
                "Splines",
                typeof(CustomCommands),
                new InputGestureCollection()
                {
                    new KeyGesture(Key.D2, ModifierKeys.Control)
                }
            );
    }
}
