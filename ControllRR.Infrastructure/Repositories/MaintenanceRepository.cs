
using ControllRR.Infrastructure.Data.Context;

using ControllRR.Domain.Entities;

//using ControlRR.Services.Exceptions;
//using ControlRR.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControllRR.Infrastructure.Exceptions;
using ControllRR.Domain.Interfaces;
using ControllRR.Domain.Enums;

public class MaintenanceRepository : IMaintenanceRepository
{
    private readonly ControllRRContext _controllRRContext;
    
    public MaintenanceRepository(ControllRRContext controllRRContext)
    {
        _controllRRContext = controllRRContext;
    }

    public async Task<List<Maintenance>> FindAllAsync()
    {

        return await _controllRRContext.Maintenances
        //.Include(x => x.User)
        .ToListAsync();
    }

    public async Task<Maintenance> FindByIdAsync(int id)
    {
        return await _controllRRContext.Maintenances
            //.Include(x => x.User)
           //.Include(x => x.Device)
           // .Include(x => x.Device.Sector)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
    
    public async Task InsertAsync(Maintenance maintenance)
    {
        var control = await _controllRRContext.MaintenanceNumberControls.FirstOrDefaultAsync();
        if(control == null)
        {
            control = new MaintenanceNumberControl { CurrentNumber = 99};
            _controllRRContext.MaintenanceNumberControls.Add(control);
            await _controllRRContext.SaveChangesAsync();
        }
        control.CurrentNumber += 1;
        maintenance.MaintenanceNumber = control.CurrentNumber;
        _controllRRContext.Update(control);
        _controllRRContext.Add(maintenance);
        await _controllRRContext.SaveChangesAsync();
    }

    public async Task RemoveAsync(int id)
    {
        var obj = await _controllRRContext.Maintenances.FindAsync(id);
        _controllRRContext.Remove(obj);
        await _controllRRContext.SaveChangesAsync();

    }

    public async Task UpdateAsync(Maintenance maintenance)
    {
        bool hasAny = await _controllRRContext.Maintenances.AnyAsync( x => x.Id == maintenance.Id);
        if(!hasAny)
        {
            throw new NotFoundException("Id Não encontrado!");
        }
        try{
           _controllRRContext.Update(maintenance);
           await _controllRRContext.SaveChangesAsync();

        }
        catch(DbConcurrencyException e)
        {
            throw new DbConcurrencyException(e.Message);
        }
    }

    public async Task FinalizeAsync(int id)
    {
        var maintenance = await _controllRRContext.Maintenances.FindAsync(id);
        if(maintenance == null)
        {
            throw new NotFoundException("Id não encontrado1");
        }
        var final = MaintenanceStatus.Finalizada;
        maintenance.Status = final;
        maintenance.CloseDate = DateTime.Now;
       
        _controllRRContext.Maintenances.Update(maintenance);
        await _controllRRContext.SaveChangesAsync();
    }

      public async Task SaveChangesAsync()
    {
        await _controllRRContext.SaveChangesAsync();
    }

}