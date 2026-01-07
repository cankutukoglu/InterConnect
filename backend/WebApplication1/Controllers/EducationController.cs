using LinkedInClone.Api.Data;
using LinkedInClone.Api.Models;
using LinkedInClone.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LinkedInClone.Api.Controllers;

[ApiController]
[Authorize]
public class EducationController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly IOwnershipService _owner;
    private readonly IValidationService _validator;

    public EducationController(AppDbContext db, IOwnershipService owner, IValidationService validator)
    {
        _db = db;
        _owner = owner;
        _validator = validator;
    }

    [HttpPost("users/{id:int}/educations")]
    public async Task<IActionResult> Create([FromRoute] int id, [FromBody] EducationTemplate record)
    {
        if (!_owner.IsOwner(id)) return Forbid();
        var length = _validator.ValidateEducation(record);
        if (length != null) return BadRequest(length);
        var user = await _db.Users.FindAsync(id);
        if (user == null) return NotFound("User not found.");
        if (record.StartYear != null && (record.StartYear < 1900)) return BadRequest("Year must be greater than 1900.");
        if ((record.EndYear != null || record.StartYear != null) && (record.StartYear >= record.EndYear)) return BadRequest("StartYear must be less than EndYear.");
        var currentYear = DateTime.UtcNow.Year;
        if (record.EndYear > currentYear || record.StartYear > currentYear) return BadRequest("StartYear and EndYear must be less or equal to current year.");
        var education = new Education
        {
            UserId = id,
            School = record.School,
            Degree = record.Degree,
            City = record.City,
            Country = record.Country,
            StartYear = record.StartYear,
            EndYear = record.EndYear,
            Activities = record.Activities,
            LogoPicUrl = record.LogoPicUrl,
        };
        _db.Educations.Add(education);
        user.Educations.Add(education);
        await _db.SaveChangesAsync();
        return Created($"/users/{id}/educations/{education.Id}", education);
    }

    [HttpPut("users/{id:int}/educations/{eduId:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromRoute] int eduId, [FromBody] EducationTemplate record)
    {
        if (!_owner.IsOwner(id)) return Forbid();
        var length = _validator.ValidateEducation(record);
        if (length != null) return BadRequest(length);
        var user = await _db.Users.FindAsync(id);
        if (user == null) return NotFound("User not found.");
        var education = await _db.Educations.FindAsync(eduId);
        if (education == null) return NotFound("Education not found.");
        if (record.StartYear != null && (record.StartYear < 1900)) return BadRequest("Year must be greater than 1900.");
        if ((record.EndYear != null || record.StartYear != null) && (record.StartYear >= record.EndYear)) return BadRequest("StartYear must be less than EndYear.");
        var currentYear = DateTime.UtcNow.Year;
        if (record.EndYear > currentYear || record.StartYear > currentYear) return BadRequest("StartYear and EndYear must be less or equal to current year.");
        education.UserId = id;
        education.School = record.School;
        education.Degree = record.Degree;
        education.City = record.City;
        education.Country = record.Country;
        education.StartYear = record.StartYear;
        education.EndYear = record.EndYear;
        education.Activities = record.Activities;
        education.LogoPicUrl = record.LogoPicUrl;
        await _db.SaveChangesAsync();
        return Ok(education);
    }

    [HttpDelete("educations/{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var education = await _db.Educations.SingleOrDefaultAsync(edu => edu.Id == id);
        if (education == null) return NotFound("Education not found.");
        if (!_owner.IsOwner(education.UserId)) return Forbid();
        _db.Educations.Remove(education);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}


