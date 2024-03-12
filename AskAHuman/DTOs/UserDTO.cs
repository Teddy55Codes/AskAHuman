﻿using DatabaseLayer.Entities;

namespace AskAHuman.DTOs;

public class UserDTO
{
    public UserDTO() {}

    public UserDTO(User user)
    {
        Username = user.Username;
        Reputation = user.Reputation;
        CreatedAt = user.CreatedAt;
        LastOnlineAt = user.LastOnlineAt;
    }
    
    public string Username { get; set; }
    public long Reputation { get; set; } 
    public DateTime CreatedAt { get; set; }
    public DateTime LastOnlineAt { get; set; }
}