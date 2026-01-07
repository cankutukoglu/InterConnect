using LinkedInClone.Api.Data;
using LinkedInClone.Api.Models;
using LinkedInClone.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LinkedInClone.Api.Controllers;

[ApiController]
[Authorize]
public class ProfileController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly IOwnershipService _owner;
    private readonly IValidationService _validator;

    public ProfileController(AppDbContext db, IOwnershipService owner, IValidationService validator)
    {
        _db = db;
        _owner = owner;
        _validator = validator;
    }

    [HttpPost("users/{id:int}/profile")]
    public async Task<IActionResult> Upsert([FromRoute] int id, [FromBody] ProfileTemplate record)
    {
        if (!_owner.IsOwner(id)) return Forbid();
        var length = _validator.ValidateProfile(record);
        if (length != null) return BadRequest(length);
        var user = await _db.Users.FindAsync(id);
        if (user == null) return NotFound("User not found.");
        var profile = await _db.Profiles.SingleOrDefaultAsync(profile => profile.UserId == id);
        if (profile == null)
        {
            var newProfile = new Profile
            {
                UserId = id,
                FullName = record.FullName,
                Country = record.Country,
                City = record.City,
                Description = record.Description,
                ProfilePic = record.ProfilePic,
                ConnectionCount = 0
            };
            _db.Profiles.Add(newProfile);
            user.Profile = newProfile;
            await _db.SaveChangesAsync();
            return Created($"/users/{id}/profile", newProfile);
        }
        profile.FullName = record.FullName;
        profile.Country = record.Country;
        profile.City = record.City;
        profile.Description = record.Description;
        profile.ProfilePic = record.ProfilePic;
        if (record.ConnectionCount != null) profile.ConnectionCount = record.ConnectionCount.Value;
        await _db.SaveChangesAsync();
        return Ok(profile);
    }

    [HttpDelete("profile/{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var profile = await _db.Profiles.SingleOrDefaultAsync(prof => prof.Id == id);
        if (profile == null) return NotFound("Profile not found.");
        if (!_owner.IsOwner(profile.UserId)) return Forbid();
        _db.Profiles.Remove(profile);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}


