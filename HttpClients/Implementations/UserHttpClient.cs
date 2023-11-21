using System.Net.Http.Json;
using System.Text.Json;
using HttpClients.ClientInterfaces;
using Shared.Domain;
using Shared.DTOs;

namespace HttpClients.Implementations;

public class UserHttpClient : IUserService
{
    private readonly HttpClient _client;
    
    public UserHttpClient(HttpClient client)
    {
        _client = client;
    }
    
    public async Task<User> CreateAsync(UserCreationDTO dto)
    {
        HttpResponseMessage response = await _client.PostAsJsonAsync("/user", dto);
        string result = await response.Content.ReadAsStringAsync();
        
        if(!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }
        
        User user = JsonSerializer.Deserialize<User>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;

        return user;
    }

    public async Task<IEnumerable<User>> GetUsersAsync(string? usernameContains = null)
    {
        string uri = "/user";
        if (!string.IsNullOrEmpty(usernameContains))
        {
            uri += $"?username={usernameContains}";
        }
        
        HttpResponseMessage response = await _client.GetAsync(uri);
        string result = await response.Content.ReadAsStringAsync();
        if(!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }
        
        IEnumerable<User> users = JsonSerializer.Deserialize<IEnumerable<User>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;

        return users;
    }
}