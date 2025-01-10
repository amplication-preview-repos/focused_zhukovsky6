using FinancialAdviserCrm.Infrastructure;

namespace FinancialAdviserCrm.APIs;

public class ClientsService : ClientsServiceBase
{
    public ClientsService(FinancialAdviserCrmDbContext context)
        : base(context) { }
}
