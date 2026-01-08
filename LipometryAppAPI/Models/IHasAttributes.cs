using LipometryAppAPI.Contracts.Models;

namespace LipometryAppAPI.Models
{
    public interface IHasAttributes
    {
        DateOnly DateOfBirth { get; }
        PersonGender PersonGender { get; init; }
    }
}
