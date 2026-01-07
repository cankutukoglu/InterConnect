namespace LinkedInClone.Api.Models;

public record EducationTemplate(
    string School,
    string Degree,
    string City,
    string Country,
    int? StartYear,
    int? EndYear,
    string? Activities,
    string? LogoPicUrl
);

public class Education
{
    public int Id { get; set; }
    public int UserId { get; set; }

    public string School { get; set; } = "";
    public string Degree { get; set; } = "";
    public string City { get; set; } = "";
    public string Country { get; set; } = "";
    public int? StartYear { get; set; }
    public int? EndYear { get; set; }
    public string? Activities { get; set; }
    public string? LogoPicUrl { get; set; }
}


