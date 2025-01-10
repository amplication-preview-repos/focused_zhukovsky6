using FinancialAdviserCrm.APIs;
using FinancialAdviserCrm.APIs.Common;
using FinancialAdviserCrm.APIs.Dtos;
using FinancialAdviserCrm.APIs.Errors;
using FinancialAdviserCrm.APIs.Extensions;
using FinancialAdviserCrm.Infrastructure;
using FinancialAdviserCrm.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancialAdviserCrm.APIs;

public abstract class FinancialProductsServiceBase : IFinancialProductsService
{
    protected readonly FinancialAdviserCrmDbContext _context;

    public FinancialProductsServiceBase(FinancialAdviserCrmDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one FinancialProduct
    /// </summary>
    public async Task<FinancialProduct> CreateFinancialProduct(
        FinancialProductCreateInput createDto
    )
    {
        var financialProduct = new FinancialProductDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            financialProduct.Id = createDto.Id;
        }

        _context.FinancialProducts.Add(financialProduct);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<FinancialProductDbModel>(financialProduct.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one FinancialProduct
    /// </summary>
    public async Task DeleteFinancialProduct(FinancialProductWhereUniqueInput uniqueId)
    {
        var financialProduct = await _context.FinancialProducts.FindAsync(uniqueId.Id);
        if (financialProduct == null)
        {
            throw new NotFoundException();
        }

        _context.FinancialProducts.Remove(financialProduct);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many FinancialProducts
    /// </summary>
    public async Task<List<FinancialProduct>> FinancialProducts(
        FinancialProductFindManyArgs findManyArgs
    )
    {
        var financialProducts = await _context
            .FinancialProducts.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return financialProducts.ConvertAll(financialProduct => financialProduct.ToDto());
    }

    /// <summary>
    /// Meta data about FinancialProduct records
    /// </summary>
    public async Task<MetadataDto> FinancialProductsMeta(FinancialProductFindManyArgs findManyArgs)
    {
        var count = await _context.FinancialProducts.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one FinancialProduct
    /// </summary>
    public async Task<FinancialProduct> FinancialProduct(FinancialProductWhereUniqueInput uniqueId)
    {
        var financialProducts = await this.FinancialProducts(
            new FinancialProductFindManyArgs
            {
                Where = new FinancialProductWhereInput { Id = uniqueId.Id }
            }
        );
        var financialProduct = financialProducts.FirstOrDefault();
        if (financialProduct == null)
        {
            throw new NotFoundException();
        }

        return financialProduct;
    }

    /// <summary>
    /// Update one FinancialProduct
    /// </summary>
    public async Task UpdateFinancialProduct(
        FinancialProductWhereUniqueInput uniqueId,
        FinancialProductUpdateInput updateDto
    )
    {
        var financialProduct = updateDto.ToModel(uniqueId);

        _context.Entry(financialProduct).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.FinancialProducts.Any(e => e.Id == financialProduct.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }
}
