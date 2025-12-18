namespace LipometryAppAPI.Contracts.Responses
{
    /// <summary>
    /// The athlete read DTO
    /// </summary>
    public class AthleteReadResponse : PersonReadResponse
    {
        /// <summary>
        /// The sport of the athlete read DTO
        /// </summary>
        public string Sport { get; set; } = string.Empty;
    }
}
