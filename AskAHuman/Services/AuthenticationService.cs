using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AskAHuman.Services.Interfaces;
using DatabaseLayer;
using DatabaseLayer.Entities;
using Microsoft.IdentityModel.Tokens;

namespace AskAHuman.Services;

// TODO: Add token refresh
public class AuthenticationService : IAuthenticationService
{
    private IConfiguration _configuration;
    private IDbService _dbService;
    
    public AuthenticationService(IConfiguration configuration, IDbService dbService)
    {
        _configuration = configuration;
        _dbService = dbService;
    }
    
    public string? Login(string username, string password)
    {
        // TODO: check if user is connected via correct config
        User? user;
        using (var unitOfWork = _dbService.UnitOfWork)
        {
            user = unitOfWork.Users.GetByName(username);
        }

        if (user == null) return null;

        return GetHashForComparison(password, user.PasswordSalt) == user.PasswordHash ? GenerateJWT(user.Id.ToString()) : null;
    }
    
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