namespace LinkedInClone.Api.Models;

public record LanguageTemplate(
    string Name
);

public class Language
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; } = "";
}


