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
        if (password.Length < 8)
            throw new Exception("Password must be at least 8 characters!");
    }
}