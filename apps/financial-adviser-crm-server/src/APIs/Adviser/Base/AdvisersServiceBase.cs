using FinancialAdviserCrm.APIs;
using FinancialAdviserCrm.APIs.Common;
using FinancialAdviserCrm.APIs.Dtos;
using FinancialAdviserCrm.APIs.Errors;
using FinancialAdviserCrm.APIs.Extensions;
using FinancialAdviserCrm.Infrastructure;
using FinancialAdviserCrm.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancialAdviserCrm.APIs;

public abstract class AdvisersServiceBase : IAdvisersService
{
    protected readonly FinancialAdviserCrmDbContext _context;

    public AdvisersServiceBase(FinancialAdviserCrmDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Adviser
    /// </summary>
    public async Task<Adviser> CreateAdviser(AdviserCreateInput createDto)
    {
        var adviser = new AdviserDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            adviser.Id = createDto.Id;
        }

        _context.Advisers.Add(adviser);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<AdviserDbModel>(adviser.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Adviser
    /// </summary>
    public async Task DeleteAdviser(AdviserWhereUniqueInput uniqueId)
    {
        var adviser = await _context.Advisers.FindAsync(uniqueId.Id);
        if (adviser == null)
        {
            throw new NotFoundException();
        }

        _context.Advisers.Remove(adviser);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Advisers
    /// </summary>
    public async Task<List<Adviser>> Advisers(AdviserFindManyArgs findManyArgs)
    {
        var advisers = await _context
            .Advisers.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return advisers.ConvertAll(adviser => adviser.ToDto());
    }

    /// <summary>
    /// Meta data about Adviser records
    /// </summary>
    public async Task<MetadataDto> AdvisersMeta(AdviserFindManyArgs findManyArgs)
    {
        var count = await _context.Advisers.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Adviser
    /// </summary>
    public async Task<Adviser> Adviser(AdviserWhereUniqueInput uniqueId)
    {
        var advisers = await this.Advisers(
            new AdviserFindManyArgs { Where = new AdviserWhereInput { Id = uniqueId.Id } }
        );
        var adviser = advisers.FirstOrDefault();
        if (adviser == null)
        {
            throw new NotFoundException();
        }

        return adviser;
    }

    /// <summary>
    /// Update one Adviser
    /// </summary>
    public async Task UpdateAdviser(AdviserWhereUniqueInput uniqueId, AdviserUpdateInput updateDto)
    {
        var adviser = updateDto.ToModel(uniqueId);

        _context.Entry(adviser).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Advisers.Any(e => e.Id == adviser.Id))
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
