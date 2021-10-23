namespace Aurora.Common.Domain.Locations
{
    public class CountryDivision
    {
        public short DivisionId { get; set; }
        public short CountryId { get; set; }
        public string Name { get; set; }
        public int LevelNumber { get; set; }
        public bool IsCityLevel { get; set; }
        public bool IsActive { get; set; }
    }
}