namespace LipometryAppAPI.Models
{
    public class Athlete : Person
    {
        public Athlete()
        {
            
        }

        /// <summary>
        /// The sport of the athlete.
        /// </summary>
        public string Sport { get; set; } = string.Empty;
    }
}
