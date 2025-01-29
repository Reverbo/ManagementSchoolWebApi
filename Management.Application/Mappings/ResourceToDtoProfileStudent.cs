using AutoMapper;
using Management.Domain.Domains.DTO.Students;
using Management.Infrasctructure.Database.Entities;
using Management.Infrastructure.Database.Entities;
using Management.Resource.Student;

namespace Management.Mappings;

public class ResourceToDtoProfileStudents: Profile
{
    public ResourceToDtoProfileStudents()
    {
        CreateMap<StudentResource, StudentDTO>().ReverseMap();
        CreateMap<StudentDTO, StudentEntity>().ReverseMap(); 
    }
}