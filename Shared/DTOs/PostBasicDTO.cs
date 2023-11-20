namespace Shared.DTOs;

public class PostBasicDTO
{
    public int PostId { get; }
    public string AuthorName { get; }
    public string PostTitle { get; }

    public PostBasicDTO(int postId, string authorName, string postTitle)
    {
        PostId = postId;
        AuthorName = authorName;
        PostTitle = postTitle;
    }
}