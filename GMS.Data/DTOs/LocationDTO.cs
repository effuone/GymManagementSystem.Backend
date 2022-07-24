namespace GMS.Data.DTOs
{
    public class LocationDTO
    {
        public int LocationId { get; set; }
        public string CountryName { get; set; }
        public string CityName { get; set; }
    }
    public class CreateLocationDTO
    {
        public int LocationId { get; set; }
        public string CountryName { get; set; }
        public string CityName { get; set; }
    }
    public class UpdateLocationDTO
    {
        public int LocationId { get; set; }
        public string CountryName { get; set; }
        public string CityName { get; set; }
    }
}