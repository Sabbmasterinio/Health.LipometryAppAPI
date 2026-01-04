namespace LipometryAppAPI.Contracts.Models
{
    /// <summary>
    /// Specifies the age group categories used to classify individuals by age range.
    /// </summary>
    /// <remarks>The <see cref="AgeGroup"/> enumeration defines standard age groupings such as Child,
    /// Teenager, YoungAdult, Adult, and Senior. These categories can be used for demographic segmentation, eligibility
    /// checks, or other scenarios where age-based grouping is required. The specific age ranges for each group are
    /// defined by the enumeration values.</remarks>
    public enum AgeGroup
    {
        /// <summary>
        /// The child age group (Age Range: 4 - 12)
        /// </summary>
        Child,
        
        /// <summary>
        /// The teenager age group (Age Range: 13 - 17)
        /// </summary>
        Teenager,

        /// <summary>
        /// The young adult age group (Age Range: 18 - 25)
        /// </summary>
        YoungAdult,

        /// <summary>
        /// The adult age group (Age Range: 26 - 64)
        /// </summary>
        Adult,

        /// <summary>
        /// The senior age group (Age Range: 65+)
        /// </summary>
        Senior
    }
}
