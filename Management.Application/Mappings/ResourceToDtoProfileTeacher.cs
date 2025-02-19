using AutoMapper;
using Management.Domain.Domains.DTO.Teachers;
using Management.Infrasctructure.Database.Entities;
using Management.Resource.Teachers;

namespace Management.Mappings;

public class ResourceToDtoProfileTeacher : Profile
{
    public ResourceToDtoProfileTeacher()
    {
        CreateMap<TeacherResource, TeacherDTO>().ReverseMap();
        CreateMap<TeacherDTO, TeacherEntity>().ReverseMap();
        CreateMap<TeacherUpdateDTO, TeacherUpdateResource>().ReverseMap();
    }
}