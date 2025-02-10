using AutoMapper;
using Management.Domain.Domains.DTO.Discipline;
using Management.Domain.Domains.Exceptions;
using Management.Domain.UseCases.Discipline;
using Management.Resource.Discipline;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Management.Controllers;

[Route("api/discipline")]
[ApiController]
public class DisciplineController : ControllerBase
{
    private readonly IDisciplineCrudUseCase _disciplineCrudUseCase;
    private readonly IMapper _mapper;

    public DisciplineController(IDisciplineCrudUseCase disciplineCrudUse, IMapper mapper)
    {
        _disciplineCrudUseCase = disciplineCrudUse;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> Create(DisciplineCreateResource disciplineRequest)
    {
        try
        {
            var disciplineRequestDto = _mapper.Map<DisciplineCreateDTO>(disciplineRequest);
            var discipline = await _disciplineCrudUseCase.Create(disciplineRequestDto);
            var response = _mapper.Map<DisciplineResponseResource>(discipline);
            return StatusCode(201, response);
        }
        catch (DisciplineException exception)
        {
            return StatusCode(exception.StatusCode, exception.Message);
        }
    }

    [HttpPut("updateDiscipline/{disciplineId}")]
    public async Task<IActionResult> Update(DisciplineEditResource disciplineRequest, string disciplineId)
    {
        try
        {
            var disciplineRequestDto = _mapper.Map<DisciplineEditDTO>(disciplineRequest);
            var discipline = await _disciplineCrudUseCase.Update(disciplineRequestDto, disciplineId);
            var response = _mapper.Map<DisciplineResponseResource>(discipline);
            return StatusCode(200, response);
        }
        catch (DisciplineException exception)
        {
            return StatusCode(exception.StatusCode, exception.Message);
        }
    }

    [HttpPut("addAverages/{disciplineId}")]
    public async Task<IActionResult> AddAverages(DisciplineUpdateResource disciplineRequest, string disciplineId)
    {
        try
        {
            var disciplineRequestDto = _mapper.Map<DisciplineUpdateDTO>(disciplineRequest);
            var discipline = await _disciplineCrudUseCase.AddAverages(disciplineRequestDto, disciplineId);
            var response = _mapper.Map<DisciplineResponseResource>(discipline);
            return StatusCode(200, response);
        }
        catch (DisciplineException exception)
        {
            return StatusCode(exception.StatusCode, exception.Message);
        }
    }

    [HttpPut("removeAverages/{disciplineId}")]
    public async Task<IActionResult> RemoveAverages(DisciplineUpdateResource disciplineRequest, string disciplineId)
    {
        try
        {
            var disciplineRequestDto = _mapper.Map<DisciplineUpdateDTO>(disciplineRequest);
            var discipline = await _disciplineCrudUseCase.RemoveAverages(disciplineRequestDto, disciplineId);
            var disciplineReponse = _mapper.Map<DisciplineResponseResource>(discipline);
            var response =
                $"Foram removidos da discipline do ID: {disciplineReponse.Id} as seguintes médias de ID: {string.Join(", ", disciplineRequestDto.AveragesId)}";
            return StatusCode(200, response);
        }
        catch (DisciplineException exception)
        {
            return StatusCode(exception.StatusCode, exception.Message);
        }
    }

    [HttpDelete("{disciplineId}")]
    public async Task<IActionResult> Delete(string disciplineId)
    {
        try
        {
            await _disciplineCrudUseCase.Delete(disciplineId);
            return StatusCode(204);
        }
        catch (DisciplineException exception)
        {
            return StatusCode(exception.StatusCode, exception.Message);
        }
    }

    [HttpGet("{disciplineId}")]
    public async Task<IActionResult> GetById(string disciplineId)
    {
        try
        {
            var discipline = await _disciplineCrudUseCase.GetById(disciplineId);
            var response = _mapper.Map<DisciplineResource>(discipline);
            return StatusCode(200, response);
        }
        catch (DisciplineException exception)
        {
            return StatusCode(exception.StatusCode, exception.Message);
        }
    }
}