using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace ApiService;

public class CurrentUser
{
    public TodoUser? User { get; set; }
    public ClaimsPrincipal Principal { get; set; } = default!;
    public string Id => Principal.FindFirstValue(ClaimTypes.NameIdentifier)!;
    public bool IsAdmin => Principal.IsInRole("admin");
}
