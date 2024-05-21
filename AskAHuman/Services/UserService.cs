using AskAHuman.Services.Interfaces;
using DatabaseLayer;
using DataBaseLayer.DTOs;
using DatabaseLayer.Entities;
using FluentResults;

namespace AskAHuman.Services;

public class UserService : IUserService
{
    private ILogger<UserService> _logger;
    private IAuthenticationService _authenticationService;
    private IDbService _dbService;
    
    public UserService(ILogger<UserService> logger, IAuthenticationService authenticationService, IDbService dbService)
    {
        _logger = logger;
        _authenticationService = authenticationService;
        _dbService = dbService;
    }

    public List<UserDTO> GetAll()
    {
        using var unitOfWork = _dbService.UnitOfWork;
        return unitOfWork.Users.GetAll().Select(u => new UserDTO(u)).ToList();
    }

    public Result<User> GetById(int id)
    {
        using var unitOfWork = _dbService.UnitOfWork;
        var user = unitOfWork.Users.GetByPrimaryKey(id);
        if (user == null) return Result.Fail($"User with id {id} doesn't exist.");
        return Result.Ok(user);
    }

    public string? Login(string username, string password) => _authenticationService.Login(username, password);

    public bool Register(string username, string password) => !UserExists(username) && _authenticationService.Register(username, password);

    public bool UserExists(string username)
    {
        if (username == string.Empty) return true;
        using var unitOfWork = _dbService.UnitOfWork;
        return unitOfWork.Users.GetByName(username.ToLower()) != null;
    }

    public bool RemoveUser(int id)
    {
        using var unitOfWork = _dbService.UnitOfWork;
        var user = unitOfWork.Users.GetByPrimaryKey(id);
        if (user == null) return false;
        unitOfWork.Users.Remove(user);
        unitOfWork.Commit();
        return true;
    }
}