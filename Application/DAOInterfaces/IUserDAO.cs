using Shared.Domain;
using Shared.DTOs;

namespace Application.DAOInterfaces;

public interface IUserDAO
{
    Task<User> CreateAsync(User user);
    Task<User?> GetByUsernameAsync(string username);
    Task<IEnumerable<User>> GetAsync(SearchUserParametersDTO searchParameters);
    Task UpdateAsync(User user);
    Task<User> GetByIdAsync(int id);
}