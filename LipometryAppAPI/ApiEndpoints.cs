namespace LipometryAppAPI
{
    public static class ApiEndpoints
    {
        private const string BasePath = "api";

        public static class Person
        {
            public const string BasePerson = $"{BasePath}/Person";
            public const string ById = $"{BasePerson}/{{id}}";
            public const string Adults = $"{BasePerson}/adults";
            public const string ByGender = $"{BasePerson}/gender/{{gender}}";
        }

        public static class Athlete
        {
            public const string BaseAthlete = $"{BasePath}/Athlete";
            public const string ById = $"{BaseAthlete}/{{id}}";

        }
    } 
}
