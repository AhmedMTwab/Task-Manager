using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TaskManager.Application.DTOs.Authentication;
using TaskManager.Domain.Exceptions;

namespace TaskManager.Application.Commands.Authentication;

public class SignInHandler(UserManager<ApplicationUser> userManager, IConfiguration configuration)
    : IRequestHandler<SignInCommand, TokenDTO>
{
    public async Task<TokenDTO> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        var dto = request.SignInData;

        var user = await userManager.FindByNameAsync(dto.Username);
        if (user == null)
            throw new BadRequestException("Wrong username.");

        if (await userManager.IsLockedOutAsync(user))
        {
            var lockoutEnd = await userManager.GetLockoutEndDateAsync(user);
            throw new BadRequestException($"Account is locked out until {lockoutEnd}.");
        }

        var passwordValid = await userManager.CheckPasswordAsync(user, dto.Password);
        if (!passwordValid)
        {
            await userManager.AccessFailedAsync(user);
            throw new UnauthorizedException("Wrong password.");
        }

        await userManager.ResetAccessFailedCountAsync(user);

        var userClaims = await userManager.GetClaimsAsync(user);
        var securityKey = configuration["Jwt:Key"]!;
        var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(securityKey));
        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

        var expiration = DateTime.Now.AddDays(1);
        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: userClaims,
            signingCredentials: signingCredentials,
            notBefore: DateTime.UtcNow,
            expires: expiration
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return new TokenDTO
        {
            Token = tokenString,
            Expire_Date = expiration
        };
    }
}
