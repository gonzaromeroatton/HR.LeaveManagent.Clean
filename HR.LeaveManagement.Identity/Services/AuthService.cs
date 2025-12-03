using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Models.Identity;
using HR.LeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity;

using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HR.LeaveManagement.Identity.Services;

/// <summary>
/// 
/// </summary>
public sealed class AuthService : IAuthService
{
    /// <summary>
    /// 
    /// </summary>
    private readonly UserManager<ApplicationUser> _userManager;

    /// <summary>
    /// 
    /// </summary>
    private readonly SignInManager<ApplicationUser> _signInManager;

    /// <summary>
    /// 
    /// </summary>
    private readonly JwtSettings _jwtSettings;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userManager"></param>
    /// <param name="signInManager"></param>
    /// <param name="jwtSettings"></param>
    public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, JwtSettings jwtSettings)
    {
        this._userManager = userManager;
        this._signInManager = signInManager;
        this._jwtSettings = jwtSettings;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<AuthResponse> Login(AuthRequest request)
    {
        var user = await this._userManager.FindByEmailAsync(request.Email);

        if (user == null)
            throw new BadRequestException($"User with {request.Email} not found.");
    
        var result = await this._signInManager.CheckPasswordSignInAsync(user, request.Password, false);

        if (result.Succeeded == false)
            throw new BadRequestException($"Invalid credentials for '{request.Email}'.");

        JwtSecurityToken jwtSecurityToken = await GenerateToken(user);

        var response = new AuthResponse
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken)
        };

        return response;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<RegistrationResponse> Register(RegistrationRequest request)
    {
        var user = new ApplicationUser
        {
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            UserName = request.UserName,
            EmailConfirmed = true
        };

        var result = await this._userManager.CreateAsync(user, request.Password);

        if (result.Succeeded == false)
            throw new BadRequestException($"Registration failed for '{request.Email}'.");

        await this._userManager.AddToRoleAsync(user, "Employee");
        return new RegistrationResponse { UserId = user.Id };
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
    {
        var userClaims = await this._userManager.GetClaimsAsync(user);
        var roles = await this._userManager.GetRolesAsync(user);

        var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("uid", user.Id)
        }
        .Union(userClaims)
        .Union(roleClaims);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._jwtSettings.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: this._jwtSettings.Issuer,
            audience: this._jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(this._jwtSettings.DurationInMinutes),
            signingCredentials: signingCredentials);

        return jwtSecurityToken;
    }
}
