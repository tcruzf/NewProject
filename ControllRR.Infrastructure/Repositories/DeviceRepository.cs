
using ControllRR.Infrastructure.Data.Context;

using ControllRR.Domain.Entities;

//using ControlRR.Services.Exceptions;
//using ControlRR.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControllRR.Infrastructure.Exceptions;
using ControllRR.Domain.Interfaces;

namespace ControllRR.Infrastructure.Repositories;
public class DeviceRepository : IDeviceRepository
{
    private readonly ControllRRContext _controllRRContext;

    public DeviceRepository(ControllRRContext controllRRContext)
    {
        _controllRRContext = controllRRContext;
    }
    public async Task<List<Device>> FindAllAsync()
    {
        return await _controllRRContext.Devices
        //.Include(x => x.Sector)
        //.Include(x => x.Maintenances)
        .ToListAsync();
    }

    public async Task<Device> FindByIdAsync(int id)
    {
        return await _controllRRContext.Devices
        //.Include(x => x.Sector)
        .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Device> GetMaintenancesAsync(int id)
    {
        return await _controllRRContext.Devices
        //.Include(x => x.Maintenances)
        //.Include(x => x.Sector)
        .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task InsertAsync(Device device)
    {
        await _controllRRContext.AddAsync(device);
        await _controllRRContext.SaveChangesAsync();

    }

    public async Task UpdateAsync(Device device)
    {
        bool existDevice = await _controllRRContext.Devices.AnyAsync(x => x.Id == device.Id);

        if (!existDevice)
        {
            throw new NotFoundException("Objeto não encontrado");

        }
        try
        {
            _controllRRContext.Devices.Update(device);
            await _controllRRContext.SaveChangesAsync();
        }
        catch (DbConcurrencyException e)
        {
            throw new DbConcurrencyException(e.Message);
        }
    }

    public async Task SaveChangesAsync()
    {
        await _controllRRContext.SaveChangesAsync();
    }


}