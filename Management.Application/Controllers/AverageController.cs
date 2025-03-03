using AutoMapper;
using Management.Domain.Domains.DTO.Average;
using Management.Domain.Domains.Exceptions;
using Management.Domain.UseCases.Average;
using Management.Resource.Average;
using Microsoft.AspNetCore.Mvc;

namespace Management.Controllers;

[Route("api/average")]
[ApiController]
public class AverageController : ControllerBase
{
    private readonly IAverageCrudUseCase _averageCrudUseCase;
    private readonly IMapper _mapper;

    public AverageController(IAverageCrudUseCase averageCrudUseCase, IMapper mapper)
    {
        _averageCrudUseCase = averageCrudUseCase;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> Create(AverageCreateResource averageRequest)
    {
        try
        {
            var averageDto = _mapper.Map<AverageCreateDTO>(averageRequest);
            var average = await _averageCrudUseCase.Create(averageDto);
            var response = _mapper.Map<AverageResource>(average);
            return StatusCode(201, response);
        }
        catch (BaseManagementSchoolException exception)
        {
            return StatusCode(exception.StatusCode, exception.Message);
        }
    }

    [HttpPut("{averageId}")]
    public async Task<IActionResult> Update(ScoresResource averageRequest, string averageId)
    {
        try
        {
            var scoreForAverageDto = _mapper.Map<ScoresDTO>(averageRequest);
            var average = await _averageCrudUseCase.Update(scoreForAverageDto, averageId);
            var response = _mapper.Map<AverageResource>(average);
            return StatusCode(200, response);
        }
        catch (BaseManagementSchoolException exception)
        {
            return StatusCode(exception.StatusCode, exception.Message);
        }
    }

    [HttpDelete("{averageId}")]
    public async Task<IActionResult> Delete(string averageId)
    {
        try
        {
            await _averageCrudUseCase.Delete(averageId);
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
            var averageList = await _averageCrudUseCase.GetAll();
            var response = _mapper.Map<List<AverageResource>>(averageList);
            return StatusCode(200, response);
        }
        catch (BaseManagementSchoolException exception)
        {
            return StatusCode(exception.StatusCode, exception.Message);
        }
    }

    [HttpGet("id/{averageId}")]
    public async Task<IActionResult> GetById(string averageId)
    {
        try
        {
            var average = await _averageCrudUseCase.GetById(averageId);
            var response = _mapper.Map<AverageResource>(average);
            return StatusCode(200, response);
        }
        catch (BaseManagementSchoolException exception)
        {
            return StatusCode(exception.StatusCode, exception.Message);
        }
    }
}