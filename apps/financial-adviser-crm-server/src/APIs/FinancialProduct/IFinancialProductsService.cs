using FinancialAdviserCrm.APIs.Common;
using FinancialAdviserCrm.APIs.Dtos;

namespace FinancialAdviserCrm.APIs;

public interface IFinancialProductsService
{
    /// <summary>
    /// Create one FinancialProduct
    /// </summary>
    public Task<FinancialProduct> CreateFinancialProduct(
        FinancialProductCreateInput financialproduct
    );

    /// <summary>
    /// Delete one FinancialProduct
    /// </summary>
    public Task DeleteFinancialProduct(FinancialProductWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many FinancialProducts
    /// </summary>
    public Task<List<FinancialProduct>> FinancialProducts(
        FinancialProductFindManyArgs findManyArgs
    );

    /// <summary>
    /// Meta data about FinancialProduct records
    /// </summary>
    public Task<MetadataDto> FinancialProductsMeta(FinancialProductFindManyArgs findManyArgs);

    /// <summary>
    /// Get one FinancialProduct
    /// </summary>
    public Task<FinancialProduct> FinancialProduct(FinancialProductWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one FinancialProduct
    /// </summary>
    public Task UpdateFinancialProduct(
        FinancialProductWhereUniqueInput uniqueId,
        FinancialProductUpdateInput updateDto
    );
}
