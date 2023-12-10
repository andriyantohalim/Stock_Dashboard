using ScottPlot;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
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
            //OHLC price = new OHLC(
            //    open: 100,
            //    high: 120,
            //    low: 80,
            //    close: 105,
            //    timeStart: new DateTime(1985, 09, 24),
            //    timeSpan: TimeSpan.FromDays(1));

            OHLC[] prices = DataGen.RandomStockPrices(new Random(0), 60);

            PricePlot.Plot.AddCandlesticks(prices);
            //PricePlot.Plot.XAxis.DateTimeFormat(true);

            PricePlot.Refresh();

            double[] volumes = DataGen.RandomNormal(5000, 60);
            VolumePlot.Plot.AddBar(volumes);
            //VolumePlot.Plot.XAxis.DateTimeFormat(true);

            VolumePlot.Refresh();
        }

        private async void PlotBtn_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Plot Button is clicked");

            Ticker ticker = new Ticker();

            // from SYNC method
            //ticker.getTickerPrices("AAPL", "1700784000", "1701561600", "1d");

            // from ASYNC method
            String tickerPrices = await ticker.GetTickerPrices("AAPL", "1700784000", "1701561600", "1d");
            Console.WriteLine(tickerPrices);

            // Parse String tickerPrices

            // Convert tickerPrices to OHLC

            // Pass OHLC tickerPrices to GenerateChart() function

            // 1700784000 --> 24 Nov 2023
            // 1701561600 --> 03 Dec 2023
            // Use https://www.epochconverter.com/ for conversion

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

    public class Ticker
    {
        //public async void getTickerPrices(String tickerName, String startDate, String stopDate, String interval)
        //{
        //    String baseURL = "https://query1.finance.yahoo.com/v7/finance/download/";
        //    String requestURI = baseURL
        //        + tickerName
        //        + "?period1=" + startDate
        //        + "&period2=" + stopDate
        //        + "&interval=" + interval
        //        + "&events=" + "history"
        //        + "&includeAdjustedClose=" + "true";

        //    Console.WriteLine(requestURI);

        //    var client = new HttpClient();
        //    var request = new HttpRequestMessage(HttpMethod.Get, requestURI);

        //    try
        //    {
        //        var response = await client.SendAsync(request);
        //        Console.WriteLine(response.StatusCode.ToString());

        //        String myTickerPrices = await response.Content.ReadAsStringAsync();
        //        Console.WriteLine(myTickerPrices);

        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //    }
        //}

        public async Task<String> GetTickerPrices(String tickerName, String startDate, String stopDate, String interval)
        {
            String baseURL = "https://query1.finance.yahoo.com/v7/finance/download/";
            String requestURI = baseURL
                + tickerName
                + "?period1=" + startDate
                + "&period2=" + stopDate
                + "&interval=" + interval
                + "&events=" + "history"
                + "&includeAdjustedClose=" + "true";

            //Console.WriteLine(requestURI);

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, requestURI);

            try
            {
                var response = await client.SendAsync(request);
                //Console.WriteLine(response.StatusCode.ToString());

                String myTickerPrices = await response.Content.ReadAsStringAsync();
                //Console.WriteLine(myTickerPrices);
                return myTickerPrices;

            }
            catch (Exception e)
            {
                //Console.WriteLine(e.Message);
                return e.Message;
            }
        }
    }
}
