using System.ComponentModel.DataAnnotations;

namespace LipometryAppAPI.Contracts.Requests
{
    public class AthleteUpdateRequest : PersonUpdateRequest
    {
        /// <summary>
        /// The sport of the athlete update DTO
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Sport { get; set; } = string.Empty;
    }
}
