namespace Shared.DTOs;

public class UserUpdateDTO
{
    public int Id { get; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
    
    public UserUpdateDTO(int id)
    {
        Id = id;
    }
}