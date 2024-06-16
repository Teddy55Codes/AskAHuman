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

    /// <inheritdoc />
    public List<UserDTO> GetAll()
    {
        using var unitOfWork = _dbService.UnitOfWork;
        return unitOfWork.Users.GetAll().Select(u => new UserDTO(u)).ToList();
    }

    /// <inheritdoc />
    public Result<User> GetById(long id)
    {
        using var unitOfWork = _dbService.UnitOfWork;
        var user = unitOfWork.Users.GetByPrimaryKey(id);
        if (user == null) return Result.Fail($"User with id {id} doesn't exist.");
        return Result.Ok(user);
    }

    /// <inheritdoc />
    public bool UserExists(string username)
    {
        if (username == string.Empty) return true;
        using var unitOfWork = _dbService.UnitOfWork;
        return unitOfWork.Users.GetByName(username.ToLower()) != null;
    }

    /// <inheritdoc />
    public bool RemoveUser(long id)
    {
        using var unitOfWork = _dbService.UnitOfWork;
        var user = unitOfWork.Users.GetByPrimaryKey(id);
        if (user == null) return false;
        unitOfWork.Users.Remove(user);
        unitOfWork.Commit();
        return true;
    }

    /// <inheritdoc />
    public void UserDisconnect(long id)
    {
        using var unitOfWork = _dbService.UnitOfWork;
        var user = unitOfWork.Users.GetByPrimaryKey(id);
        if (user == null) return;
        unitOfWork.Commit();
    }

    /// <inheritdoc />
    public void UserConnect(long id)
    {
        using var unitOfWork = _dbService.UnitOfWork;
        var user = unitOfWork.Users.GetByPrimaryKey(id);
        if (user == null) return;
        unitOfWork.Commit();
    }
}