using Shared.Domain;
using Shared.DTOs;

namespace Application.DAOInterfaces;

public interface IPostDAO
{
    Task<Post> CreateAsync(Post post);
    Task<IEnumerable<Post>> GetAsync(PostSearchParametersDTO searchParameters);
}