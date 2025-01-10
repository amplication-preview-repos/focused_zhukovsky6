using FinancialAdviserCrm.APIs.Dtos;
using FinancialAdviserCrm.Infrastructure.Models;

namespace FinancialAdviserCrm.APIs.Extensions;

public static class AdvisersExtensions
{
    public static Adviser ToDto(this AdviserDbModel model)
    {
        return new Adviser
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static AdviserDbModel ToModel(
        this AdviserUpdateInput updateDto,
        AdviserWhereUniqueInput uniqueId
    )
    {
        var adviser = new AdviserDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            adviser.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            adviser.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return adviser;
    }
}
