using System;
using System.Collections.Generic;

namespace DatabaseLayer.Entities;

public partial class User
{
    public long Id { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string PasswordSalt { get; set; } = null!;

    public long? Reputation { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime LastOnlineAt { get; set; }

    public virtual ICollection<Chat> ChatUsersAnswerers { get; set; } = new List<Chat>();

    public virtual ICollection<Chat> ChatUsersQuestionings { get; set; } = new List<Chat>();

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
