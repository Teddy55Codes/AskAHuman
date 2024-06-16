using System.Security.Claims;
using FluentResults;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace AskAHuman.Services.Interfaces;

public interface IAuthenticationService
{
    /// <summary>
    /// If the user is authenticated this property holds the id of the user.
    /// If the user is not authenticated it is null.
    /// </summary>
    public long? AuthenticatedUser { get; set; }
    
    /// <summary>
    /// Used to Log in a user.
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
    /// Authenticates a user via the browsers local storage
    /// </summary>
    /// <returns>Is the user authenticated.</returns>
    public Task<bool> AuthenticateViaLocalStorage(ProtectedLocalStorage protectedLocalStorage);
    
    /// <summary>
    /// Used to validate a jwt token.
    /// </summary>
    /// <param name="token">The token to validate.</param>
    /// <returns>A result containing the parsed content of the jwt token.</returns>
    public Result<ClaimsPrincipal> ValidateJWT(string token);
}