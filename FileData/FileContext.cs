using System.Text.Json;
using Shared.Domain;

namespace FileData;

public class FileContext
{
    // FileData is kept the same as tutorial, to make the change to DataBase similar to the next tutorial.
    // FileData is not a good solution for a real application.
    private const string filePath = "data.json";
    private DataContainer? _dataContainer;

    public ICollection<User> Users
    {
        get
        {
            LoadData();
            return _dataContainer!.Users;
        }
    }
    
    public ICollection<ForumPost> ForumPosts
    {
        get
        {
            LoadData();
            return _dataContainer!.ForumPosts;
        }
    }
    
    private void LoadData()
    {
        if (_dataContainer != null) return;
    
        if (!File.Exists(filePath))
        {
            _dataContainer = new ()
            {
                ForumPosts = new List<ForumPost>(),
                Users = new List<User>()
            };
            return;
        }
        string content = File.ReadAllText(filePath);
        _dataContainer = JsonSerializer.Deserialize<DataContainer>(content);
    }
    
    public void SaveChanges()
    {
        string serialized = JsonSerializer.Serialize(_dataContainer, new JsonSerializerOptions
            {
                WriteIndented = true
            }
        );
        File.WriteAllText(filePath, serialized);
        _dataContainer = null;
    }
}