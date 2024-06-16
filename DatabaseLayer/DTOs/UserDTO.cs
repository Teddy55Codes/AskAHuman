using DatabaseLayer.Entities;

namespace DataBaseLayer.DTOs;

public class UserDTO
{
    public UserDTO() {}

    public UserDTO(User user)
    {
        Username = user.Username;
        Reputation = user.Reputation;
        CreatedAt = user.CreatedAt;
    }
    
    public string Username { get; set; }
    public long Reputation { get; set; } 
    public DateTime CreatedAt { get; set; }
}