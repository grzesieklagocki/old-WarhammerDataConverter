namespace WarhammerDataConverter.Data
{
    public class Stats
    {
        public MainStats Main { get; set; }
        public SecondaryStats Secondary { get; set; }

        public class MainStats
        {
            public int WS { get; set; }
            public int BS { get; set; }
            public int S { get; set; }
            public int T { get; set; }
            public int Ag { get; set; }
            public int Int { get; set; }
            public int WP { get; set; }
            public int Fel { get; set; }
        }

        public class SecondaryStats
        {
            public int A { get; set; }
            public int W { get; set; }
            public int SB { get; set; }
            public int TB { get; set; }
            public int M { get; set; }
            public int Mag { get; set; }
            public int IP { get; set; }
            public int FP { get; set; }
        }
    }
}
