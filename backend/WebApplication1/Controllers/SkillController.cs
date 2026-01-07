using LinkedInClone.Api.Data;
using LinkedInClone.Api.Models;
using LinkedInClone.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LinkedInClone.Api.Controllers;

[ApiController]
[Authorize]
public class SkillController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly IOwnershipService _owner;
    private readonly IValidationService _validator;

    public SkillController(AppDbContext db, IOwnershipService owner, IValidationService validator)
    {
        _db = db;
        _owner = owner;
        _validator = validator;
    }

    [HttpPost("users/{id:int}/skills")]
    public async Task<IActionResult> Create([FromRoute] int id, [FromBody] SkillTemplate record)
    {
        if (!_owner.IsOwner(id)) return Forbid();
        var length = _validator.ValidateSkill(record);
        if (length != null) return BadRequest(length);
        var user = await _db.Users.FindAsync(id);
        if (user == null) return NotFound("User not found.");
        var skill = new Skill { UserId = id, Name = record.Name };
        _db.Skills.Add(skill);
        user.Skills.Add(skill);
        await _db.SaveChangesAsync();
        return Created($"/users/{id}/skills/{skill.Id}", skill);
    }

    [HttpPut("users/{id:int}/skills/{skillId:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromRoute] int skillId, [FromBody] SkillTemplate record)
    {
        if (!_owner.IsOwner(id)) return Forbid();
        var length = _validator.ValidateSkill(record);
        if (length != null) return BadRequest(length);
        var user = await _db.Users.FindAsync(id);
        if (user == null) return NotFound("User not found.");
        var skill = await _db.Skills.FindAsync(skillId);
        if (skill == null) return NotFound("Skill not found.");
        skill.UserId = id;
        skill.Name = record.Name;
        await _db.SaveChangesAsync();
        return Ok(skill);
    }

    [HttpDelete("skills/{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var skill = await _db.Skills.SingleOrDefaultAsync(skill => skill.Id == id);
        if (skill == null) return NotFound("Skill not found.");
        if (!_owner.IsOwner(skill.UserId)) return Forbid();
        _db.Skills.Remove(skill);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}


