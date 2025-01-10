using FinancialAdviserCrm.APIs;
using FinancialAdviserCrm.APIs.Common;
using FinancialAdviserCrm.APIs.Dtos;
using FinancialAdviserCrm.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace FinancialAdviserCrm.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class FinancialProductsControllerBase : ControllerBase
{
    protected readonly IFinancialProductsService _service;

    public FinancialProductsControllerBase(IFinancialProductsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one FinancialProduct
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<FinancialProduct>> CreateFinancialProduct(
        FinancialProductCreateInput input
    )
    {
        var financialProduct = await _service.CreateFinancialProduct(input);

        return CreatedAtAction(
            nameof(FinancialProduct),
            new { id = financialProduct.Id },
            financialProduct
        );
    }

    /// <summary>
    /// Delete one FinancialProduct
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteFinancialProduct(
        [FromRoute()] FinancialProductWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeleteFinancialProduct(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many FinancialProducts
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<FinancialProduct>>> FinancialProducts(
        [FromQuery()] FinancialProductFindManyArgs filter
    )
    {
        return Ok(await _service.FinancialProducts(filter));
    }

    /// <summary>
    /// Meta data about FinancialProduct records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> FinancialProductsMeta(
        [FromQuery()] FinancialProductFindManyArgs filter
    )
    {
        return Ok(await _service.FinancialProductsMeta(filter));
    }

    /// <summary>
    /// Get one FinancialProduct
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<FinancialProduct>> FinancialProduct(
        [FromRoute()] FinancialProductWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.FinancialProduct(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one FinancialProduct
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateFinancialProduct(
        [FromRoute()] FinancialProductWhereUniqueInput uniqueId,
        [FromQuery()] FinancialProductUpdateInput financialProductUpdateDto
    )
    {
        try
        {
            await _service.UpdateFinancialProduct(uniqueId, financialProductUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
