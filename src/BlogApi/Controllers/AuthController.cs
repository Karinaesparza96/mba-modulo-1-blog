using BlogApi.DTOs;
using BlogCore.Business.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BlogCore.Business.Messages;

namespace BlogApi.Controllers;

[ApiController]
[Route("api/conta")]
public class AuthController(SignInManager<IdentityUser> signInManager,
                            UserManager<IdentityUser> userManager,
                            IOptions<JwtSettings> jwtSettings) : ControllerBase
{
    [HttpPost("registrar")]
    public async Task<ActionResult> Registrar(RegisterUserDto registerUser)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);

        var user = new IdentityUser
        {
            UserName = registerUser.Email,
            Email = registerUser.Email,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(user, registerUser.Password!);

        if (result.Succeeded)
        {
            await signInManager.SignInAsync(user, false);
            return Ok(await GerarJwt(user.Email!));
        }

        return Problem(Messages.FalhaRegistrarUsuario);
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login(LoginUserDto loginUser)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);

        var result = await signInManager.PasswordSignInAsync(loginUser.Email!, loginUser.Password!, false, true);
        
        if (result.Succeeded)
        {
            return Ok(await GerarJwt(loginUser.Email!));
        }

        return Problem(Messages.UsuarioOuSenhaIncorretos);
    }

    private async Task<string> GerarJwt(string email)
    {
        var user = await userManager.FindByEmailAsync(email);

        if (user == null) return string.Empty;

        var roles = await userManager.GetRolesAsync(user);

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id),
        };

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var tokenHandler = new JwtSecurityTokenHandler();

        var key = Encoding.ASCII.GetBytes(jwtSettings.Value.Segredo!);

        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Issuer = jwtSettings.Value.Emissor,
            Audience = jwtSettings.Value.Audiencia,
            Expires = DateTime.UtcNow.AddHours(jwtSettings.Value.ExpiracaoHoras),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        });

        var encodedToken = tokenHandler.WriteToken(token);

        return encodedToken;
    }
}
