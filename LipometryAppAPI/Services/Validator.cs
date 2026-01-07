namespace LipometryAppAPI.Services
{
    /// <summary>
    /// A reusable, generic validator that can validate any type T
    /// based on a set of user-defined rules.
    /// </summary>
    public class Validator<T>
    {
        private readonly List<(Func<T, bool> Rule, string Message)> rules = new();

        /// <summary>
        /// Registers a new validation rule for the given type.
        /// </summary>
        public void AddRule(Func<T, bool> rule, string message)
        {
            rules.Add((rule, message));
        }

        /// <summary>
        /// Validates a single object of type T and returns a list of messages for failed rules.
        /// </summary>
        public List<string> Validate(T item)
        {
            var errors = new List<string>();

            foreach (var (rule, message) in rules)
            {
                if (!rule(item))
                    errors.Add(message);
            }

            return errors;
        }

        /// <summary>
        /// Validates a collection of objects and returns only those that pass all rules.
        /// </summary>
        public IEnumerable<T> ValidateAll(IEnumerable<T> items)
        {
            return items.Where(item => !Validate(item).Any());
        }
    }
}
