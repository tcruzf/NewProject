
using ControllRR.Infrastructure.Data.Context;

using ControllRR.Domain.Entities;

//using ControlRR.Services.Exceptions;
//using ControlRR.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControllRR.Infrastructure.Exceptions;
using ControllRR.Domain.Interfaces;
public class SectorRepository : ISectorRepository
{
    private readonly ControllRRContext _controllRRContext;
    public SectorRepository(ControllRRContext controllRRContext)
    {
        _controllRRContext = controllRRContext;
    }

    public async Task<List<Sector>> FindAllAsync()
    {
        return await _controllRRContext.Sectors
        //.Include(x => x.Devices)
        .ToListAsync();
    }

    public async Task InsertAsync(Sector sector)
    {
        _controllRRContext.Add(sector);
        await _controllRRContext.SaveChangesAsync();
    }

    public async Task<Sector> FindByIdAsync(int id)
    {
        return await _controllRRContext.Sectors
        //.Include(x => x.Devices)
        .FirstOrDefaultAsync(x => x.Id == id);
    }

     public async Task SaveChangesAsync()
    {
        await _controllRRContext.SaveChangesAsync();
    }

  
}