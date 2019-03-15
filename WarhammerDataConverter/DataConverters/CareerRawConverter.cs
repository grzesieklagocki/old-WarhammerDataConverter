using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarhammerDataConverter.Data;

namespace WarhammerDataConverter.DataConverters
{
    public static class CareerRawConverter
    {
        private readonly static string[] separators = new string[] { "Main Profile WS BS S T Ag Int WP Fel ", "Secondary Profile A W SB TB M Mag IP FP ", "Skills: ", "Talents: ", "Trappings: ", "Career Entries: ", "Career Exits: ", "Note:" };

        public static Career[] GetCareers(string[] lines, char columnSeparator = '\t')
        {
            return DataConverter.Convert(lines, columnSeparator, s => CreateCareer(s));
        }

        private enum CareerColumn
        {
            NamePL, Name, IsAdvanced, WhExtension, WS, BS, S, T, Ag, Int, WP, Fel, A, W, SB, TB, M, Mag, IP, FP, Skills, Talents, Trippings, CareerEntries, CareerExits, Description, Note, Races, RAW, RAWDescription
        }

        private enum CareerRawColumn
        {
            NamePL, Name, IsAdvanced, SourceBook, RAWData, Description, Quotation
        }

        private enum RawColumn
        {
            MainProfile, SecondaryProfile, Skills, Talents, Trappings, CareerEntries, CareerExits, Note
        }

        private enum MainStatsColumn
        {
            WS, BS, S, T, Ag, Int, WP, Fel
        }

        private enum SecondaryStatsColumn
        {
            A, W, SB, TB, M, Mag, IP, FP
        }

        private static Career CreateCareer(string[] columns)
        {
            var parameters = columns[(int)CareerRawColumn.RAWData].Split(separators, StringSplitOptions.RemoveEmptyEntries);
            if (parameters.Length != 8)
            {
                System.Diagnostics.Debug.WriteLine($"{columns[(int)CareerColumn.Name]}: {parameters.Last()}");
            }

            Career career = new Career()
            {
                Name = columns[(int)CareerRawColumn.Name],
                IsAdvanced = bool.Parse(columns[(int)CareerRawColumn.IsAdvanced]),
                SourceBook = columns[(int)CareerRawColumn.SourceBook],
                Stats = CreateStats(parameters[(int)RawColumn.MainProfile], parameters[(int)RawColumn.SecondaryProfile]),
                Skills = parameters[(int)RawColumn.Skills],
                Talents = parameters[(int)RawColumn.Talents],
                Trippings = parameters[(int)RawColumn.Trappings],
                CareerEntries = Split(parameters[(int)RawColumn.CareerEntries], ','),
                CareerExits = Split(parameters[(int)RawColumn.CareerExits], ','),
                Description = columns[(int)CareerRawColumn.Description].Replace("        ", " "),
                Note = (parameters.Length > (int)RawColumn.Note) ? parameters[(int)RawColumn.Note] : ""
                //Races = Split(columns[(int)CareerRawColumn.Races], ',')
            };

            return career;
        }

        private static Stats CreateStats(string main, string secondary)
        {
            var m = main.Replace("+", "").Replace("%", "").Split(' ');
            var s = secondary.Replace("+", "").Replace("%", "").Split(' ');

            Stats stats = new Stats()
            {
                Main = new Stats.MainStats()
                {
                    WS = (int.TryParse(m[(int)MainStatsColumn.WS], out int number)) ? number : 0,
                    BS = (int.TryParse(m[(int)MainStatsColumn.BS], out number)) ? number : 0,
                    S = (int.TryParse(m[(int)MainStatsColumn.S], out number)) ? number : 0,
                    T = (int.TryParse(m[(int)MainStatsColumn.T], out number)) ? number : 0,
                    Ag = (int.TryParse(m[(int)MainStatsColumn.Ag], out number)) ? number : 0,
                    Int = (int.TryParse(m[(int)MainStatsColumn.Int], out number)) ? number : 0,
                    WP = (int.TryParse(m[(int)MainStatsColumn.WP], out number)) ? number : 0,
                    Fel = (int.TryParse(m[(int)MainStatsColumn.Fel], out number)) ? number : 0
                },
                Secondary = new Stats.SecondaryStats()
                {
                    A = (int.TryParse(s[(int)SecondaryStatsColumn.A], out number)) ? number : 0,
                    W = (int.TryParse(s[(int)SecondaryStatsColumn.W], out number)) ? number : 0,
                    SB = (int.TryParse(s[(int)SecondaryStatsColumn.SB], out number)) ? number : 0,
                    TB = (int.TryParse(s[(int)SecondaryStatsColumn.TB], out number)) ? number : 0,
                    M = (int.TryParse(s[(int)SecondaryStatsColumn.M], out number)) ? number : 0,
                    Mag = (int.TryParse(s[(int)SecondaryStatsColumn.Mag], out number)) ? number : 0,
                    IP = (int.TryParse(s[(int)SecondaryStatsColumn.IP], out number)) ? number : 0,
                    FP = (int.TryParse(s[(int)SecondaryStatsColumn.FP], out number)) ? number : 0
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

        public static string FromCareersToTSV(Career[] careers)
        {
            string tsv = string.Empty;

            foreach (var career in careers)
            {
                tsv += FromCareerToTSV(career);
            }

            return tsv;
        }

        private static string FromCareerToTSV(Career career)
        {
            // NamePL, Name, IsAdvanced, WhExtension, WS, BS, S, T, Ag, Int, WP, Fel, A, W, SB, TB, M, Mag, IP, FP, Skills, Talents, Trippings, CareerEntries, CareerExits, Description, Note, Races, RAW, RAWDescription
            return "\t"
                + $"{career.Name}\t"
                + $"{career.IsAdvanced}\t"
                + $"{career.SourceBook}\t"
                + $"{career.Stats.Main.WS}\t"
                + $"{career.Stats.Main.BS}\t"
                + $"{career.Stats.Main.S}\t"
                + $"{career.Stats.Main.T}\t"
                + $"{career.Stats.Main.Ag}\t"
                + $"{career.Stats.Main.Int}\t"
                + $"{career.Stats.Main.WP}\t"
                + $"{career.Stats.Main.Fel}\t"
                + $"{career.Stats.Secondary.A}\t"
                + $"{career.Stats.Secondary.W}\t"
                + $"{career.Stats.Secondary.SB}\t"
                + $"{career.Stats.Secondary.TB}\t"
                + $"{career.Stats.Secondary.M}\t"
                + $"{career.Stats.Secondary.Mag}\t"
                + $"{career.Stats.Secondary.IP}\t"
                + $"{career.Stats.Secondary.FP}\t"
                + $"{career.Skills}\t"
                + $"{career.Talents}\t"
                + $"{career.Trippings}\t"
                + $"{string.Join(", ", career.CareerEntries)}\t"
                + $"{string.Join(", ", career.CareerExits)}\t"
                + $"{career.Description}\t"
                + $"{career.Note}"
                + Environment.NewLine;
        }
    }
}
