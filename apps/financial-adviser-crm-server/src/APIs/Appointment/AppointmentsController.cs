using Microsoft.AspNetCore.Mvc;

namespace FinancialAdviserCrm.APIs;

[ApiController()]
public class AppointmentsController : AppointmentsControllerBase
{
    public AppointmentsController(IAppointmentsService service)
        : base(service) { }
}
