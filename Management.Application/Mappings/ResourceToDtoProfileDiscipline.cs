using AutoMapper;
using Management.Domain.Domains.DTO.Discipline;
using Management.Infrastructure.Database.Entities;
using Management.Resource.Discipline;

namespace Management.Mappings;

public class ResourceToDtoProfileDiscipline : Profile
{
    public ResourceToDtoProfileDiscipline()
    {
        CreateMap<DisciplineResponseDTO, DisciplineResponseResource>().ReverseMap();
        CreateMap<DisciplineResponseDTO, DisciplineResponseEntity>().ReverseMap();
        CreateMap<DisciplineResponseDTO, DisciplineEntity>().ReverseMap();

        CreateMap<DisciplineDTO, DisciplineResource>().ReverseMap();
        CreateMap<DisciplineDTO, DisciplineResponseResource>().ReverseMap();
        CreateMap<DisciplineDTO, DisciplineEntity>().ReverseMap();

        CreateMap<DisciplineUpdateResource, DisciplineUpdateDTO>().ReverseMap();

        CreateMap<DisciplineCreateDTO, DisciplineCreateResource>().ReverseMap();
        CreateMap<DisciplineCreateDTO, DisciplineEntity>().ReverseMap();

        CreateMap<DisciplineUpdateAveragesResource, DisciplineUpdateAveragesDTO>().ReverseMap();
        CreateMap<DisciplineEntity, DisciplineResponseEntity>()
            .ForMember(dest => dest.Averages, opt => opt.Ignore());
    }
}