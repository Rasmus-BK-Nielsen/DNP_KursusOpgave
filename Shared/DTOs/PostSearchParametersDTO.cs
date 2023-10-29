namespace Shared.DTOs;

public class PostSearchParametersDTO
{
    public string? UserName { get; }
    public int? UserId { get; }
    public string? PostTitle { get; }
    public int? PostId { get; }
    
    public PostSearchParametersDTO(string? userName, int? userId, string? postTitle, int? postId)
    {
        UserName = userName;
        UserId = userId;
        PostTitle = postTitle;
        PostId = postId;
    }
}