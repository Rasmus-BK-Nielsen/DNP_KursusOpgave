using System.Globalization;
using Application.DAOInterfaces;
using Shared.Domain;
using Shared.DTOs;

namespace FileData.DAOs;

public class PostFileDAO : IPostDAO
{
    private readonly FileContext _context;
    
    public PostFileDAO(FileContext context)
    {
        _context = context;
    }
    public Task<Post> CreateAsync(Post post)
    {
        int id = 1;
        if (_context.ForumPosts.Any())
        {
            id = _context.ForumPosts.Max(p => p.PostId);
            id++;
        }

        post.PostId = id;
        post.PostDate = DateTime.Now.ToString("MM/dd/yyyy H:mm");
        
        _context.ForumPosts.Add(post);
        _context.SaveChanges();
        
        return Task.FromResult(post);
    }

    // TODO: Implement this method
    public Task<IEnumerable<Post>> GetAsync(PostSearchParametersDTO searchParameters)
    {
        IEnumerable<Post> result = _context.ForumPosts.AsEnumerable();

        if (!string.IsNullOrEmpty(searchParameters.UserName))
        {
            result = _context.ForumPosts.Where(post => 
                post.PostAuthor.UserName.Equals(searchParameters.UserName, StringComparison.OrdinalIgnoreCase));
        }
        
        if (!string.IsNullOrEmpty(searchParameters.PostTitle))
        {
            result = _context.ForumPosts.Where(post => 
                post.PostTitle.Equals(searchParameters.PostTitle, StringComparison.OrdinalIgnoreCase));
        }
        
        return Task.FromResult(result);
    }
}