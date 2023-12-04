using ScottPlot;
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

        private void GenerateChart() 
        {
            OHLC price = new OHLC(
                open: 100,
                high: 120,
                low: 80,
                close: 105,
                timeStart: new DateTime(1985, 09, 24),
                timeSpan: TimeSpan.FromDays(1));

            OHLC[] prices = DataGen.RandomStockPrices(new Random(0), 60);

            PricePlot.Plot.AddCandlesticks(prices);
            //PricePlot.Plot.XAxis.DateTimeFormat(true);

            PricePlot.Refresh();

            double[] volumes = DataGen.RandomNormal(5000, 60);
            VolumePlot.Plot.AddBar(volumes);
            //VolumePlot.Plot.XAxis.DateTimeFormat(true);

            VolumePlot.Refresh();
        }

        private void PlotBtn_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Plot Button is clicked");

            GenerateChart();

        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            TickerNameTextBox.Clear();

            PricePlot.Plot.Clear();
            VolumePlot.Plot.Clear();

            PricePlot.Refresh();
            VolumePlot.Refresh();

            //MessageBox.Show("Clear Button is clicked.");

            Console.WriteLine("Clear Button is clicked");
        }
    }
}
