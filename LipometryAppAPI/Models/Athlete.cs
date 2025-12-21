namespace LipometryAppAPI.Models
{
    /// <summary>
    /// Represents an individual who participates in a sport, including information about their specific discipline.
    /// </summary>
    /// <remarks>The <see cref="Athlete"/> class extends <see cref="Person"/> to provide additional details
    /// relevant to sports participants, such as the sport they are associated with.</remarks>
    public class Athlete : Person
    {
        #region Constructors
        public Athlete()
        {
            
        }
        #endregion

        #region Public Members
        /// <summary>
        /// The sport of the athlete.
        /// </summary>
        public string Sport { get; set; } = string.Empty;
        #endregion
    }
}
