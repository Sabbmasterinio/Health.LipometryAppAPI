using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LipometryAppAPI.Contracts.Responses
{
    public class BodyMeasurementReadResponse
    {
        public Guid Id { get; set; }
        public DateOnly DateRecorded { get; set; }
        public double WeightInKg { get; set; }
        public double? HeightInCm { get; set; }
        public double? WaistInCm { get; set; }
        public double? HipInCm { get; set; }
        public double? NeckInCm { get; set; }
    }
}
