using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LipometryAppAPI.Models
{
    public class BodyMeasurement
    {
        /// <summary>
        /// The id of the measurement
        /// </summary>
        [Key]
        public Guid MeasurementId { get; init; }

        /// <summary>
        /// The id of the person - Foreign key to Person
        /// </summary>
        public Guid PersonId { get; set; }

        /// <summary>
        /// The date and time of the measurement
        /// </summary>
        public DateTime MeasurementDate { get; set; } = DateTime.UtcNow;

        [ForeignKey(nameof(PersonId))]
        public Person Person { get; set; } = null!;

        /// <summary>
        /// The weight in Kg of the person
        /// </summary>
        public double? WeightInKg { get; set; } = 0.0;

        /// <summary>
        /// The Height in cm of the person
        /// </summary>
        public double? HeightInCm { get; set; } = 0.0;

        /// <summary>
        /// The waist perimeter in cm of the person
        /// </summary>
        public double? WaistInCm { get; set; } = 0.0;

        /// <summary>
        /// The hip perimeter in cm of the person
        /// </summary>
        public double? HipInCm { get; set; } = 0.0;

        /// <summary>
        /// The neck perimeter in cm of the person
        /// </summary>
        public double? NeckInCm { get; set; } = 0.0;

    }
}
