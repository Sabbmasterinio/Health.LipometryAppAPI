using System.Text.Json.Serialization;

namespace LipometryAppAPI.Models
{
    public class PersonBodyDetails
    {
        public int Id { get; set; }
        public double? WaistInCm { get; set; } = 0.0;
        public double? HipInCm { get; set; } = 0.0;
        public double? NeckInCm { get; set; } = 0.0;
        [JsonIgnore]
        public Person? Person { get; set; }
    }
}
