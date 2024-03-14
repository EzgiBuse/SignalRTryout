namespace SignalRTryout
{
    public static class SD
    {
        static SD()
        {
            HallowRace = new Dictionary<string,int>();
            HallowRace.Add(Cloak, 0);
            HallowRace.Add(Stone, 0);
            HallowRace.Add(Wand, 0);
        }
        public const string Wand = "wand";
        public const string Stone = "stone";
        public const string Cloak = "cloak";

        public static Dictionary<string,int> HallowRace { get; set; }
    }
}
