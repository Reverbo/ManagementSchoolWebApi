using AutoMapper;
using Management.Domain.Domains.DTO.Students;
using Management.Domain.Domains.Exceptions;
using Management.Domain.UseCases.Students;
using Management.Resource.Student;
using Microsoft.AspNetCore.Mvc;


namespace Management.Controllers;

[Route("api/student")]
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
    public async Task<IActionResult> Create(StudentDTO studentRequest)
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
    public async Task<IActionResult> Update(StudentDTO studentRequest, string studentId)
    { 
        try
        {
           var studentDto = _mapper.Map<StudentDTO>(studentRequest);
           var updateStudent = await _studentCrudUseCase.Update(studentDto, studentId);
           var response =_mapper.Map<StudentResource>(updateStudent);
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
            var student = await _studentCrudUseCase.Delete(studentId);  
            var response =_mapper.Map<StudentResource>(student);
            return StatusCode(204, response);
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
            var student = await _studentCrudUseCase.GetAll();
            var response = _mapper.Map<List<StudentResource>>(student);
            return StatusCode(200, response);
        }
        catch (StudentException exception)
        {
            return StatusCode(exception.StatusCode, exception.Message);
        }
    }
    
    [HttpGet("{studentId}")] 
    public async Task<IActionResult> GetById(string studentId)
    {
        try
        {
            var studentGetCreateDto = await _studentCrudUseCase.GetById(studentId);
            var response = _mapper.Map<StudentResource>(studentGetCreateDto);
            return StatusCode(200, response);
        }
        catch (StudentException exception)
        {
            return StatusCode(exception.StatusCode, exception.Message);
        }
    } 
    }
