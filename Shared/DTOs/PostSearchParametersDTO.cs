namespace Shared.DTOs;

public class PostSearchParametersDTO
{
    public string? UserName { get; }
    
    // TODO : Cleanup when testing completed.
    // public int? UserId { get; }
    public string? PostTitle { get; }
    
    //public int? PostId { get; }
    
    public PostSearchParametersDTO(string? userName, string? postTitle)
    {
        UserName = userName;
        // Unsure if searching by UserId is necessary
        // UserId = userId;
        PostTitle = postTitle;
        // PostId = postId;
    }
}