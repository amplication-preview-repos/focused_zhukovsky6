using FinancialAdviserCrm.Infrastructure;

namespace FinancialAdviserCrm.APIs;

public class AdvisersService : AdvisersServiceBase
{
    public AdvisersService(FinancialAdviserCrmDbContext context)
        : base(context) { }
}
