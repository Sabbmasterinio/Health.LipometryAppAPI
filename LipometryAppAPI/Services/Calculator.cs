using LipometryAppAPI.Contracts.Models;

namespace LipometryAppAPI.Services
{
    /// <summary>
    /// The calculator is responsible for all the calculations needed in the app.
    /// </summary>
    public static class Calculator
    {
        #region Formulas
        /// <summary>
        /// Calculation of BMI 
        /// </summary>
        /// <param name="weightInKg"></param>
        /// <param name="heightInCm"></param>
        /// <returns>Returns the Body Mass Index (BMI)<returns>
        /// <exception cref="ArgumentException"></exception>
        public static double CalculateBMI(double weightInKg, double heightInCm)
        {
            if (heightInCm <= 0) throw new ArgumentException("Height must be greater than zero.");
            if (weightInKg <= 0) throw new ArgumentException("Weight must be greater than zero.");
            double heightM = heightInCm / 100.0;
            return weightInKg / (heightM * heightM);
        }

        /// <summary>
        /// Calculation of WHR
        /// </summary>
        /// <param name="waistInCm"></param>
        /// <param name="hipInCm"></param>
        /// <returns>Returns the Waist-to-Hip Ratio (WHR)</returns>
        /// <exception cref="ArgumentException"></exception>
        public static double CalculateWHR(double waistInCm, double hipInCm)
        {
            if (hipInCm <= 0) throw new ArgumentException("Hip circumference must be greater than zero.");
            if (waistInCm <= 0) throw new ArgumentException("Waist circumference must be greater than zero.");
            return waistInCm / hipInCm;
        }

        /// <summary>
        /// Calculation of BFP, using the BMI based formula
        /// </summary>
        /// <param name="bmi"></param>
        /// <param name="age"></param>
        /// <param name="gender"></param>
        /// <returns>Returns the Body-Fat Percentage (BFP).</returns>
        /// <exception cref="ArgumentException"></exception>
        public static double CalculateBFP(double bmi, int age, PersonGender gender)
        {
            if (age <= 0) throw new ArgumentException("Age must be greater than zero.");

            // Formula implementation for male 
            if (gender == PersonGender.Male)
                return (1.20 * bmi) + (0.23 * age) - (16.2);

            // Formula implementation for female 
            return (1.20 * bmi) + (0.23 * age) - 5.4;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="heightCm"></param>
        /// <param name="waistCm"></param>
        /// <param name="neckCm"></param>
        /// <param name="gender"></param>
        /// <returns>Returns the Body Fat % (using U.S. Navy Formula).</returns>
        public static double CalculateBodyFat(double heightCm, double waistCm, double hipCm, double neckCm, PersonGender gender)
        {
            // Formula implementation for male 
            if (gender == PersonGender.Male)
                return 495 / (1.0324 - 0.19077 * Math.Log10(waistCm - neckCm) + 0.15456 * Math.Log10(heightCm)) - 450;

            // Formula implementation for female 
            return 495 / (1.29579 - 0.35004 * Math.Log10(waistCm + hipCm - neckCm) + 0.22100 * Math.Log10(heightCm)) - 450;
        }
        #endregion


        /// <summary>
        /// Calculation of Age
        /// </summary>
        /// <param name="dateOfBirth"></param>
        /// <returns>Returns the age.</returns>
        public static int CalculateAgeFromDob(DateOnly dateOfBirth)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            int age = today.Year - dateOfBirth.Year;
            if (dateOfBirth > today.AddYears(-age)) 
                age--;
            return age;
        }


    }
}
