namespace Shared.DTOs;

public class PostUpdateDTO
{
    public int PostId { get; }
    public int? OwnerId { get; set; }
    public string? PostTitle { get; set; }
    public string? PostBody { get; set; }
    
    public PostUpdateDTO(int postId)
    {
        PostId = postId;
    }
    
}