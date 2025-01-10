using FinancialAdviserCrm.APIs.Dtos;
using FinancialAdviserCrm.Infrastructure.Models;

namespace FinancialAdviserCrm.APIs.Extensions;

public static class FinancialProductsExtensions
{
    public static FinancialProduct ToDto(this FinancialProductDbModel model)
    {
        return new FinancialProduct
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static FinancialProductDbModel ToModel(
        this FinancialProductUpdateInput updateDto,
        FinancialProductWhereUniqueInput uniqueId
    )
    {
        var financialProduct = new FinancialProductDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            financialProduct.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            financialProduct.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return financialProduct;
    }
}
