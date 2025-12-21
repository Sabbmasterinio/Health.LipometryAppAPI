using System.ComponentModel.DataAnnotations;

namespace LipometryAppAPI.Contracts.Requests
{
    public class AthleteCreateRequest : PersonCreateRequest
    {
        #region Public Members
        /// <summary>
        /// The sport of the athlete create DTO
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Sport { get; set; } = string.Empty;
        #endregion
    }
}
