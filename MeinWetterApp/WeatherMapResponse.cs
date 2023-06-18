using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeinWetterApp
{
    // Hier wurde das JSON als klasse gemacht
   public class WeatherMapResponse

    {
        public Main main;
        public List<Weather> weather;
        public standort sys;
        public string name;
        // weather[0].description
    }
}
