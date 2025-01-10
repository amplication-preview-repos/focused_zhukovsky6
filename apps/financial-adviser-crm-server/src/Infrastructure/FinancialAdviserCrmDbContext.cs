using FinancialAdviserCrm.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancialAdviserCrm.Infrastructure;

public class FinancialAdviserCrmDbContext : DbContext
{
    public FinancialAdviserCrmDbContext(DbContextOptions<FinancialAdviserCrmDbContext> options)
        : base(options) { }

    public DbSet<ClientDbModel> Clients { get; set; }

    public DbSet<AdviserDbModel> Advisers { get; set; }

    public DbSet<FinancialProductDbModel> FinancialProducts { get; set; }

    public DbSet<AppointmentDbModel> Appointments { get; set; }
}
