namespace LipometryAppAPI
{
    public static class ApiEndpoints
    {
        private const string BasePath = "Lipometry";

        public static class Person
        {
            public const string BasePerson = $"{BasePath}/Person";
            public const string GetAll = $"{BasePerson}";
            public const string GetById = $"{BasePerson}/{{id}}";
            public const string Create = $"{BasePerson}";
            public const string Update = $"{BasePerson}/{{id}}";
            public const string Remove = $"{BasePerson}/{{id}}";
            public const string GetByGender = $"{BasePerson}/gender/{{gender}}";
            public const string GetAdults = $"{BasePerson}/adults";
        }

        public static class Athlete
        {
            public const string BaseAthlete = $"{BasePath}/Athlete";
            public const string GetAll = $"{BaseAthlete}";
            public const string GetById = $"{BaseAthlete}/{{id}}";
            public const string Create = $"{BaseAthlete}";
            public const string Update = $"{BaseAthlete}/{{id}}";
            public const string Remove = $"{BaseAthlete}/{{id}}";
            public const string GetBySport = $"{BaseAthlete}/sport/{{sport}}";
        }
    } 
}
