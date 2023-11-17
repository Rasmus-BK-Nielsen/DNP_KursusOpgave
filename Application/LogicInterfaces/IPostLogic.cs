using Shared.Domain;
using Shared.DTOs;

namespace Application.LogicInterfaces;

public interface IPostLogic
{
    Task<Post> CreateAsync(PostCreationDTO dto);
    Task<IEnumerable<Post>> GetAsync(PostSearchParametersDTO searchParameters);
    Task UpdateAsync(PostUpdateDTO post);
}