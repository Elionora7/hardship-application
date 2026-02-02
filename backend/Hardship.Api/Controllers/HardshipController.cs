using Hardship.Api.Data.Repositories;
using Hardship.Api.Models.Domain;
using Hardship.Api.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Hardship.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HardshipController : ControllerBase
{
    private readonly HardshipApplicationRepository _repository;

    public HardshipController(HardshipApplicationRepository repository)
    {
        _repository = repository;
    }

    // GET /api/hardship
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var list = await _repository.GetAllAsync();
        return Ok(list);
    }

    // POST /api/hardship
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateHardshipApplicationRequest request)
    {
        var application = new HardshipApplication
        {
            CustomerName = request.CustomerName,
            DateOfBirth = request.DateOfBirth,
            Income = request.Income,
            Expenses = request.Expenses,
            HardshipReason = request.HardshipReason,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var id = await _repository.CreateAsync(application);
        application.Id = id;

        return CreatedAtAction(nameof(GetById), new { id = id }, application);
    }

    // GET /api/hardship/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var app = await _repository.GetByIdAsync(id);
        if (app == null)
            return NotFound();
        return Ok(app);
    }

    // PUT /api/hardship/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] UpdateHardshipApplicationRequest request)
    {
        if (id != request.Id)
            return BadRequest("Id mismatch");

        var existing = await _repository.GetByIdAsync(id);
        if (existing == null)
            return NotFound();

        existing.CustomerName = request.CustomerName;
        existing.DateOfBirth = request.DateOfBirth;
        existing.Income = request.Income;
        existing.Expenses = request.Expenses;
        existing.HardshipReason = request.HardshipReason;
        existing.UpdatedAt = DateTime.UtcNow;

        await _repository.UpdateAsync(existing);
        return NoContent();
    }

    // DELETE /api/hardship/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _repository.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }

    // // GET /api/hardship/throw
    // [HttpGet("throw")]
    // public IActionResult Throw()
    // {
    //     throw new Exception("Test exception for middleware");
    // }

}
