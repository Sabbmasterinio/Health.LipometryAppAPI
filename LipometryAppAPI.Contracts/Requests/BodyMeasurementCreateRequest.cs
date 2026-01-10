using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LipometryAppAPI.Contracts.Requests
{
    public class BodyMeasurementCreateRequest
    {
        [Required]
        public Guid PersonId { get; set; }

        [Required]
        public DateTime MeasurementDate { get; set; }

        [Range(10, 300)]
        public double? WeightInKg { get; set; }

        [Range(50, 250)]
        public double? HeightInCm { get; set; }

        [Range(30, 200)]
        public double? WaistInCm { get; set; }

        [Range(30, 200)]
        public double? HipInCm { get; set; }

        [Range(25, 50)]
        public double? NeckInCm { get; set; }
    }
}
