using JwtAuthApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Authorize(Roles = "Admin")] 
[Route("api/[controller]")]
[ApiController]
public class AdminController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;

    public AdminController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpGet("users")]
    public IActionResult GetAllUsers()
    {
        var users = _userManager.Users.ToList();

        var userList = users.Select(u => new
        {
            u.Id,
            u.UserName,
            u.Email,
            u.Role
        });

        return Ok(userList);
    }

    [HttpDelete("users/{id}")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        // Busca al usuario por su ID
        var user = await _userManager.FindByIdAsync(id);

        if (user == null)
        {
            return NotFound(new { message = "User not found." });
        }

        // Elimina al usuario
        var result = await _userManager.DeleteAsync(user);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return Ok(new { message = "User deleted successfully!" });
    }

    [HttpPut("users/{id}/role")]
    public async Task<IActionResult> UpdateUserRole(string id, [FromBody] UpdateRoleModel model)
    {
        // Busca al usuario por su ID
        var user = await _userManager.FindByIdAsync(id);

        if (user == null)
        {
            return NotFound(new { message = "User not found." });
        }

        // Actualiza el rol del usuario
        user.Role = model.Role;

        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return Ok(new { message = "User role updated successfully!" });
    }
}
