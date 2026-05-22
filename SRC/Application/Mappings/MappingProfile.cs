using Mapster;
using TaskManager.Application.DTOs.Authentication;

namespace TaskManager.Application.Mappings;

public class MappingProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<SignUpDTO, ApplicationUser>()
            .Map(dest => dest.UserName, src => src.Username)
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.PhoneNumber, src => src.PhoneNumber);
    }
}