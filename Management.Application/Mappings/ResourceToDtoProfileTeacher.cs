using AutoMapper;
using Management.Domain.Domains.DTO.Teachers;
using Management.Infrastructure.Database.Entities.Teacher;
using Management.Resource.Teachers;

namespace Management.Mappings;

public class ResourceToDtoProfileTeacher : Profile
{
    public ResourceToDtoProfileTeacher()
    {
        CreateMap<TeacherResource, TeacherDTO>().ReverseMap();
        CreateMap<TeacherDTO, TeacherEntity>().ReverseMap();
        CreateMap<TeacherUpdateDTO, TeacherUpdateResource>().ReverseMap();
        CreateMap<TeacherUpdateDTO, TeacherEntity>().ReverseMap();
    }
}