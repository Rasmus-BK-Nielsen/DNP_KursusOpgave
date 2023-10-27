namespace Shared.DTOs;

public class UserSearchParametersDTO
{
    public string? UsernameContains { get; }
    
    public UserSearchParametersDTO(string? usernameContains)
    {
        UsernameContains = usernameContains;
    }
}