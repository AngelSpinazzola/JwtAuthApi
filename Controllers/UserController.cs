using JwtAuthApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

[Authorize(Roles = "User")] 
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpGet("profile")]
    public async Task<IActionResult> GetProfile()
    {
        // Obtiene el nombre del usuario autenticado
        var userName = User.Identity.Name;

        // Busca al usuario en la base de datos
        var user = await _userManager.FindByNameAsync(userName);

        if (user == null)
        {
            return NotFound(new { message = "User not found." });
        }

        // Devuelve el perfil del usuario
        return Ok(new
        {
            user.Id,
            user.UserName,
            user.Email,
            user.Role
        });
    }

    [HttpPut("update-profile")]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileModel model)
    {
        // Obtiene el nombre del usuario autenticado
        var userName = User.Identity.Name;

        // Busca al usuario en la base de datos
        var user = await _userManager.FindByNameAsync(userName);

        if (user == null)
        {
            return NotFound(new { message = "User not found." });
        }

        // Actualiza la información del usuario
        user.Email = model.Email;
        user.UserName = model.UserName;

        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return Ok(new { message = "Profile updated successfully!" });
    }

    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel model)
    {
        // Obtiene el nombre del usuario autenticado
        var userName = User.Identity.Name;

        // Busca al usuario en la base de datos
        var user = await _userManager.FindByNameAsync(userName);

        if (user == null)
        {
            return NotFound(new { message = "User not found." });
        }

        // Cambia la contraseña
        var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return Ok(new { message = "Password changed successfully!" });
    }
}

