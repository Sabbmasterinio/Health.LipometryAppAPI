using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LipometryAppAPI.Contracts.Models
{
    public sealed class PersonQuery
    {
        public AgeGroup? AgeGroup { get; init; }
        public int? MinAge { get; init; }
        public int? MaxAge { get; init; }
    }
}
