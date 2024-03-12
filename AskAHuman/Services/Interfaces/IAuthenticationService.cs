namespace AskAHuman.Services.Interfaces;

public interface IAuthenticationService
{
    public string? Login(string username, string password);
    public bool Register(string username, string password);
}