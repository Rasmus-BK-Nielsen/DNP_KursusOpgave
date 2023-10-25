namespace Shared.DTOs;

public class UserCreationDTO
{
    public string UserName { get; }
    public string Password { get; }
    
    public UserCreationDTO(string userName, string password)
    {
        UserName = userName;
        Password = password;
    }
}