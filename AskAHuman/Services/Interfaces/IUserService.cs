using DataBaseLayer.DTOs;
using DatabaseLayer.Entities;
using FluentResults;

namespace AskAHuman.Services.Interfaces;


public interface IUserService
{
    public List<UserDTO> GetAll();
    public Result<User> GetById(int id);
    public string? Login(string username, string password);
    public bool Register(string username, string password);
    public bool UserExists(string username);
    public bool RemoveUser(int id);
}