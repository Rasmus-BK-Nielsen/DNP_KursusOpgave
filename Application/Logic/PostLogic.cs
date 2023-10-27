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

        ValidatePost(dto);
        Post post = new(user, dto.PostTitle, dto.PostBody);
        Post created = await _postDAO.CreateAsync(post);
        
        return created;
    }
    
    private void ValidatePost(PostCreationDTO dto)
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