using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarhammerDataConverter.Data;
using Newtonsoft.Json;
using WarhammerDataConverter.DataConverters;

namespace WarhammerDataConverter
{
    class Program
    {


        static void Main(string[] args)
        {
            string inputUrl = args[0];
            string outputUrl = args[1];

            var lines = File.ReadAllLines(inputUrl);

            Career[] careers = CareerConverter.GetCareers(lines);
            File.WriteAllText(outputUrl, JsonConvert.SerializeObject(careers));
            Console.WriteLine($"Liczba wczytanych profesji: {careers.Length}");

            var careerNames = GetAllCareers(careers);

            foreach (string career in careerNames)
            {
                Console.WriteLine(career);
            }
            Console.WriteLine(careerNames.Count());
            Console.ReadKey();
        }

        private static IEnumerable<string> GetAllCareers(IEnumerable<Career> careers)
        {
            var dictionary = new Dictionary<string, object>();
            foreach (var career in careers)
            {
                foreach (var car in career.CareerEntries)
                {
                    dictionary[car] = null;
                }

                foreach (var car in career.CareerExits)
                {
                    dictionary[car] = null;
                }
            }

            return dictionary.Keys.OrderBy(s => s).ToArray();
        }
    }
}
