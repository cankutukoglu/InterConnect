using LinkedInClone.Api.Data;
using LinkedInClone.Api.Models;
using LinkedInClone.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LinkedInClone.Api.Controllers;

[ApiController]
[Authorize]
public class LanguageController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly IOwnershipService _owner;
    private readonly IValidationService _validator;

    public LanguageController(AppDbContext db, IOwnershipService owner, IValidationService validator)
    {
        _db = db;
        _owner = owner;
        _validator = validator;
    }

    [HttpPost("users/{id:int}/languages")]
    public async Task<IActionResult> Create([FromRoute] int id, [FromBody] LanguageTemplate record)
    {
        if (!_owner.IsOwner(id)) return Forbid();
        var length = _validator.ValidateLanguage(record);
        if (length != null) return BadRequest(length);
        var user = await _db.Users.FindAsync(id);
        if (user == null) return NotFound("User not found.");
        var language = new Language { UserId = id, Name = record.Name };
        _db.Languages.Add(language);
        user.Languages.Add(language);
        await _db.SaveChangesAsync();
        return Created($"/users/{id}/languages/{language.Id}", language);
    }

    [HttpPut("users/{id:int}/languages/{langId:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromRoute] int langId, [FromBody] LanguageTemplate record)
    {
        if (!_owner.IsOwner(id)) return Forbid();
        var length = _validator.ValidateLanguage(record);
        if (length != null) return BadRequest(length);
        var user = await _db.Users.FindAsync(id);
        if (user == null) return NotFound("User not found.");
        var language = await _db.Languages.FindAsync(langId);
        if (language == null) return NotFound("Language not found.");
        language.UserId = id;
        language.Name = record.Name;
        await _db.SaveChangesAsync();
        return Ok(language);
    }

    [HttpDelete("languages/{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var language = await _db.Languages.SingleOrDefaultAsync(lang => lang.Id == id);
        if (language == null) return NotFound("Language not found.");
        if (!_owner.IsOwner(language.UserId)) return Forbid();
        _db.Languages.Remove(language);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}


