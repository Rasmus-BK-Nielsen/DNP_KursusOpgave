using Shared.Domain;
using Shared.DTOs;

namespace Application.LogicInterfaces;

public interface IUserLogic
{
    Task<User> CreateAsync(UserCreationDTO userToCreate);
    Task<IEnumerable<User>> GetAsync(UserSearchParametersDTO userSearchParameters);
    Task UpdateAsync(UserUpdateDTO userToUpdate);
}