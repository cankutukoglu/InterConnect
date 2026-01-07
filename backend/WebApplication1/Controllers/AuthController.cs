using LinkedInClone.Api.Data;
using LinkedInClone.Api.Models;
using LinkedInClone.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LinkedInClone.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly JwtTokenService _tokens;
    private readonly IValidationService _validator;

    public AuthController(AppDbContext db, JwtTokenService tokens, IValidationService validator)
    {
        _db = db;
        _tokens = tokens;
        _validator = validator;
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var length = _validator.ValidateRegister(request);
        if (length != null) return BadRequest(length);
        if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password)) return BadRequest("Invalid fields");
        var mailExists = await _db.Users.AnyAsync(u => u.Email == request.Email);
        if (mailExists) return Conflict("Email is used by another user.");
        var user = new User { Email = request.Email.Trim().ToLowerInvariant(), PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password) };
        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        return Created($"/users/{user.Id}", new { user.Id, user.Email });
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var length = _validator.ValidateLogin(request);
        if (length != null) return BadRequest(length);
        var email = request.Email?.Trim().ToLowerInvariant();
        var user = await _db.Users.SingleOrDefaultAsync(u => u.Email == email);
        if (user == null) return Unauthorized();
        var passwordCheck = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
        if (!passwordCheck) return Unauthorized();
        var accessToken = _tokens.CreateAccessToken(user);
        var (refreshToken, refreshTokenExpiration) = _tokens.CreateRefreshToken();
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiration = refreshTokenExpiration;
        await _db.SaveChangesAsync();
        return Ok(new AuthResponse(accessToken, refreshToken, user.Id));
    }

    [HttpPost("refresh")]
    [AllowAnonymous]
    public async Task<IActionResult> Refresh([FromBody] RefreshRequest request)
    {
        var user = await _db.Users.SingleOrDefaultAsync(user => user.RefreshToken == request.RefreshToken);
        if (user == null || user.RefreshTokenExpiration == null || user.RefreshTokenExpiration < DateTime.UtcNow) return Unauthorized();
        var accessToken = _tokens.CreateAccessToken(user);
        var (newRefreshToken, refreshTokenExpiration) = _tokens.CreateRefreshToken();
        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiration = refreshTokenExpiration;
        await _db.SaveChangesAsync();
        return Ok(new AuthResponse(accessToken, newRefreshToken, user.Id));
    }

    [HttpGet("validation")]
    [Authorize]
    public async Task<IActionResult> Validate()
    {
        string? idClaim = null;
        var subClaim = User.FindFirst(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub);
        if (subClaim != null) idClaim = subClaim.Value; else { var nameIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier); if (nameIdClaim != null) idClaim = nameIdClaim.Value; }
        if (!int.TryParse(idClaim, out var userId)) return Unauthorized();
        var user = await _db.Users.FindAsync(userId);
        if (user == null) return Unauthorized();
        return Ok(new { user.Id, user.Email, user.CreatedAt });
    }
}


