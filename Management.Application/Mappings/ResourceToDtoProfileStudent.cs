using AutoMapper;
using Management.Domain.Domains.DTO.Students;
using Management.Infrasctructure.Database.Entities;
using Management.Resource.Student;

namespace Management.Mappings;

public class ResourceToDtoProfileStudent: Profile
{
    public ResourceToDtoProfileStudent()
    {
        CreateMap<StudentResource, StudentDTO>().ReverseMap();
        CreateMap<StudentDTO, StudentEntity>().ReverseMap();
    }
}