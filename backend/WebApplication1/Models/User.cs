namespace LinkedInClone.Api.Models;

public record LoginRequest(
    string Email,
    string Password
);
public record AuthResponse(
    string AccessToken,
    string RefreshToken,
    int UserId
);
public record RefreshRequest(
    string RefreshToken
);

public record RegisterRequest(
    string Email,
    string Password
);

public class User
{
    public int Id { get; set; }
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Profile? Profile { get; set; }
    public List<Experience> Experiences { get; set; } = new();
    public List<Education> Educations { get; set; } = new();
    public List<Skill> Skills { get; set; } = new();
    public List<Achievement> Achievements { get; set; } = new();
    public List<Language> Languages { get; set; } = new();
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiration { get; set; }
}


