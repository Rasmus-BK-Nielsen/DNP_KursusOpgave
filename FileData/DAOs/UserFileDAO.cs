using Application.DAOInterfaces;
using Shared.Domain;
using Shared.DTOs;

namespace FileData.DAOs;

public class UserFileDAO : IUserDAO
{
    private readonly FileContext context;
    
    public UserFileDAO(FileContext context)
    {
        this.context = context;
    }
    
    public Task<User> CreateAsync(User user)
    {
        int userId = 1;
        if (context.Users.Any())
        {
            userId = context.Users.Max(u => u.Id);
            userId++;
        }

        user.Id = userId;
        user.SecurityLevel = 1;

        context.Users.Add(user);
        context.SaveChanges();

        return Task.FromResult(user);
    }

    public Task<User?> GetByUsernameAsync(string username)
    {
        User? existing = context.Users.FirstOrDefault(u =>
            u.UserName.Equals(username, StringComparison.OrdinalIgnoreCase)
        );
        return Task.FromResult(existing);
    }

    public Task<IEnumerable<User>> GetAsync(SearchUserParametersDTO searchParameters)
    {
        IEnumerable<User> users = context.Users.AsEnumerable();
        if(searchParameters.UsernameContains != null)
        {
            users = users.Where(u => u.UserName.Contains(searchParameters.UsernameContains,
                StringComparison.OrdinalIgnoreCase));
        }
        
        return Task.FromResult(users);
    }
    
    public Task UpdateAsync(User user)
    {
        User? existing = context.Users.FirstOrDefault(u => u.Id == user.Id);
        if (existing == null)
        {
            throw new Exception($"User with ID {user.Id} not found!");
        }

        context.Users.Remove(existing);
        context.Users.Add(user);
        
        context.SaveChanges();
        
        return Task.CompletedTask;
    }

    public Task<User> GetByIdAsync(int id)
    {
        User? existing = context.Users.FirstOrDefault(u => u.Id == id);
        return Task.FromResult(existing);
    }
}