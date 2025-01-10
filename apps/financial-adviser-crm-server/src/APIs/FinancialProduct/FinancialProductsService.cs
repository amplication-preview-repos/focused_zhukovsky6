using FinancialAdviserCrm.Infrastructure;

namespace FinancialAdviserCrm.APIs;

public class FinancialProductsService : FinancialProductsServiceBase
{
    public FinancialProductsService(FinancialAdviserCrmDbContext context)
        : base(context) { }
}
