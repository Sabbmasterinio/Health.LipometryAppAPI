using System.ComponentModel.DataAnnotations;

namespace LipometryAppAPI.Contracts.Requests
{
    public class AthleteCreate : PersonCreate
    {
        /// <summary>
        /// The sport of the athlete create DTO
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Sport { get; set; } = string.Empty;
    }
}
