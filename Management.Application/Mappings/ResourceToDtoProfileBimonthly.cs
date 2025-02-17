using AutoMapper;
using Management.Domain.Domains.DTO.Bimonthly;
using Management.Infrastructure.Database.Entities.Bimonthly;
using Management.Resource.Bimonthly;

namespace Management.Mappings;

public class ResourceToDtoProfileBimonthly : Profile
{
    public ResourceToDtoProfileBimonthly()
    {
        CreateMap<BimontlhyResource, BimonthlyDTO>().ReverseMap();
        CreateMap<BimonthlyDTO, BimonthlyEntity>().ReverseMap();
        
        CreateMap<BimonthlyEntity, BimonthlyResponseEntity>()
            .ForMember(dest => dest.Disciplines, opt => opt.Ignore());
        
        CreateMap<BimonthlyResponseResource, BimonthlyResponseDTO>().ReverseMap();
        CreateMap<BimonthlyResponseDTO, BimonthlyResponseEntity>().ReverseMap();
        
        CreateMap<BimonthlyEntity, BimonthlyResponseEntity>().ReverseMap();
        
        CreateMap<BimonthlyDatesResource, BimonthlyDatesDTO>().ReverseMap();
        
        CreateMap<BimonthlyUpdateDisciplinesResource, BimonthlyUpdateDisciplinesDTO>().ReverseMap();

        CreateMap<BimonthlyCreateDTO, BimonthlyCreateResource>().ReverseMap();
        CreateMap<BimonthlyCreateDTO, BimonthlyEntity>()
            .ForMember(dest => dest.DisciplinesId, opt => opt.Ignore());
    }
}