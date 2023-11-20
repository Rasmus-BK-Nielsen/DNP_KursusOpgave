using Application.DAOInterfaces;
using Application.LogicInterfaces;
using Shared.Domain;
using Shared.DTOs;

namespace Application.Logic;

public class PostLogic : IPostLogic
{
    private readonly IPostDAO _postDAO;
    private readonly IUserDAO _userDAO;
    
    public PostLogic(IPostDAO postDAO, IUserDAO userDAO)
    {
        _postDAO = postDAO;
        _userDAO = userDAO;
    }
    
    public async Task<Post> CreateAsync(PostCreationDTO dto)
    {
        User? user = await _userDAO.GetByIdAsync(dto.OwnerId);
        if (user == null)
        {
            throw new Exception($"User with ID {dto.OwnerId} not found!");
        }
        
        Post post = new(user, dto.PostTitle, dto.PostBody);
        ValidatePost(post);
        Post created = await _postDAO.CreateAsync(post);
        
        return created;
    }
    
    public Task<IEnumerable<Post>> GetAsync(PostSearchParametersDTO searchParameters)
    {
        return _postDAO.GetAsync(searchParameters);
    }

    public async Task UpdateAsync(PostUpdateDTO post)
    {
        Post? existing = await _postDAO.GetByIdAsync(post.PostId);
        
        if (existing == null)
        {
            throw new Exception($"Post with ID {post.PostId} not found!");
        }

        User? user = null;
        if (post.OwnerId != null)
        {
            user = await _userDAO.GetByIdAsync((int)post.OwnerId);
            if (user == null)
            {
                throw new Exception($"User with ID {post.OwnerId} not found!");
            }
        }

        User userToUse = user ?? existing.PostAuthor;
        string titleToUse = post.PostTitle ?? existing.PostTitle;
        string bodyToUse = post.PostBody ?? existing.PostBody;
        
        Post updated = new(userToUse, titleToUse, bodyToUse);
        updated.PostId = existing.PostId;
        updated.PostDate = existing.PostDate;
        
        ValidatePost(updated);
        
        await _postDAO.UpdateAsync(updated);
    }

    public async Task DeleteAsync(int postId, int userId)
    {
        Post? post = await _postDAO.GetByIdAsync(postId);
        if (post == null)
        {
            throw new Exception($"Post with ID {postId} not found!");
        }
        
        if (post.PostAuthor.Id != userId)
        {
            throw new Exception($"User with ID {userId} is not the owner of post with ID {postId}!");
        }
        
        await _postDAO.DeleteAsync(postId);
    }

    public async Task<PostBasicDTO> GetByIdAsync(int id)
    {
        Post? post = await _postDAO.GetByIdAsync(id);
        if (post == null)
        {
            throw new Exception($"Post with ID {id} not found!");
        }
        
        return new PostBasicDTO(post.PostId, post.PostAuthor.UserName, post.PostTitle);
    }


    private void ValidatePost(Post dto)
    {
        if (string.IsNullOrWhiteSpace(dto.PostTitle))
            throw new Exception("Title cannot be empty");
        
        if (dto.PostTitle.Length > 50)
            throw new Exception("Title can't be longer than 50 characters");
        
        if (string.IsNullOrWhiteSpace(dto.PostBody))
            throw new Exception("Content cannot be empty");
        
        if (dto.PostBody.Length > 255)
            throw new Exception("Post can't be longer than 255 characters");
    }
}