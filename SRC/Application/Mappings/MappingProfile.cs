using Mapster;
using TaskManager.Application.DTOs.Authentication;
using TaskManager.Application.DTOs.Project;
using TaskManager.Application.DTOs.Task;

namespace TaskManager.Application.Mappings;

public class MappingProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<SignUpDTO, ApplicationUser>()
            .Map(dest => dest.UserName, src => src.Username)
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.PhoneNumber, src => src.PhoneNumber);

        config.NewConfig<CreateProjectDTO, global::Project>()
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Description, src => src.Description);

        config.NewConfig<CreateTaskDTO, ProjectTask>();
    }
}