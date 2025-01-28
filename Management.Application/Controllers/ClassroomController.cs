using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using Management.Domain.Domains.DTO.Classroom;
using Management.Domain.Domains.DTO.Students;
using Management.Domain.Domains.Exceptions;
using Management.Domain.UseCases.Classroom;
using Management.Infrasctructure.Database.Entities;
using Management.Resource.Classroom;
using Management.Resource.Student;
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
    public async Task<IActionResult> Create(ClassroomDTO request)
    {
        try
        {
            var classroomCreateDto = _mapper.Map<ClassroomDTO>(request);
            var classroom = await _classroomCrudUseCases.Create(classroomCreateDto);
            var response = _mapper.Map<ClassroomResponseResource>(classroom);
            return StatusCode(201, response);
        }
        catch (ClassroomException exception)
        {
            return StatusCode(exception.StatusCode, exception.Message);
        }
    }
       
    [HttpPut("upateClass/{classroomId}")]
    public async Task<IActionResult> Update(ClassroomDTO classroomRequest , string classroomId)
    {
        try
        {
            var classroomUpdateDto = _mapper.Map<ClassroomDTO>(classroomRequest);
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
    public async Task<IActionResult> AddStudent(ClassroomUpdateDTO classroomRequest, string classroomId)
    {
        try
        {
           var classroomUpdateDto  = _mapper.Map<ClassroomDTO>(classroomRequest);
           var classroom = await _classroomCrudUseCases.AddStudent(classroomUpdateDto, classroomId);
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
    public async Task<IActionResult> RemoveStudent(ClassroomUpdateDTO classroomRequest, string classroomId)
    {
        try
        {
          var classroomUpdateDto = _mapper.Map<ClassroomDTO>(classroomRequest);
          var classroom = await _classroomCrudUseCases.RemoveStudent(classroomUpdateDto, classroomId);
          var classroomResponse = _mapper.Map<ClassroomResponseResource>(classroom);
          var response =
              $"Foram removidos da turma do ID: {classroomResponse.Id} os seguintes estudantes de ID: {string.Join(", ", classroomUpdateDto.StudentsId)}";
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
            var classroom = await _classroomCrudUseCases.Delete(classroomId);
            var response = _mapper.Map<ClassroomResponseResource>(classroom);

            return StatusCode(204, response);
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
    
