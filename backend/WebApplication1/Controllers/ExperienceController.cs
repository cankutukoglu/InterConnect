using LinkedInClone.Api.Data;
using LinkedInClone.Api.Models;
using LinkedInClone.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LinkedInClone.Api.Controllers;

[ApiController]
[Authorize]
public class ExperienceController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly IOwnershipService _owner;
    private readonly IValidationService _validator;

    public ExperienceController(AppDbContext db, IOwnershipService owner, IValidationService validator)
    {
        _db = db;
        _owner = owner;
        _validator = validator;
    }

    [HttpPost("users/{id:int}/experiences")]
    public async Task<IActionResult> Create([FromRoute] int id, [FromBody] ExperienceTemplate record)
    {
        if (!_owner.IsOwner(id)) return Forbid();
        var length = _validator.ValidateExperience(record);
        if (length != null) return BadRequest(length);
        var user = await _db.Users.FindAsync(id);
        if (user == null) return NotFound("User not found.");
        var isCurrent = record.EndDate == null;
        var minDate = new DateOnly(1900, 1, 1);
        if (record.StartDate < minDate) return BadRequest("StartDate must be greater than 01.01.1900");
        if (record.EndDate != null && (record.StartDate >= record.EndDate.Value)) return BadRequest("StartDate must be less than EndDate.");
        var currentDate = DateOnly.FromDateTime(DateTime.UtcNow);
        if (record.EndDate > currentDate || record.StartDate > currentDate) return BadRequest("StartDate and EndDate must be less than or equal to today.");
        var experience = new Experience
        {
            UserId = id,
            Company = record.Company,
            Title = record.Title,
            StartDate = record.StartDate,
            EndDate = record.EndDate,
            City = record.City,
            Country = record.Country,
            Description = record.Description,
            LogoPicUrl = record.LogoPicUrl,
            IsCurrent = isCurrent
        };
        _db.Experiences.Add(experience);
        user.Experiences.Add(experience);
        await _db.SaveChangesAsync();
        return Created($"/users/{id}/experiences/{experience.Id}", experience);
    }

    [HttpPut("users/{id:int}/experiences/{expId:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromRoute] int expId, [FromBody] ExperienceTemplate record)
    {
        if (!_owner.IsOwner(id)) return Forbid();
        var length = _validator.ValidateExperience(record);
        if (length != null) return BadRequest(length);
        var user = await _db.Users.FindAsync(id);
        if (user == null) return NotFound("User not found.");
        var experience = await _db.Experiences.FindAsync(expId);
        if (experience == null) return NotFound("Experience not found.");
        var isCurrent = record.EndDate == null;
        var minDate = new DateOnly(1900, 1, 1);
        if (record.StartDate < minDate) return BadRequest("StartDate must be greater than 01.01.1900");
        if (record.EndDate != null && (record.StartDate >= record.EndDate.Value)) return BadRequest("StartDate must be less than EndDate.");
        var currentDate = DateOnly.FromDateTime(DateTime.UtcNow);
        if (record.EndDate > currentDate || record.StartDate > currentDate) return BadRequest("StartDate and EndDate must be less than or equal to today.");
        experience.UserId = id;
        experience.Company = record.Company;
        experience.Title = record.Title;
        experience.StartDate = record.StartDate;
        experience.EndDate = record.EndDate;
        experience.City = record.City;
        experience.Country = record.Country;
        experience.Description = record.Description;
        experience.LogoPicUrl = record.LogoPicUrl;
        experience.IsCurrent = isCurrent;
        await _db.SaveChangesAsync();
        return Ok(experience);
    }

    [HttpDelete("experiences/{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var experience = await _db.Experiences.SingleOrDefaultAsync(exp => exp.Id == id);
        if (experience == null) return NotFound("Experience not found.");
        if (!_owner.IsOwner(experience.UserId)) return Forbid();
        _db.Experiences.Remove(experience);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}


