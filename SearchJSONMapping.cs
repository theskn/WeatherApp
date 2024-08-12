using Newtonsoft.Json;

namespace WeatherApp
{
    public class SearchRoot
    {
        public List<SearchResult> Results { get; set; }
        public double GenerationTimeMs { get; set; }
    }

    public class SearchResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Elevation { get; set; }
        public string FeatureCode { get; set; }
        public string CountryCode { get; set; }
        public int Admin1Id { get; set; }
        public int? Admin2Id { get; set; }  // Nullable because not all results have admin2_id
        public int? Admin3Id { get; set; }  // Nullable because not all results have admin3_id
        public string Timezone { get; set; }
        public int Population { get; set; }
        public List<string> Postcodes { get; set; }
        public int CountryId { get; set; }
        public string Country { get; set; }
        public string Admin1 { get; set; }
        public string Admin2 { get; set; }
        public string Admin3 { get; set; }
    }
}