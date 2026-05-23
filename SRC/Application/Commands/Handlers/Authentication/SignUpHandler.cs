using System.Security.Claims;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using TaskManager.Domain.Exceptions;

namespace TaskManager.Application.Commands.Authentication;

public class SignUpHandler(UserManager<ApplicationUser> userManager, IMapper mapper) : IRequestHandler<SignUpCommand, Unit>
{
    public async Task<Unit> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        var dto = request.SignUpData;

        var existingUser = await userManager.FindByNameAsync(dto.Username);
        if (existingUser != null)
            throw new ConflictException("Username already exists.");

        var user = mapper.Map<ApplicationUser>(dto);

        var result = await userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded)
        {
            var errors = result.Errors
                .GroupBy(e => e.Code)
                .ToDictionary(g => g.Key, g => g.Select(e => e.Description).ToList());

            throw new BadRequestException("Registration failed.", errors);
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.UserName!),
            new(ClaimTypes.Email, user.Email!),
            new(ClaimTypes.MobilePhone, user.PhoneNumber ?? "")
        };
        await userManager.AddClaimsAsync(user, claims);

        return Unit.Value;
    }
}
