using DataBaseLayer.DTOs;
using DatabaseLayer.Entities;
using FluentResults;

namespace AskAHuman.Services.Interfaces;


public interface IUserService
{
    /// <summary>
    /// Get all users.
    /// </summary>
    /// <returns>All users</returns>
    public List<UserDTO> GetAll();
    
    /// <summary>
    /// Gets a user be there id.
    /// </summary>
    /// <param name="id">The id of the user.</param>
    /// <returns>A result containing the user for the id.</returns>
    public Result<User> GetById(long id);
    
    /// <summary>
    /// Checks if a user exists.
    /// </summary>
    /// <param name="username">The username to check.</param>
    /// <returns>Whether the user already exists.</returns>
    public bool UserExists(string username);
    
    /// <summary>
    /// Removes a user.
    /// </summary>
    /// <param name="id">The id of the user to remove.</param>
    /// <returns>Whether the operation was succesful or not.</returns>
    public bool RemoveUser(int id);
}