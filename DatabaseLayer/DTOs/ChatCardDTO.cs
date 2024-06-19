using DatabaseLayer.Entities.Enums;

namespace DataBaseLayer.DTOs;

public record ChatCardDTO(long Id, string Title, string Question, ChatState ChatState, string AuthorName);