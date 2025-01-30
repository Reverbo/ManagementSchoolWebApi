using AutoMapper;
using Management.Domain.Domains.DTO.Classroom;
using Management.Infrasctructure.Database.Entities;
using Management.Infrastructure.Database.Entities;
using Management.Resource.Classroom;

namespace Management.Mappings;

public class ResourceToDtoProfileClassroom : Profile
{
    public ResourceToDtoProfileClassroom()
    {
        CreateMap<ClassroomEntity, ClassroomResponseEntity>()
            .ForMember(dest => dest.Students, opt => opt.Ignore());
        
        CreateMap<ClassroomResource, ClassroomDTO>().ReverseMap();
        CreateMap<ClassroomDTO, ClassroomEntity>().ReverseMap();
        
        CreateMap<ClassroomResponseResource, ClassroomResponseDTO>().ReverseMap();
        CreateMap<ClassroomResponseDTO, ClassroomResponseEntity>().ReverseMap();
        CreateMap<ClassroomEntity, ClassroomResponseDTO>().ReverseMap();
        CreateMap<ClassroomDTO, ClassroomResponseDTO>().ReverseMap();
        CreateMap<ClassroomUpdateDTO, ClassroomDTO>().ReverseMap();
        CreateMap<ClassroomUpdateDTO, ClassroomResponseResource>().ReverseMap();
    }
}