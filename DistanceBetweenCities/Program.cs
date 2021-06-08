using System;
using System.Collections.Generic;
using System.IO;

namespace DistanceBetweenCities
{
    class Program
    {
        private static List<City> Cities { get; } = new List<City>();
        static void Main()
        {
            for(int i = 0; i < 5; i++)
            {
                Generate();
            }
            FillTxt();
        }
        static void FillTxt()
        {
            foreach(var cityProps in Cities)
            {
                File.AppendAllText("rng.txt", cityProps.CityNameFirst + ";" + cityProps.CityNameSecond + ";" + cityProps.Distance + "\n");
            }
        }
        static void Generate()
        {
            Random generator = new Random();
            City Obj = new City();
            for(int i = 0; i < 5; i++)
            {
                Obj.CityNameFirst += (char)generator.Next(65, 90);
                Obj.CityNameSecond += (char)generator.Next(65, 90);
            }
            Obj.Distance = generator.Next(0, 9999);
            Cities.Add(Obj);
        }
    }
    class City
    {
        public string CityNameFirst { get; set; } = "";
        public string CityNameSecond { get; set; } = "";
        public int Distance { get; set; }
    }
}
