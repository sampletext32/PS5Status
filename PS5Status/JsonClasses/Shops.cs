using System;
using System.Collections.Generic;
using PS5Status.JsonClasses;

namespace PS5Status
{
    public class Shops
    {
        public CityData dns { get; set; }
        public CityData eldorado { get; set; }
        public CityData gamepark { get; set; }
        public CityData mvideo { get; set; }
        public CityData onec { get; set; }
        public CityData ozon { get; set; }
        public CityData sony { get; set; }
        public CityData technopark { get; set; }
        public CityData videoigr { get; set; }
        public CityData wildberries { get; set; }

        public IEnumerable<CityData> All()
        {
            yield return dns;
            yield return eldorado;
            yield return gamepark;
            yield return mvideo;
            yield return onec;
            yield return ozon;
            yield return sony;
            yield return technopark;
            yield return videoigr;
            yield return wildberries;
        }
    }
}