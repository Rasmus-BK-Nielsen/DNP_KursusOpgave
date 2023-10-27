using Application.DAOInterfaces;
using Application.LogicInterfaces;
using Shared.Domain;
using Shared.DTOs;

namespace Application.Logic;

public class UserLogic : IUserLogic
{
    private readonly IUserDAO UserDAO;
    
    public UserLogic (IUserDAO userDAO)
    {
        this.UserDAO = userDAO;
    }
    
    public async Task<User> CreateAsync(UserCreationDTO userToCreate)
    {
        User? existing = await UserDAO.GetByUsernameAsync(userToCreate.UserName);
        if (existing != null)
            throw new Exception("Username already taken!");

        ValidateData(userToCreate);
        User toCreate = new User
        {
            UserName = userToCreate.UserName,
            Password = userToCreate.Password
        };
    
        User created = await UserDAO.CreateAsync(toCreate);
    
        return created;
    }

    public Task<IEnumerable<User>> GetAsync(SearchUserParametersDTO searchParameters)
    {
        return UserDAO.GetAsync(searchParameters);
    }

    public async Task UpdateAsync(UserUpdateDTO userToUpdate)
    {
        User? existing = UserDAO.GetByIdAsync(userToUpdate.Id).Result;
        if (existing == null)
            throw new Exception($"User with ID {userToUpdate.Id} not found!");
        
        // Patchwork solution to prevent duplicate usernames
        // Should be a combined with .GetByIdAsync() to create a private method for checking both.
        // Problem has arisen due to me wanting both a unique username and a unique ID. 
        User? existingUsername = await UserDAO.GetByUsernameAsync(userToUpdate.UserName);
        if (existingUsername != null)
            throw new Exception("Username already taken!");
        // --------------------------------------------------
        
        string userNameToUse = userToUpdate.UserName ?? existing.UserName;
        string passwordToUse = userToUpdate.Password ?? existing.Password;

        // This is a makeshift solution, making sure user data is still following the rules
        UserCreationDTO validateUpdated = new UserCreationDTO(userNameToUse, passwordToUse);
        ValidateData(validateUpdated);
        
        User updated = new User
        {
            Id = userToUpdate.Id,
            UserName = userNameToUse,
            Password = passwordToUse,
        };
        
        await UserDAO.UpdateAsync(updated);
    }

    private static void ValidateData(UserCreationDTO userToCreate)
    {
        string userName = userToCreate.UserName;
        string password = userToCreate.Password;
        
        // TODO: Add custom exception types!

        if (userName.Length < 3)
            throw new Exception("Username must be at least 3 characters!");

        if (userName.Length > 15)
            throw new Exception("Username must be less than 16 characters!");
        
        // Prevent admin usernames
        if (userName.Equals("admin", StringComparison.OrdinalIgnoreCase))
            throw new Exception("Username cannot be admin!");
        
        // Temporary solution to prevent null passwords
        if (password.Length < 4)
            throw new Exception("Password must be at least 4 characters!");
    }
}