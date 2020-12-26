using System;
using System.Collections.Generic;
using System.Text;

namespace PS5Status.JsonClasses
{
    public class CityData
    {
        public DigitalInfo digital_info { get; set; }
        public string digital_link { get; set; }
        public string name { get; set; }
        public NormalInfo normal_info { get; set; }
        public string normal_link { get; set; }
        public int order { get; set; }
        public string pic { get; set; }
    }
}
