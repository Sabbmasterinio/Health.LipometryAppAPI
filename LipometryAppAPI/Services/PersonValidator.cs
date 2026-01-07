using LipometryAppAPI.Models;

namespace LipometryAppAPI.Services
{
    public class PersonValidator : Validator<Person>
    {
        public PersonValidator()
        {
            AddRule(p => !string.IsNullOrWhiteSpace(p.FirstName), "Firstname cannot be empty.");
            AddRule(p => !string.IsNullOrWhiteSpace(p.LastName), "Lastname cannot be empty.");
            AddRule(p => p.Age >= 4 && p.Age <= 120, "Age must be between 4 and 120.");
            AddRule(p => p.WeightInKg == null || p.WeightInKg >= 10 && p.WeightInKg <= 300, "Weight must be between 10 and 300kg.");
            AddRule(p => p.HeightInCm == null || p.HeightInCm >= 0.5 && p.HeightInCm <= 2.50, "Height must be between 5cm and 250cm.");
            AddRule(p => p.WaistInCm == null || p.WaistInCm >= 30 && p.WaistInCm <= 200, "Waist must be between 30cm and 200cm");
            AddRule(p => p.HipInCm == null || p.HipInCm>= 30 && p.HipInCm <= 200, "Hip must be between 30cm and 200cm");
            AddRule(p => p.NeckInCm == null || p.NeckInCm >= 25 && p.NeckInCm <= 50, "Neck must be between 25cm and 50cm");
        }
    }
}
