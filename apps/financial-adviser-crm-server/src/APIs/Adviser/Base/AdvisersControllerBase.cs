using FinancialAdviserCrm.APIs;
using FinancialAdviserCrm.APIs.Common;
using FinancialAdviserCrm.APIs.Dtos;
using FinancialAdviserCrm.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace FinancialAdviserCrm.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class AdvisersControllerBase : ControllerBase
{
    protected readonly IAdvisersService _service;

    public AdvisersControllerBase(IAdvisersService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Adviser
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Adviser>> CreateAdviser(AdviserCreateInput input)
    {
        var adviser = await _service.CreateAdviser(input);

        return CreatedAtAction(nameof(Adviser), new { id = adviser.Id }, adviser);
    }

    /// <summary>
    /// Delete one Adviser
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteAdviser([FromRoute()] AdviserWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteAdviser(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Advisers
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Adviser>>> Advisers(
        [FromQuery()] AdviserFindManyArgs filter
    )
    {
        return Ok(await _service.Advisers(filter));
    }

    /// <summary>
    /// Meta data about Adviser records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> AdvisersMeta(
        [FromQuery()] AdviserFindManyArgs filter
    )
    {
        return Ok(await _service.AdvisersMeta(filter));
    }

    /// <summary>
    /// Get one Adviser
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Adviser>> Adviser([FromRoute()] AdviserWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Adviser(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Adviser
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateAdviser(
        [FromRoute()] AdviserWhereUniqueInput uniqueId,
        [FromQuery()] AdviserUpdateInput adviserUpdateDto
    )
    {
        try
        {
            await _service.UpdateAdviser(uniqueId, adviserUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
