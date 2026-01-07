using System.Security.Claims;

namespace LinkedInClone.Services;

public interface IOwnershipService
{
    bool IsOwner(int routeUserId);
}

public class OwnershipService : IOwnershipService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public OwnershipService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public bool IsOwner(int routeUserId)
    {
        var http = _httpContextAccessor.HttpContext;
        if (http == null) return false;
        var sub = http.User.FindFirst(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub)
                 ?? http.User.FindFirst(ClaimTypes.NameIdentifier);
        return sub != null && int.TryParse(sub.Value, out var uid) && uid == routeUserId;
    }
}


