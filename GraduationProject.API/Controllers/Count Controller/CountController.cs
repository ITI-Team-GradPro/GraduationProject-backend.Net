using GraduationProject.BL.Dtos;
using GraduationProject.BL.Managers;
using GraduationProject.Data.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProject.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CountController : ControllerBase
{
   
    private readonly ICountManager _countManager;

    public CountController(ICountManager countManager)
    { 
        _countManager = countManager;
    }

    [HttpGet("counts")]
    public async Task<ActionResult<CountDto>> GetCounts()
    {
        try
        {
            var counts = await _countManager.GetAllData();
            return Ok(counts);
        }
        catch (Exception)
        {
            return StatusCode(500, "Can't get data");
        }
    }
}

