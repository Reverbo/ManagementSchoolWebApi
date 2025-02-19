using AutoMapper;
using Management.Domain.Domains.DTO.Teachers;
using Management.Domain.Domains.Exceptions;
using Management.Domain.UseCases.Teachers;
using Management.Resource.Teachers;
using Microsoft.AspNetCore.Mvc;

namespace Management.Controllers;

[Route("api/teacher")]
[ApiController]
public class TeacherController : ControllerBase
{
    private readonly ITeacherCrudUseCase _teacherCrudUseCase;
    private readonly IMapper _mapper;

    public TeacherController(ITeacherCrudUseCase teacherCrudUseCase, IMapper mapper)
    {
        _mapper = mapper;
        _teacherCrudUseCase = teacherCrudUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> Create(TeacherResource teacherRequest)
    {
        try
        {
            var teacherRequestDto = _mapper.Map<TeacherDTO>(teacherRequest);
            var teacher = await _teacherCrudUseCase.Create(teacherRequestDto);
            var response = _mapper.Map<TeacherResource>(teacher);
            return StatusCode(201, response);
        }
        catch (BaseManagementSchoolException exception)
        {
            return StatusCode(exception.StatusCode, exception.Message);
        }
    }

    [HttpPut("{teacherId}")]
    public async Task<IActionResult> Update(TeacherUpdateResource teacherRequest, string teacherId)
    {
        try
        {
            var teacherRequestDto = _mapper.Map<TeacherUpdateDTO>(teacherRequest);
            var teacher = await _teacherCrudUseCase.Update(teacherRequestDto, teacherId);
            var response = _mapper.Map<TeacherResource>(teacher);
            return StatusCode(200, response);
        }
        catch (BaseManagementSchoolException exception)
        {
            return StatusCode(exception.StatusCode, exception.Message);
        }
    }

    [HttpDelete("{teacherId}")]
    public async Task<IActionResult> Delete(string teacherId)
    {
        try
        {
            await _teacherCrudUseCase.Delete(teacherId);
            return StatusCode(204);
        }
        catch (BaseManagementSchoolException exception)
        {
            return StatusCode(exception.StatusCode, exception.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var teacherList = await _teacherCrudUseCase.GetAll();
            var response = _mapper.Map<List<TeacherResource>>(teacherList);
            return StatusCode(200, response);
        }
        catch (BaseManagementSchoolException exception)
        {
            return StatusCode(exception.StatusCode, exception.Message);
        }
    }

    [HttpGet("id/{teacherId}")]
    public async Task<IActionResult> GetById(string teacherId)
    {
        try
        {
            var teacher = await _teacherCrudUseCase.GetById(teacherId);
            var response = _mapper.Map<TeacherResource>(teacher);
            return StatusCode(200, response);
        }
        catch (BaseManagementSchoolException exception)
        {
            return StatusCode(exception.StatusCode, exception.Message);
        }
    }
}