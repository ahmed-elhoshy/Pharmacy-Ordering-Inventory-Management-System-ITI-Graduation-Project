namespace PharmacySystem.ApplicationLayer.DTOs.RepresentativeOrder
{
    public class RepresentativeOrderStatsDto
    {
        public int PharmacyCount { get; set; }
        public decimal Revenue { get; set; }
        public Dictionary<string, int> Stats { get; set; } = new();
    }
}
