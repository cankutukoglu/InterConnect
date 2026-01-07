namespace LinkedInClone.Api.Models;

public record ProfileTemplate(
    string FullName,
    string City,
    string Country,
    string? Description,
    string? ProfilePic,
    int? ConnectionCount
);

public class Profile
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string FullName { get; set; } = "";
    public string City { get; set; } = "";
    public string Country { get; set; } = "";
    public int ConnectionCount { get; set; } = 0;
    public string? Description { get; set; }
    public string? ProfilePic { get; set; }
}


