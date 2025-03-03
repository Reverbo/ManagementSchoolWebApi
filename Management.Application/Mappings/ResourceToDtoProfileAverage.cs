using AutoMapper;
using Management.Domain.Domains.DTO.Average;
using Management.Infrastructure.Database.Entities;
using Management.Resource.Average;

namespace Management.Mappings;

public class ResourceToDtoProfileAverage : Profile
{
    public ResourceToDtoProfileAverage()
    {
        CreateMap<AverageResource, AverageDTO>().ReverseMap();
        CreateMap<AverageCreateResource , AverageCreateDTO>().ReverseMap();
        CreateMap<AverageDTO, AverageEntity>().ReverseMap();
        CreateMap<AverageCreateDTO, AverageEntity>().ReverseMap();
        
        CreateMap<ScoresResource, ScoresDTO>().ReverseMap();
        CreateMap<ScoresDTO, ScoresEntity>().ReverseMap();
    }
}