namespace Shared.Domain;

public class User
{
    // I want to be able to edit these:
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }

    // I want persistent-layer to assign these:
    public int SecurityLevel { get; set; }
}