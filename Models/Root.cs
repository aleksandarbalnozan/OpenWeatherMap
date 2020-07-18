using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace WeatherWPF.Models
{
    class Root
    {
        public List<Weather> weather { get; set; }
        public string visibility { get; set; }
        public Main main { get; set; }
        public Wind wind { get; set; }
        public Sys sys { get; set; }
        public string name { get; set; }
        public Coord coord { get; set; }
    }
}
