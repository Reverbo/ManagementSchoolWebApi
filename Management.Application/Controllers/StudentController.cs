using System.Diagnostics;
using System.Security.Claims;
using Management.Domain.Services;
using Management.Infrasctructure.Database.Entities;
using Management.Infrasctructure.Database.EntitiesConfiguration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;


namespace Management.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentController : ControllerBase
{
    private readonly StudentsServices _studentServices;

    public StudentController(StudentsServices studentServices)
    {
        _studentServices = studentServices;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Student newStudent)
    {
        await _studentServices.CreaterAsync(newStudent);

        return CreatedAtAction(nameof(Post), new { Name = newStudent.FirstName }, newStudent);
    }

    [HttpGet("diagnostic")]
    public IActionResult Diagnostic()
    {
        try
        {
            var client = new MongoClient("mongodb://root:example@mongo:27017/?authSource=admin");
            var database = client.GetDatabase("ManagementDatabase");
            var command = new BsonDocument("ping", 1);
            var result = database.RunCommand<BsonDocument>(command);
            return Ok(result.ToJson());
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");

        }
    }
}