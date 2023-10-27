namespace Shared.Domain;

public class User
{
    // TODO: Figure out how to auto generate an Admin user on startup
    // Standard users security level is 1, admin is 2
    // I want to be able to edit these:
    public string UserName { get; set; }
    public string Password { get; set; }

    // I want persistent-layer to assign these:
    public int Id { get; set; }
    public int SecurityLevel { get; set; }
}