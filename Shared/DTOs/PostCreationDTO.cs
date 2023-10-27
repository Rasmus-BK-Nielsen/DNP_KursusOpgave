namespace Shared.DTOs;

public class PostCreationDTO
{
    public int OwnerId { get; } // Id of User
    public string PostTitle { get; }
    public string PostBody { get; }
    
    public PostCreationDTO(int ownerId, string postTitle, string postBody)
    {
        OwnerId = ownerId;
        PostTitle = postTitle;
        PostBody = postBody;
    }
}