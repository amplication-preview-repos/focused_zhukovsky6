using FinancialAdviserCrm.APIs.Common;
using FinancialAdviserCrm.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinancialAdviserCrm.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class AppointmentFindManyArgs : FindManyInput<Appointment, AppointmentWhereInput> { }
