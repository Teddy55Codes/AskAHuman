﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AskAHuman.Services.Interfaces;
using DatabaseLayer;
using DatabaseLayer.Entities;
using FluentResults;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.IdentityModel.Tokens;

namespace AskAHuman.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IConfiguration _configuration;
    private readonly IDbService _dbService;
    
    public AuthenticationService(IConfiguration configuration, IDbService dbService)
    {
        _configuration = configuration;
        _dbService = dbService;
    }
    
    public long? AuthenticatedUser { get; set; }

    /// <inheritdoc />
    public string? Login(string username, string password)
    {
        using var unitOfWork = _dbService.UnitOfWork;
        var user = unitOfWork.Users.GetByName(username);
            
        if (user == null) return null;
        if (GetHashForComparison(password, user.PasswordSalt) != user.PasswordHash) return null;
        AuthenticatedUser = user.Id;
        return GenerateJWT(user.Id.ToString());
    }

    /// <inheritdoc />
    public async Task Logout(ProtectedLocalStorage protectedLocalStorage)
    {
        await protectedLocalStorage.DeleteAsync("Jwt");
        AuthenticatedUser = null;
    }

    /// <inheritdoc />
    public bool Register(string username, string password)
    {
        using var unitOfWork = _dbService.UnitOfWork;
        var passwordAndSalt = GenerateNewHashFromString(password);
        unitOfWork.Users.Add(new User
        {
            Username = username,
            PasswordHash = passwordAndSalt.Item1,
            PasswordSalt = passwordAndSalt.Item2
        });
        unitOfWork.Commit();
        return true;
    }
    
    /// <inheritdoc />
    public async Task<bool> AuthenticateViaLocalStorage(ProtectedLocalStorage protectedLocalStorage)
    {
        try
        {
            var jwt = await protectedLocalStorage.GetAsync<string>("Jwt");
        
            if (jwt.Success)
            {
                var result = ValidateJWT(jwt.Value!);
                if (int.TryParse(result.Value.Claims.FirstOrDefault(c => c.Type == "userId")?.Value, out var userId))
                {
                    AuthenticatedUser = userId;
                    return true;
                }
                
            }
        } catch (InvalidOperationException) {}

        return false;
    }
    
    /// <inheritdoc />
    public Result<ClaimsPrincipal> ValidateJWT(string token)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _configuration["Jwt:Issuer"],
            ValidAudience = _configuration["Jwt:Audience"],
            IssuerSigningKey = securityKey
        };

        try
        {
            var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken _);
            return Result.Ok(principal);
        }
        catch (Exception ex)
        {
            return Result.Fail("Jwt token is invalid.");
        }
    }
    
    private string GenerateJWT(string id)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[] {
            new Claim("userId", id)
        };

        var token = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddHours(3),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    #region Hashing
    private (string, string) GenerateNewHashFromString(string password)
    {
        var salt = GetRandomSalt();

        var tmpSource = ASCIIEncoding.ASCII.GetBytes(password + salt);

        using (var alg = SHA512.Create())
        {
            alg.ComputeHash(tmpSource);
            return (BitConverter.ToString(alg.Hash), salt);
        }
    }

    private string GetHashForComparison(string password, string salt)
    {
        var tmpSource = ASCIIEncoding.ASCII.GetBytes(password + salt);

        using (var alg = SHA512.Create())
        {
            alg.ComputeHash(tmpSource);
            return BitConverter.ToString(alg.Hash);
        }
    }

    private string GetRandomSalt()
    {
        var salt = new byte[32];
        using (var random = new RNGCryptoServiceProvider())
        {
            random.GetNonZeroBytes(salt);
        }

        var stringSalt = string.Join("", salt);

        return stringSalt;
    }

    #endregion
}