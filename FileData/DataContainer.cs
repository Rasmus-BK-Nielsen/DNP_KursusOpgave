using Shared.Domain;

namespace FileData;

public class DataContainer
{
    public ICollection<User> Users { get; set; }
    public ICollection<Post> ForumPosts { get; set; }
}