using LipometryAppAPI.Contracts.Models;

namespace LipometryAppAPI.Contracts.Responses
{
    public class PersonReadResponse
    {
        /// <summary>
        /// The id for the person read DTO
        /// </summary>
        public Guid PersonId { get; set; }

        /// <summary>
        /// The first name of the person read DTO
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// The last name of the person read DTO
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// The date of birth of the person read DTO
        /// </summary>
        public DateOnly DateOfBirth { get; set; }

        /// <summary>
        /// The age of the person read DTO
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// The weight in Kg of the person read DTO
        /// </summary>
        public double? WeightInKg { get; set; }

        /// <summary>
        /// The height in cm of the person read DTO
        /// </summary>
        public double? HeightInCm { get; set; }

        /// <summary>
        /// The waist perimeter in cm of the person read DTO
        /// </summary>
        public double? WaistInCm { get; set; }

        /// <summary>
        /// The hip perimeter in cm of the person read DTO
        /// </summary>
        public double? HipInCm { get; set; }

        /// <summary>
        /// The neck perimeter in cm of the person read DTO
        /// </summary>
        public double? NeckInCm { get; set; }

        /// <summary>
        /// The gender of the person read DTO
        /// </summary>
        public PersonGender PersonGender { get; set; }
    }
}
