using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
using Newtonsoft.Json;

namespace MeinWetterApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string apikey = "91b0c31e59fdb92f32767f54da5c92f9";

        private readonly string requestUrl = "https://api.openweathermap.org/data/2.5/weather";  //? q ={city name}&appid={API key}
        public MainWindow()
        {
            InitializeComponent();

            SetData("Barcelona");

         
        }
        public WeatherMapResponse GetWeatherData(string city)
        {
            // Es wird eine Instanz der HttpClient-Klasse erstellt, um eine HTTP-Anfrage durchzuführen.
            HttpClient httpClient = new HttpClient();

            // Die Variable "finalUri" enthält die URL für die Wetterabfrage,
            // die aus der Basis-URL, dem Stadtnamen, dem API-Schlüssel und der gewünschten Einheit besteht.

            var finalUri = requestUrl + "?q=" + city + "&appid=" + apikey + "&units=metric";
            HttpResponseMessage httpResponse = httpClient.GetAsync(finalUri).Result;

            string response = httpResponse.Content.ReadAsStringAsync().Result;

            WeatherMapResponse weatherMapResponse = JsonConvert.DeserializeObject<WeatherMapResponse>(response);

            return weatherMapResponse;
        }
        

        public void SetData(string city)
        {

            WeatherMapResponse result = GetWeatherData(city);
            string finalImage = "Sun.png";
             
            string current = result.weather[0].main.ToLower(); // ToLower setzt das text auf klein Buchstuch
          
            if (current.Contains("cloud"))
            {
                finalImage = "Cloud.png";
            }
            else if (current.Contains("rain"))
            {
                finalImage = "Rain.png";
            }
            else if (current.Contains("snow"))
            {
                finalImage = "snow.png";
            }
            else
            {
                finalImage = "Sun.Png";

            }
            lbltemperature.Content = (result.main.temp).ToString("f1")+" °C";
            lblStadt.Content = result.name;
            lblstatus.Content = result.weather[0].main;
                backgorundImage.ImageSource = new BitmapImage(new Uri("images/" + finalImage, UriKind.Relative));
        }

        private void SearchCity(object sender, RoutedEventArgs e)
        {
            string query = txtSuche.Text;
            SetData(query);
        }
    }
}
