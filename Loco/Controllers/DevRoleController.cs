#if DEBUG
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[Authorize]
public class DevRoleController : Controller
{
    private readonly UserManager<IdentityUser> _users;

    public DevRoleController(UserManager<IdentityUser> users)
    {
        _users = users;
    }

    [HttpPost]
    public async Task<IActionResult> MakeAdmin()
    {
        var user = await _users.GetUserAsync(User);
        await _users.AddToRoleAsync(user, "Admin");
        await _users.UpdateSecurityStampAsync(user); // refresh cookie
        return Redirect(Request.Headers["Referer"].ToString());
    }

    [HttpPost]
    public async Task<IActionResult> RemoveAdmin()
    {
        var user = await _users.GetUserAsync(User);
        await _users.RemoveFromRoleAsync(user, "Admin");
        await _users.UpdateSecurityStampAsync(user);
        return Redirect(Request.Headers["Referer"].ToString());
    }
}
#endif