namespace LinkedInClone.Api.Models;

public record ExperienceTemplate(
    string Company,
    string Title,
    DateOnly StartDate,
    DateOnly? EndDate,
    string City,
    string Country,
    string? Description,
    string? LogoPicUrl
);

public class Experience
{
    public int Id { get; set; }
    public int UserId { get; set; }

    public string Company { get; set; } = "";
    public string Title { get; set; } = "";
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public string City { get; set; } = "";
    public string Country { get; set; } = "";
    public string? Description { get; set; }
    public string? LogoPicUrl { get; set; }
    public bool IsCurrent { get; set; }
}


