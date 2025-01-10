using Microsoft.AspNetCore.Mvc;

namespace FinancialAdviserCrm.APIs;

[ApiController()]
public class FinancialProductsController : FinancialProductsControllerBase
{
    public FinancialProductsController(IFinancialProductsService service)
        : base(service) { }
}
