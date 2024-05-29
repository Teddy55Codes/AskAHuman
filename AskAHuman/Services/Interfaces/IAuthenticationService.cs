using System.Security.Claims;
using FluentResults;

namespace AskAHuman.Services.Interfaces;

public interface IAuthenticationService
{
    /// <summary>
    /// Used to Login a user.
    /// </summary>
    /// <param name="username">The username of the user to login.</param>
    /// <param name="password">The password of the user to login.</param>
    /// <returns>The jwt token for the user.</returns>
    public string? Login(string username, string password);
    
    /// <summary>
    /// User to register a new user.
    /// </summary>
    /// <param name="username">The username of the new user.</param>
    /// <param name="password">The password of the new user.</param>
    /// <returns></returns>
    public bool Register(string username, string password);

    /// <summary>
    /// Used to validate a jwt token.
    /// </summary>
    /// <param name="token">The token to validate.</param>
    /// <returns>A result containing the parsed content of the jwt token.</returns>
    public Result<ClaimsPrincipal> ValidateJWT(string token);
}