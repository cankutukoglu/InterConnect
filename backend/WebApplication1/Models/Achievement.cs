namespace LinkedInClone.Api.Models;

public record AchievementTemplate(
    string Title,
    int? Year,
    string? Description
);

public class Achievement
{
    public int Id { get; set; }
    public int UserId { get; set; }

    public string Title { get; set; } = "";
    public int? Year { get; set; }
    public string? Description { get; set; }
}


