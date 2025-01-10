using FinancialAdviserCrm.APIs.Common;
using FinancialAdviserCrm.APIs.Dtos;

namespace FinancialAdviserCrm.APIs;

public interface IAdvisersService
{
    /// <summary>
    /// Create one Adviser
    /// </summary>
    public Task<Adviser> CreateAdviser(AdviserCreateInput adviser);

    /// <summary>
    /// Delete one Adviser
    /// </summary>
    public Task DeleteAdviser(AdviserWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Advisers
    /// </summary>
    public Task<List<Adviser>> Advisers(AdviserFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Adviser records
    /// </summary>
    public Task<MetadataDto> AdvisersMeta(AdviserFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Adviser
    /// </summary>
    public Task<Adviser> Adviser(AdviserWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Adviser
    /// </summary>
    public Task UpdateAdviser(AdviserWhereUniqueInput uniqueId, AdviserUpdateInput updateDto);
}
