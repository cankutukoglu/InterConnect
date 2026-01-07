namespace LinkedInClone.Api.Models;

public record SkillTemplate(
    string Name
);

public class Skill
{
    public int Id { get; set; }
    public int UserId { get; set; }

    public string Name { get; set; } = "";
}


