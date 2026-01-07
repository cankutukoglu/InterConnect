using LinkedInClone.Api.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LinkedInClone.Api.Controllers;

[ApiController]
[Route("users")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly AppDbContext _db;
    public UsersController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var list = await _db.Users
            .Select(user => new
            {
                user.Id,
                user.Email,
                user.CreatedAt,
                Profile = user.Profile == null ? null : new
                {
                    user.Profile.Id,
                    user.Profile.FullName,
                    user.Profile.City,
                    user.Profile.Country,
                    user.Profile.Description,
                    user.Profile.ConnectionCount,
                    user.Profile.ProfilePic
                },
                Experiences = user.Experiences.Any()
                    ? user.Experiences.Select(exp => new
                    {
                        exp.Id,
                        exp.Title,
                        exp.Company,
                        exp.City,
                        exp.Country,
                        exp.Description,
                        exp.StartDate,
                        exp.EndDate,
                        exp.IsCurrent,
                        exp.LogoPicUrl
                    }).ToList()
                    : null,
                Educations = user.Educations.Any()
                    ? user.Educations.Select(edu => new
                    {
                        edu.Id,
                        edu.School,
                        edu.Degree,
                        edu.City,
                        edu.Country,
                        edu.StartYear,
                        edu.EndYear,
                        edu.Activities,
                        edu.LogoPicUrl
                    }).ToList()
                    : null,
                Skills = user.Skills.Any()
                    ? user.Skills.Select(skill => new { skill.Id, skill.Name }).ToList()
                    : null,
                Achievements = user.Achievements.Any()
                    ? user.Achievements.Select(achievement => new
                    {
                        achievement.Id,
                        achievement.Title,
                        achievement.Description,
                        achievement.Year
                    }).ToList()
                    : null,
                Languages = user.Languages.Any()
                    ? user.Languages.Select(language => new
                    {
                        language.Id,
                        language.Name
                    }).ToList()
                    : null
            })
            .ToListAsync();
        return Ok(list);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var user = await _db.Users
            .Where(user => user.Id == id)
            .Select(user => new
            {
                user.Id,
                user.Email,
                user.CreatedAt,
                Profile = user.Profile == null ? null : new
                {
                    user.Profile.Id,
                    user.Profile.FullName,
                    user.Profile.City,
                    user.Profile.Country,
                    user.Profile.Description,
                    user.Profile.ConnectionCount,
                    user.Profile.ProfilePic
                },
                Experiences = user.Experiences.Any()
                    ? user.Experiences.Select(exp => new
                    {
                        exp.Id,
                        exp.Title,
                        exp.Company,
                        exp.City,
                        exp.Country,
                        exp.Description,
                        exp.StartDate,
                        exp.EndDate,
                        exp.IsCurrent,
                        exp.LogoPicUrl
                    }).ToList()
                    : null,
                Educations = user.Educations.Any()
                    ? user.Educations.Select(edu => new
                    {
                        edu.Id,
                        edu.School,
                        edu.Degree,
                        edu.City,
                        edu.Country,
                        edu.StartYear,
                        edu.EndYear,
                        edu.Activities,
                        edu.LogoPicUrl
                    }).ToList()
                    : null,
                Skills = user.Skills.Any()
                    ? user.Skills.Select(skill => new
                    {
                        skill.Id,
                        skill.Name
                    }).ToList()
                    : null,
                Achievements = user.Achievements.Any()
                    ? user.Achievements.Select(achievement => new
                    {
                        achievement.Id,
                        achievement.Title,
                        achievement.Description,
                        achievement.Year
                    }).ToList()
                    : null,
                Languages = user.Languages.Any()
                    ? user.Languages.Select(language => new
                    {
                        language.Id,
                        language.Name
                    }).ToList()
                    : null
            })
            .FirstOrDefaultAsync();
        if (user != null) return Ok(user);
        return NotFound();
    }
}


