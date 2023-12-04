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

namespace Stock_Dashboard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void PlotBtn_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Plot Button is clicked");
 
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            TickerNameTextBox.Clear();
            PricePlot.Plot.Clear();
            VolumePlot.Plot.Clear();

            MessageBox.Show("Clear Button is clicked.");

            Console.WriteLine("Clear Button is clicked");
        }
    }
}
