using Microsoft.AspNetCore.Mvc;

namespace FinancialAdviserCrm.APIs;

[ApiController()]
public class AdvisersController : AdvisersControllerBase
{
    public AdvisersController(IAdvisersService service)
        : base(service) { }
}
