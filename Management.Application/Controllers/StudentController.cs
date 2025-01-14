using AutoMapper;
using Management.Domain.Domains.DTO.Students;
using Management.Domain.UseCases.Students;
using Management.Resource.Student;
using Microsoft.AspNetCore.Mvc;


namespace Management.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentController : ControllerBase
{
    private readonly IStudentCrudUseCase _studentCrudUseCase;
    private readonly IMapper _mapper;

    public StudentController(IStudentCrudUseCase studentServices, IMapper mapper)
    {
        _studentCrudUseCase = studentServices;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> Create(StudentResource studentRequest)
    {
        var studentRequestDto = _mapper.Map<StudentDTO>(studentRequest) ;
        var student = await _studentCrudUseCase.Create(studentRequestDto);
        var response = _mapper.Map<StudentResource>(student);
        return Ok(response);
    }
}