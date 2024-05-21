using System;
using System.Collections.Generic;

namespace DatabaseLayer.Entities;

public partial class Chat
{
    public long Id { get; set; }

    public long? UsersAnswererId { get; set; }

    public long UsersQuestioningId { get; set; }

    public sbyte Completed { get; set; }
    
    public string Title { get; set; }
    
    public string Question { get; set; }

    public virtual User UsersAnswerer { get; set; }

    public virtual User UsersQuestioning { get; set; } = null!;
}
