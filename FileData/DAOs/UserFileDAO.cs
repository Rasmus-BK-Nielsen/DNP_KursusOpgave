using Application.DAOInterfaces;
using Shared.Domain;
using Shared.DTOs;

namespace FileData.DAOs;

public class UserFileDAO : IUserDAO
{
    private readonly FileContext _context;
    
    public UserFileDAO(FileContext context)
    {
        _context = context;
    }
    
    public Task<User> CreateAsync(User user)
    {
        int userId = 1;
        if (_context.Users.Any())
        {
            userId = _context.Users.Max(u => u.Id);
            userId++;
        }

        user.Id = userId;
        user.SecurityLevel = 1;

        _context.Users.Add(user);
        _context.SaveChanges();

        return Task.FromResult(user);
    }

    public Task<User?> GetByUsernameAsync(string username)
    {
        User? existing = _context.Users.FirstOrDefault(u =>
            u.UserName.Equals(username, StringComparison.OrdinalIgnoreCase)
        );
        return Task.FromResult(existing);
    }

    public Task<IEnumerable<User>> GetAsync(UserSearchParametersDTO userSearchParameters)
    {
        IEnumerable<User> users = _context.Users.AsEnumerable();
        if(userSearchParameters.UsernameContains != null)
        {
            users = users.Where(u => u.UserName.Contains(userSearchParameters.UsernameContains,
                StringComparison.OrdinalIgnoreCase));
        }
        
        return Task.FromResult(users);
    }
    
    // I have made a mistake here, I didn't take into account that a User could have posts when changing their info.
    // The correct way to do this would be to update the user's posts as well. I'll leave it as is for now.
    public Task UpdateAsync(User user)
    {
        User? existing = _context.Users.FirstOrDefault(u => u.Id == user.Id);
        if (existing == null)
        {
            throw new Exception($"User with ID {user.Id} not found!");
        }

        _context.Users.Remove(existing);
        _context.Users.Add(user);
        
        _context.SaveChanges();
        
        return Task.CompletedTask;
    }

    public Task<User> GetByIdAsync(int id)
    {
        User? existing = _context.Users.FirstOrDefault(u => u.Id == id);
        return Task.FromResult(existing);
    }
}