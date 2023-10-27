namespace Shared.Domain;

public class Post
{
    // I don't want user to be able to edit these:
    public User PostAuthor { get; }
    public string PostTitle { get; }

    // I want user to be able to edit these:
    public string PostBody { get; set; }

    // I want persistent-layer to assign these:
    public string PostDate { get; set; }
    public int PostId { get; set; }
    
    public Post(User postAuthor, string postTitle, string postBody)
    {
        PostAuthor = postAuthor;
        PostTitle = postTitle;
        PostBody = postBody;
    }
}