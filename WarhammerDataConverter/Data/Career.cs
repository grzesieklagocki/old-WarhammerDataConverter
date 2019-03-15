using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarhammerDataConverter.Data
{
    public class Career
    {
        public string Name { get; set; }
        public bool IsAdvanced { get; set; }
        public string SourceBook { get; set; }

        public Stats Stats { get; set; }

        //public Group[] Skills { get; set; }
        //public Group[] Abilities { get; set; }
        //public Group[] Inventory { get; set; }
        public string Skills { get; set; }
        public string Talents { get; set; }
        public string Trippings { get; set; }
        public string[] CareerEntries { get; set; }
        public string[] CareerExits { get; set; }

        public string Description { get; set; }
        public string Note { get; set; }

        public string[] Races { get; set; }
    }

    public class Group
    {
        public int SelectionCount { get; set; }
        public string[] Items { get; set; }
    }
}