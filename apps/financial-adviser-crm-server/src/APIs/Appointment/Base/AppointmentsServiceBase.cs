using FinancialAdviserCrm.APIs;
using FinancialAdviserCrm.APIs.Common;
using FinancialAdviserCrm.APIs.Dtos;
using FinancialAdviserCrm.APIs.Errors;
using FinancialAdviserCrm.APIs.Extensions;
using FinancialAdviserCrm.Infrastructure;
using FinancialAdviserCrm.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancialAdviserCrm.APIs;

public abstract class AppointmentsServiceBase : IAppointmentsService
{
    protected readonly FinancialAdviserCrmDbContext _context;

    public AppointmentsServiceBase(FinancialAdviserCrmDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Appointment
    /// </summary>
    public async Task<Appointment> CreateAppointment(AppointmentCreateInput createDto)
    {
        var appointment = new AppointmentDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            appointment.Id = createDto.Id;
        }

        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<AppointmentDbModel>(appointment.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Appointment
    /// </summary>
    public async Task DeleteAppointment(AppointmentWhereUniqueInput uniqueId)
    {
        var appointment = await _context.Appointments.FindAsync(uniqueId.Id);
        if (appointment == null)
        {
            throw new NotFoundException();
        }

        _context.Appointments.Remove(appointment);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Appointments
    /// </summary>
    public async Task<List<Appointment>> Appointments(AppointmentFindManyArgs findManyArgs)
    {
        var appointments = await _context
            .Appointments.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return appointments.ConvertAll(appointment => appointment.ToDto());
    }

    /// <summary>
    /// Meta data about Appointment records
    /// </summary>
    public async Task<MetadataDto> AppointmentsMeta(AppointmentFindManyArgs findManyArgs)
    {
        var count = await _context.Appointments.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Appointment
    /// </summary>
    public async Task<Appointment> Appointment(AppointmentWhereUniqueInput uniqueId)
    {
        var appointments = await this.Appointments(
            new AppointmentFindManyArgs { Where = new AppointmentWhereInput { Id = uniqueId.Id } }
        );
        var appointment = appointments.FirstOrDefault();
        if (appointment == null)
        {
            throw new NotFoundException();
        }

        return appointment;
    }

    /// <summary>
    /// Update one Appointment
    /// </summary>
    public async Task UpdateAppointment(
        AppointmentWhereUniqueInput uniqueId,
        AppointmentUpdateInput updateDto
    )
    {
        var appointment = updateDto.ToModel(uniqueId);

        _context.Entry(appointment).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Appointments.Any(e => e.Id == appointment.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }
}
