namespace LipometryAppAPI.Models
{
    public class Person
    {
        public int PersonId { get; init; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateOnly DateOfBirth { get; init; }
        public double? WeightInKg { get; set; } = 0.0;
        public double? HeightInCm { get; set; } = 0.0;
        public double? WaistInCm { get; set; } = 0.0;
        public double? HipInCm { get; set; } = 0.0;
        public double? NeckInCm { get; set; } = 0.0;
        public int Age
        {
            get
            {
                var today = DateOnly.FromDateTime(DateTime.Today);
                var age = today.Year - DateOfBirth.Year;
                if (DateOfBirth > today.AddYears(-age))
                    age--;
                return age;
            }
        }
        public PersonGender PersonGender { get; init; } = PersonGender.Male;

    }
}
