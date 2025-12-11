namespace LipometryAppAPI.Contracts.Responses
{
    /// <summary>
    /// The athlete read DTO
    /// </summary>
    public class AthleteRead : PersonRead
    {
        /// <summary>
        /// The sport of the athlete read DTO
        /// </summary>
        public string Sport { get; set; } = string.Empty;
    }
}
