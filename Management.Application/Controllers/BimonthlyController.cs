using AutoMapper;
using Management.Domain.Domains.DTO.Bimonthly;
using Management.Domain.Domains.Exceptions;
using Management.Domain.UseCases.Bimonthly;
using Management.Resource.Bimonthly;
using Microsoft.AspNetCore.Mvc;

namespace Management.Controllers;

[Route("api/bimonthly")]
[ApiController]
public class BimonthlyController : ControllerBase
{
    private readonly IBimonthlyCrudUseCase _bimonthlyCrudUseCase;
    private readonly IMapper _mapper;

    public BimonthlyController(IMapper mapper, IBimonthlyCrudUseCase bimonthlyCrudUseCase)
    {
        _bimonthlyCrudUseCase = bimonthlyCrudUseCase;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> Create(BimonthlyCreateResource bimonthlyRequest)
    {
        try
        {
            var bimonthlyDto = _mapper.Map<BimonthlyCreateDTO>(bimonthlyRequest);
            var bimonthly = await _bimonthlyCrudUseCase.Create(bimonthlyDto);
            var response = _mapper.Map<BimonthlyResponseResource>(bimonthly);
            return StatusCode(201, response);
        }
        catch (BaseManagementSchoolException exception)
        {
            return StatusCode(exception.StatusCode, exception.Message);
        }
    }

    [HttpPut("update/{bimonthlyId}")]
    public async Task<IActionResult> Update(BimonthlyDatesResource bimonthlyRequest, string bimonthlyId)
    {
        try
        {
            var bimonthlyUpdateDto = _mapper.Map<BimonthlyDatesDTO>(bimonthlyRequest);
            var bimonthly = await _bimonthlyCrudUseCase.Update(bimonthlyUpdateDto, bimonthlyId);
            var response = _mapper.Map<BimonthlyResponseResource>(bimonthly);
            return StatusCode(200, response);
        }
        catch (BaseManagementSchoolException exception)
        {
            return StatusCode(exception.StatusCode, exception.Message);
        }
    }

    [HttpPut("addDisciplines/{bimonthlyId}")]
    public async Task<IActionResult> AddDisciplines(BimonthlyUpdateDisciplinesResource bimonthlyRequest,
        string bimonthlyId)
    {
        try
        {
            var bimonthlyUpdateDto = _mapper.Map<BimonthlyUpdateDisciplinesDTO>(bimonthlyRequest);
            var bimonthly = await _bimonthlyCrudUseCase.AddDisciplines(bimonthlyUpdateDto, bimonthlyId);
            var bimonthlyResponse = _mapper.Map<BimonthlyResponseResource>(bimonthly);
            var messageResponse =
                $"Foram adicionados ao bimestre do ID: {bimonthlyResponse.Id} as disciplinas de ID: {string.Join(", ", bimonthlyUpdateDto.DisciplinesId)}";
            return StatusCode(200, messageResponse);
        }
        catch (BaseManagementSchoolException exception)
        {
            return StatusCode(exception.StatusCode, exception.Message);
        }
    }

    [HttpPut("removeDisciplines/{bimonthlyId}")]
    public async Task<IActionResult> RemoveDisciplines(BimonthlyUpdateDisciplinesResource bimonthlyRequest,
        string bimonthlyId)
    {
        try
        {
            var bimonthlyUpdateDto = _mapper.Map<BimonthlyUpdateDisciplinesDTO>(bimonthlyRequest);
            var bimonthly = await _bimonthlyCrudUseCase.RemoveDisciplines(bimonthlyUpdateDto, bimonthlyId);
            var bimonthlyResponse = _mapper.Map<BimonthlyResponseResource>(bimonthly);
            var messageResponse =
                $"Foram adicionados ao bimestre do ID: {bimonthlyResponse.Id} as disciplinas de ID: {string.Join(", ", bimonthlyUpdateDto.DisciplinesId)}";
            return StatusCode(200, messageResponse);
        }
        catch (BaseManagementSchoolException exception)
        {
            return StatusCode(exception.StatusCode, exception.Message);
        }
    }


    [HttpDelete("{bimonthlyId}")]
    public async Task<IActionResult> Delete(string bimonthlyId)
    {
        try
        {
            await _bimonthlyCrudUseCase.Delete(bimonthlyId);
            return StatusCode(204);
        }
        catch (BaseManagementSchoolException exception)
        {
            return StatusCode(exception.StatusCode, exception.Message);
        }
    }

    [HttpGet("id/{bimonthlyId}")]
    public async Task<IActionResult> GetById(string bimonthlyId)
    {
        try
        {
            var bimonthly = await _bimonthlyCrudUseCase.GetById(bimonthlyId);
            var response = _mapper.Map<BimonthlyResponseResource>(bimonthly);
            return StatusCode(200, response);
        }
        catch (BaseManagementSchoolException exception)
        {
            return StatusCode(exception.StatusCode, exception.Message);
        }
    }
    
    [HttpGet("date")]
    public async Task<IActionResult> GetByDate([FromQuery] BimonthlyDatesResource bimonthlyRequest)
    {
        try
        {
            var bimonthlyRequestDto = _mapper.Map<BimonthlyDatesDTO>(bimonthlyRequest);
            var bimonthlyList = await _bimonthlyCrudUseCase.GetByDate(bimonthlyRequestDto);
            var response = _mapper.Map<List<BimonthlyResponseResource>>(bimonthlyList).ToList();
            return StatusCode(200, response);
        }
        catch (BaseManagementSchoolException exception)
        {
            return StatusCode(exception.StatusCode, exception.Message);
        }
    }
}