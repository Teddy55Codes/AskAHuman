using System;
using System.Collections.Generic;

namespace DatabaseLayer.Entities;

public partial class Message
{
    public long Id { get; set; }

    public long ChatId { get; set; }

    public long AuthorId { get; set; }

    public string Content { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
    
    public virtual User Author { get; set; }
}
