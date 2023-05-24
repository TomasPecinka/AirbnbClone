using AirbnbClone.Domain.Entities;

namespace AirbnbClone.Application.Common.Interfaces.Persistence;
public interface IUserRepository
{
    User? GetUserByEmail(string email);
    void Add(User user);
}
