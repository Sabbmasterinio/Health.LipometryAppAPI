using LipometryAppAPI.Contracts.Models;
using LipometryAppAPI.Services;

namespace LipometryAppAPI.Models
{
    /// <summary>
    /// Represents an individual including information about their specific discipline.
    /// </summary>
    /// <remarks>The <see cref="Person"/> class provides details about an individual, including attributes
    /// such as height and weight.</remarks>
    public class Person : IHasAttributes
    {
        #region Public Members
        /// <summary>
        /// The id for the person
        /// </summary>
        public Guid PersonId { get; init; }


        /// <summary>
        /// The first name of the person
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// The last name of the person
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// The full name of the person
        /// </summary>
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
        
        /// <summary>
        /// The phone number of the person
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// The email address of the person
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// The date of birth of the person
        /// </summary>
        public DateOnly DateOfBirth { get; init; }
        
        /// <summary>
        /// The weight in Kg of the person
        /// </summary>
        public double? WeightInKg { get; set; }

        /// <summary>
        /// The Height in cm of the person
        /// </summary>
        public double? HeightInCm { get; set; } 

        /// <summary>
        /// The waist perimeter in cm of the person
        /// </summary>
        public double? WaistInCm { get; set; }

        /// <summary>
        /// The hip perimeter in cm of the person
        /// </summary>
        public double? HipInCm { get; set; }

        /// <summary>
        /// The neck perimeter in cm of the person
        /// </summary>
        public double? NeckInCm { get; set; } 

        /// <summary>
        /// The age of the person
        /// </summary>
        public int Age
        {
            get
            {
                return Calculator.CalculateAgeFromDob(DateOfBirth);
            }
        }

        /// <summary>
        /// The gender of the person
        /// </summary>
        public PersonGender PersonGender { get; init; } = PersonGender.Male;

        public virtual ICollection<BodyMeasurement> BodyMeasurements { get; set; } = new List<BodyMeasurement>();        
        #endregion
    }
}
