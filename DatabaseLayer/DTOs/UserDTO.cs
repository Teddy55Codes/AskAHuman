using DatabaseLayer.Entities;

namespace DataBaseLayer.DTOs;

public class UserDTO
{
    public UserDTO() {}

    public UserDTO(User user)
    {
        Username = user.Username;
        Reputation = user.Reputation;
        IsOnline = user.IsOnline;
        CreatedAt = user.CreatedAt;
        LastOnlineAt = user.LastOnlineAt;
    }
    
    public string Username { get; set; }
    public long Reputation { get; set; } 
    public bool IsOnline { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastOnlineAt { get; set; }
}