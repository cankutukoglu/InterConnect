using LinkedInClone.Api.Models;

namespace LinkedInClone.Services;

public interface IValidationService
{
    string? ValidateProfile(ProfileTemplate template);
    string? ValidateExperience(ExperienceTemplate template);
    string? ValidateEducation(EducationTemplate template);
    string? ValidateAchievement(AchievementTemplate template);
    string? ValidateSkill(SkillTemplate template);
    string? ValidateLanguage(LanguageTemplate template);
    string? ValidateRegister(RegisterRequest template);
    string? ValidateLogin(LoginRequest template);
}

public class ValidationService : IValidationService
{
    public string? ValidateProfile(ProfileTemplate template)
    {
        if (IsLengthInvalid(template.FullName, 100)) return "Max length is exceeded.";
        if (IsLengthInvalid(template.City, 80)) return "Max length is exceeded.";
        if (IsLengthInvalid(template.Country, 80)) return "Max length is exceeded.";
        if (IsLengthInvalid(template.Description, 800)) return "Max length is exceeded.";
        if (IsLengthInvalid(template.ProfilePic, 550)) return "Max length is exceeded.";
        return null;
    }

    public string? ValidateExperience(ExperienceTemplate template)
    {
        if (IsLengthInvalid(template.Company, 150)) return "Max length is exceeded.";
        if (IsLengthInvalid(template.Title, 80)) return "Max length is exceeded.";
        if (IsLengthInvalid(template.City, 80)) return "Max length is exceeded.";
        if (IsLengthInvalid(template.Country, 80)) return "Max length is exceeded.";
        if (IsLengthInvalid(template.Description, 800)) return "Max length is exceeded.";
        if (IsLengthInvalid(template.LogoPicUrl, 550)) return "Max length is exceeded.";
        return null;
    }

    public string? ValidateEducation(EducationTemplate template)
    {
        if (IsLengthInvalid(template.School, 150)) return "Max length is exceeded.";
        if (IsLengthInvalid(template.Degree, 100)) return "Max length is exceeded.";
        if (IsLengthInvalid(template.City, 80)) return "Max length is exceeded.";
        if (IsLengthInvalid(template.Country, 80)) return "Max length is exceeded.";
        if (IsLengthInvalid(template.Activities, 800)) return "Max length is exceeded.";
        if (IsLengthInvalid(template.LogoPicUrl, 550)) return "Max length is exceeded.";
        return null;
    }

    public string? ValidateAchievement(AchievementTemplate template)
    {
        if (IsLengthInvalid(template.Title, 150)) return "Max length is exceeded.";
        if (IsLengthInvalid(template.Description, 800)) return "Max length is exceeded.";
        return null;
    }

    public string? ValidateSkill(SkillTemplate template)
    {
        if (IsLengthInvalid(template.Name, 50)) return "Max length is exceeded.";
        return null;
    }

    public string? ValidateLanguage(LanguageTemplate template)
    {
        if (IsLengthInvalid(template.Name, 50)) return "Max length is exceeded.";
        return null;
    }

    public string? ValidateRegister(RegisterRequest template)
    {
        if (template.Email.Length > 150) return "Email max length is exceeded.";
        if (template.Password.Length > 50) return "Password max length is exceeded.";
        return null;
    }

    public string? ValidateLogin(LoginRequest template)
    {
        if (template.Email.Length > 150) return "Email max length is exceeded.";
        if (template.Password.Length > 50) return "Password max length is exceeded.";
        return null;
    }

    static bool IsLengthInvalid(string? text, int max)
    {
        if (text != null && text.Length > max) return true;
        return false;
    }
}


