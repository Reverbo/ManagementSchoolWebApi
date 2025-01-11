using AutoMapper;
using Management.Domain.Domains.DTO.Teachers;
using Management.Domain.UseCases.Teachers;
using Management.Resource.Teachers;
using Microsoft.AspNetCore.Mvc;

namespace Management.Controllers;

public class TeacherController : ControllerBase
{
    private readonly ITeacherCrudUseCase _teacherCrudUseCase;
    private readonly IMapper _mapper;

    TeacherController(ITeacherCrudUseCase teacherCrudUseCase, IMapper mapper)
    {
        _mapper = mapper;
        _teacherCrudUseCase = teacherCrudUseCase;
    }


    [HttpPost]
    public async Task<IActionResult> Create(TeacherResource teacherRequest)
    {
        var teacherRequestDto = _mapper.Map<TeacherDTO>(teacherRequest);
        var teacher = await _teacherCrudUseCase.Create(teacherRequestDto);
        var response = _mapper.Map<TeacherResource>(teacher);
        return StatusCode(201, response);
    }
}