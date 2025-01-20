using AutoMapper;
using Management.Domain.Domains.DTO.Students;
using Management.Domain.Domains.Exceptions;
using Management.Domain.UseCases.Students;
using Management.Resource.Student;
using Microsoft.AspNetCore.Mvc;


namespace Management.Controllers;

[Route("api/student")]

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
        try
        {
            var studentCreateDto = _mapper.Map<StudentDTO>(studentRequest) ;
            var student = await _studentCrudUseCase.Create(studentCreateDto);
            var response = _mapper.Map<StudentResource>(student);
            return StatusCode(201, response);
        }
        catch (StudentException exception)
        {
           return StatusCode(exception.StatusCode, exception.Message);
        }

    }

    [HttpPut("{studentId}")]
    public async Task<IActionResult> Update(StudentResource studentRequest, string studentId)
    { 
        try
        {
           var studentCreateDto = _mapper.Map<StudentDTO>(studentRequest);
           var student = await _studentCrudUseCase.Update(studentCreateDto, studentId);
           var response =_mapper.Map<StudentResource>(student);
           return StatusCode(200, response);
        }
        catch (StudentException exception)
        {
            return StatusCode(exception.StatusCode, exception.Message);
        }
        
    }

    [HttpDelete("{studentId}")]
    public async Task<IActionResult> Delete(string studentId)
    {
        try
        { 
            await _studentCrudUseCase.Delete(studentId);  
            return StatusCode(204);
        }
        catch (StudentException exception)
        {
            return StatusCode(exception.StatusCode, exception.Message);
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        { 
            var studentList = await _studentCrudUseCase.GetAll();
            var response = _mapper.Map<List<StudentResource>>(studentList);
            return StatusCode(200, response);
        }
        catch (StudentException exception)
        {
            return StatusCode(exception.StatusCode, exception.Message);
        }
    }
    
    [HttpGet("id/{studentId}")] 
    public async Task<IActionResult> GetById(string studentId)
    {
        try
        {
            var student = await _studentCrudUseCase.GetById(studentId);
            var response = _mapper.Map<StudentResource>(student);
            return StatusCode(200, response);
        }
        catch (StudentException exception)
        {
            return StatusCode(exception.StatusCode, exception.Message);
        }
    } 
    }
