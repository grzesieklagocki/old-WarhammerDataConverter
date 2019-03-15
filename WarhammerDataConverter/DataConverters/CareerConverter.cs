using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarhammerDataConverter.Data;

namespace WarhammerDataConverter.DataConverters
{
    public static class CareerConverter
    {
        public static Career[] GetCareers(string[] lines, char columnSeparator = '\t')
        {
            return DataConverter.Convert(lines, columnSeparator, s => CreateCareer(s));
        }

        private enum CareerColumns
        {
            Name, IsAdvanced, WhExtension, WS, BS, S, T, Ag, Int, WP, Fel, A, W, SB, TB, M, Mag, IP, FP, Skills, Talents, Trippings, CareerEntries, CareerExits, Description, Note, Races
        }

        private static Career CreateCareer(string[] parameters)
        {
            Career career = new Career()
            {
                Name = parameters[(int)CareerColumns.Name],
                IsAdvanced = (parameters[(int)CareerColumns.IsAdvanced] == "A"),
                SourceBook = parameters[(int)CareerColumns.WhExtension],
                Stats = CreateStats(parameters),
                CareerEntries = Split(parameters[(int)CareerColumns.CareerEntries], ','),
                CareerExits = Split(parameters[(int)CareerColumns.CareerExits], ','),
                Description = parameters[(int)CareerColumns.Description],
                Note = parameters[(int)CareerColumns.Note],
                Races = Split(parameters[(int)CareerColumns.Races], ',')
            };

            return career;
        }

        private static Stats CreateStats(string[] parameters)
        {
            Stats stats = new Stats()
            {
                Main = new Stats.MainStats()
                {
                    WS = (int.TryParse(parameters[(int)CareerColumns.WS], out int number)) ? number : 0,
                    BS = (int.TryParse(parameters[(int)CareerColumns.BS], out number)) ? number : 0,
                    S = (int.TryParse(parameters[(int)CareerColumns.S], out number)) ? number : 0,
                    T = (int.TryParse(parameters[(int)CareerColumns.T], out number)) ? number : 0,
                    Ag = (int.TryParse(parameters[(int)CareerColumns.Ag], out number)) ? number : 0,
                    Int = (int.TryParse(parameters[(int)CareerColumns.Int], out number)) ? number : 0,
                    WP = (int.TryParse(parameters[(int)CareerColumns.WP], out number)) ? number : 0,
                    Fel = (int.TryParse(parameters[(int)CareerColumns.Fel], out number)) ? number : 0
                },
                Secondary = new Stats.SecondaryStats()
                {
                    A = (int.TryParse(parameters[(int)CareerColumns.A], out number)) ? number : 0,
                    W = (int.TryParse(parameters[(int)CareerColumns.W], out number)) ? number : 0,
                    SB = (int.TryParse(parameters[(int)CareerColumns.SB], out number)) ? number : 0,
                    TB = (int.TryParse(parameters[(int)CareerColumns.TB], out number)) ? number : 0,
                    M = (int.TryParse(parameters[(int)CareerColumns.M], out number)) ? number : 0,
                    Mag = (int.TryParse(parameters[(int)CareerColumns.Mag], out number)) ? number : 0,
                    IP = (int.TryParse(parameters[(int)CareerColumns.IP], out number)) ? number : 0,
                    FP = (int.TryParse(parameters[(int)CareerColumns.FP], out number)) ? number : 0
                }
            };

            return stats;
        }

        private static string[] Split(string text, char separator)
        {
            string[] items = text.Split(separator);

            for (int i = 0; i < items.Length; i++)
            {
                items[i] = items[i].Trim();
            }

            return items;
        }
    }
}
