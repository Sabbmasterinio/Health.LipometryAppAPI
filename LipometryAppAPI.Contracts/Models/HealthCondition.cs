namespace LipometryAppAPI.Contracts.Models
{
    /// <summary>
    /// Specifies the health condition classification based on body weight relative to standard ranges.
    /// </summary>
    /// <remarks>The <see cref="HealthCondition"/> enumeration represents common categories used to describe
    /// an individual's health status with respect to weight, such as underweight, normal, overweight, and obese. These
    /// categories are typically determined by metrics like Body Mass Index (BMI), but the specific criteria may vary
    /// depending on context or application.</remarks>
    public enum HealthCondition
    {
        /// <summary>
        /// The underweight health condition
        /// </summary>
        Underweight,

        /// <summary>
        /// The normal health condition
        /// </summary>
        Normal,

        /// <summary>
        /// The overweight health condition
        /// </summary>
        Overweight,

        /// <summary>
        /// The obese health condition
        /// </summary>
        Obese
    }
}
