using LinkedInClone.Api.Data;
using LinkedInClone.Api.Models;
using LinkedInClone.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LinkedInClone.Api.Controllers;

[ApiController]
[Authorize]
public class AchievementController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly IOwnershipService _owner;
    private readonly IValidationService _validator;

    public AchievementController(AppDbContext db, IOwnershipService owner, IValidationService validator)
    {
        _db = db;
        _owner = owner;
        _validator = validator;
    }

    [HttpPost("users/{id:int}/achievements")]
    public async Task<IActionResult> Create([FromRoute] int id, [FromBody] AchievementTemplate record)
    {
        if (!_owner.IsOwner(id)) return Forbid();
        var length = _validator.ValidateAchievement(record);
        if (length != null) return BadRequest(length);
        var user = await _db.Users.FindAsync(id);
        if (user == null) return NotFound("User not found.");
        if (record.Year != null && (record.Year < 1900)) return BadRequest("Year must be greater than 1900.");
        var currentYear = DateTime.UtcNow.Year;
        if (record.Year > currentYear) return BadRequest("Year must be less or equal to current year.");
        var achievement = new Achievement { UserId = id, Title = record.Title, Year = record.Year, Description = record.Description };
        _db.Achievements.Add(achievement);
        user.Achievements.Add(achievement);
        await _db.SaveChangesAsync();
        return Created($"/users/{id}/achievements/{achievement.Id}", achievement);
    }

    [HttpPut("users/{id:int}/achievements/{achieveId:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromRoute] int achieveId, [FromBody] AchievementTemplate record)
    {
        if (!_owner.IsOwner(id)) return Forbid();
        var length = _validator.ValidateAchievement(record);
        if (length != null) return BadRequest(length);
        var user = await _db.Users.FindAsync(id);
        if (user == null) return NotFound("User not found.");
        var achievement = await _db.Achievements.FindAsync(achieveId);
        if (achievement == null) return NotFound("Achievement not found.");
        if (record.Year != null && (record.Year < 1900)) return BadRequest("Year must be greater than 1900");
        var currentYear = DateTime.UtcNow.Year;
        if (record.Year > currentYear) return BadRequest("Year must be less or equal to current year.");
        achievement.UserId = id;
        achievement.Title = record.Title;
        achievement.Year = record.Year;
        achievement.Description = record.Description;
        await _db.SaveChangesAsync();
        return Ok(achievement);
    }

    [HttpDelete("achievements/{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var achievement = await _db.Achievements.SingleOrDefaultAsync(achieve => achieve.Id == id);
        if (achievement == null) return NotFound("Achievement not found.");
        if (!_owner.IsOwner(achievement.UserId)) return Forbid();
        _db.Achievements.Remove(achievement);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}


