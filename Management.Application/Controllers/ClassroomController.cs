using AutoMapper;
using Management.Domain.Domains.DTO.Classroom;
using Management.Domain.Domains.Exceptions;
using Management.Domain.UseCases.Classroom;
using Management.Resource.Classroom;
using Microsoft.AspNetCore.Mvc;

namespace Management.Controllers;

[Route("api/classroom")]
[ApiController]
public class ClassroomController : ControllerBase
{
    private readonly IClassroomCrudUseCases _classroomCrudUseCases;
    private readonly IMapper _mapper;

    public ClassroomController(IClassroomCrudUseCases crudUseCases, IMapper mapper)
    {
        _classroomCrudUseCases = crudUseCases;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> Create(ClassroomResource classroomRequest)
    {
        try
        {
            var classroomCreateDto = _mapper.Map<ClassroomDTO>(classroomRequest);
            var classroom = await _classroomCrudUseCases.Create(classroomCreateDto);
            var response = _mapper.Map<ClassroomResponseResource>(classroom);
            return StatusCode(201, response);
        }
        catch (ClassroomException exception)
        {
            return StatusCode(exception.StatusCode, exception.Message);
        }
    }

    [HttpPut("updateClass/{classroomId}")]
    public async Task<IActionResult> Update(ClassroomUpdateResource classroomRequest, string classroomId)
    {
        try
        {
            var classroomUpdateDto = _mapper.Map<ClassroomUpdateDTO>(classroomRequest);
            var classroom = await _classroomCrudUseCases.Update(classroomUpdateDto, classroomId);
            var response = _mapper.Map<ClassroomResponseResource>(classroom);
            return StatusCode(200, response);
        }
        catch (ClassroomException exception)
        {
            return StatusCode(exception.StatusCode, exception.Message);
        }
    }


    [HttpPut("addStudent/{classroomId}")]
    public async Task<IActionResult> AddStudents(ClassroomUpdateStudentsResource classroomRequest, string classroomId)
    {
        try
        {
            var classroomUpdateDto = _mapper.Map<ClassroomUpdateStudentsDTO>(classroomRequest);
            var classroom = await _classroomCrudUseCases.AddStudents(classroomUpdateDto, classroomId);
            var classroomResponse = _mapper.Map<ClassroomResponseResource>(classroom);
            var response =
                $"Foram adicionados a turma do ID: {classroomResponse.Id} os seguintes estudantes de ID: {string.Join(", ", classroomUpdateDto.StudentsId)}";
            return StatusCode(200, response);
        }
        catch (ClassroomException exception)
        {
            return StatusCode(exception.StatusCode, exception.Message);
        }
    }

    [HttpPut("removeStudent/{classroomId}")]
    public async Task<IActionResult> RemoveStudents(ClassroomUpdateStudentsResource classroomRequest, string classroomId)
    {
        try
        {
            var classroomUpdateDto = _mapper.Map<ClassroomUpdateStudentsDTO>(classroomRequest);
            var classroom = await _classroomCrudUseCases.RemoveStudents(classroomUpdateDto, classroomId);
            var classroomResponse = _mapper.Map<ClassroomResponseResource>(classroom);
            var response =
                $"Foram removidos da turma do ID: {classroomId} os seguintes estudantes de ID: {string.Join(", ", classroomRequest.StudentsId)}";
            return StatusCode(200, response);
        }
        catch (ClassroomException exception)
        {
            return StatusCode(exception.StatusCode, exception.Message);
        }
    }

    [HttpDelete("{classroomId}")]
    public async Task<IActionResult> Delete(string classroomId)
    {
        try
        {
            await _classroomCrudUseCases.Delete(classroomId);
            return StatusCode(204);
        }
        catch (ClassroomException exception)
        {
            return StatusCode(exception.StatusCode, exception.Message);
        }
    }

    [HttpGet("id/{classroomId}")]
    public async Task<IActionResult> GetById(string classroomId)
    {
        try
        {
            var classroom = await _classroomCrudUseCases.GetById(classroomId);
            var response = _mapper.Map<ClassroomResponseResource>(classroom);
            return StatusCode(200, response);
        }
        catch (ClassroomException exception)
        {
            return StatusCode(exception.StatusCode, exception.Message);
        }
    }

    [HttpGet("name/{classroomName}")]
    public async Task<IActionResult> GetByName(string classroomName)
    {
        try
        {
            var classroom = await _classroomCrudUseCases.GetByName(classroomName);
            var response = _mapper.Map<ClassroomResponseResource>(classroom);
            return StatusCode(200, response);
        }
        catch (ClassroomException exception)
        {
            return StatusCode(exception.StatusCode, exception.Message);
        }
    }
}