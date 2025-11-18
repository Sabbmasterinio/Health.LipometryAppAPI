namespace LipometryAppAPI.Models
{
    public class Person
    {
        public int PersonId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateOnly DateOfBirth { get; set; }
        public double? WeightInKg { get; set; } = 0.0;
        public double? HeightInCm { get; set; } = 0.0;
        public PersonBodyDetails? BodyDetails { get; set; }
        public int? BodyDetailsId { get; set; }
        //public PersonGender PersonGender { get; init; } = PersonGender.Male;
        //public PersonBodyDetails BodyDetails { get; set; }

    }
}
