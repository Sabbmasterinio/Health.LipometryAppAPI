using System.ComponentModel.DataAnnotations;

namespace LipometryAppAPI.Contracts.Requests
{
    public class PersonCreate
    {
        /// <summary>
        /// The first name of the person create DTO
        /// </summary>
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// The last name of the person create DTO
        /// </summary>
        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// The date of birth of the person create DTO
        /// </summary>
        [Required]
        public DateOnly DateOfBirth { get; set; }

        /// <summary>
        /// The weight in Kg of the person create DTO
        /// </summary>
        [Range(1, 500)]
        public double? WeightInKg { get; set; }

        /// <summary>
        /// The height in cm of the person create DTO
        /// </summary>
        [Range(30, 300)]
        public double? HeightInCm { get; set; }

        /// <summary>
        /// The waist perimeter in cm of the person create DTO
        /// </summary>
        [Range(1, 300)]
        public double? WaistInCm { get; set; }

        /// <summary>
        /// The hip perimeter in cm of the person create DTO
        /// </summary>
        [Range(1, 300)]
        public double? HipInCm { get; set; }

        /// <summary>
        /// The neck perimeter in cm of the person create DTO
        /// </summary>
        [Range(1, 300)]
        public double? NeckInCm { get; set; }

        /// <summary>
        /// The gender of the person create DTO
        /// </summary>
        [Required]
        public PersonGender PersonGender { get; set; }
    }
}
