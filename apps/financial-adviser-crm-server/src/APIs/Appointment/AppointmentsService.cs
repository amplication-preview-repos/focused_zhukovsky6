using FinancialAdviserCrm.Infrastructure;

namespace FinancialAdviserCrm.APIs;

public class AppointmentsService : AppointmentsServiceBase
{
    public AppointmentsService(FinancialAdviserCrmDbContext context)
        : base(context) { }
}
