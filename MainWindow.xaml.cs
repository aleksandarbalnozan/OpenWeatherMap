using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Text;
using System.Windows;
using WeatherWPF.Models;

namespace WeatherWPF
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

        //convert unix to datetime
        public static DateTime UnixTimeStampToDateTime(double sunRiseSet)
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(sunRiseSet).ToLocalTime();
            return dtDateTime;
        }

        private static string GetJson(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            try
            {
                WebResponse response = request.GetResponse();

                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    return reader.ReadToEnd();
                }
            }
            catch (WebException exc)
            {
                WebResponse response = exc.Response;

                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    return reader.ReadToEnd();
                }
                throw;
            }
        }


        private void ClearTextBoxs()
        {
            textBlockTemp.Text = "";
            textBlockMaxTemp.Text = "";
            textBlockMinTemp.Text = "";

            textBlockSunRise.Text = "";
            textBlockSunSet.Text = "";

            textBoxWeatehr.Clear();

        }


        private void textBoxLat_GotFocus(object sender, RoutedEventArgs e)
        {
            this.textBoxLat.Clear();
        }

        private void textBoxLon_GotFocus(object sender, RoutedEventArgs e)
        {
            this.textBoxLon.Clear();
        }

        private void textBoxCity_GotFocus(object sender, RoutedEventArgs e)
        {
            this.textBoxCity.Clear();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            ClearTextBoxs();
            string adress = "";
            if (textBoxCity.Text != "City")
            {
                adress = "http://api.openweathermap.org/data/2.5/weather?q=" + textBoxCity.Text + "&units=metric&appid={ YOUR API }";
                string result = GetJson(adress);
                var ms = JsonConvert.DeserializeObject<Root>(result);
                string sunrise = UnixTimeStampToDateTime(double.Parse(ms.sys.sunrise)).ToString();
                string sunset = UnixTimeStampToDateTime(double.Parse(ms.sys.sunset)).ToString();
                textBlockSunSet.Text = sunset;
                textBlockSunRise.Text = sunrise;
                textBlockMaxTemp.Text = ms.main.temp_max;
                textBlockMinTemp.Text = ms.main.temp_min;
                textBlockTemp.Text = ms.main.temp;
                textBoxWeatehr.AppendText(ms.weather[0].main + '\n');
                textBoxWeatehr.AppendText(ms.weather[0].description);
                
            }
            else if (textBoxLon.Text != "Lon" && textBoxLat.Text != "Lat")
            {
                adress = "http://api.openweathermap.org/data/2.5/weather?lat=" + textBoxLat.Text + "&lon=" + textBoxLon.Text + "&units=metric&appid={ YOUR API }";
                string result = GetJson(adress);
                var ms = JsonConvert.DeserializeObject<Root>(result);
                string sunrise = UnixTimeStampToDateTime(double.Parse(ms.sys.sunrise)).ToString();
                string sunset = UnixTimeStampToDateTime(double.Parse(ms.sys.sunset)).ToString();
                textBlockSunSet.Text = sunset;
                textBlockSunRise.Text = sunrise;
                textBlockMaxTemp.Text = ms.main.temp_max;
                textBlockMinTemp.Text = ms.main.temp_min;
                textBlockTemp.Text = ms.main.temp;
            }

            
        }
    }
}
