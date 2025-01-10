using Microsoft.AspNetCore.Mvc;

namespace FinancialAdviserCrm.APIs;

[ApiController()]
public class ClientsController : ClientsControllerBase
{
    public ClientsController(IClientsService service)
        : base(service) { }
}
