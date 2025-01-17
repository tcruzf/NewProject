using ControllRR.Application.Interfaces;
using ControllRR.Domain.Entities;
using ControllRR.Domain.Interfaces;

namespace ControllRR.Application.Services;

public class SectorService : ISectorService
{
    private readonly ISectorRepository _sectorRepository;
    public SectorService(ISectorRepository sectorRepository )
    {
        _sectorRepository = sectorRepository;
    }

    public async Task<List<Sector>> FindAllAsync()
    {
       return await _sectorRepository.FindAllAsync();
    }

    public async Task InsertAsync(Sector sector)
    {
        await _sectorRepository.InsertAsync(sector);
    }

    public async Task<Sector> FindByIdAsync(int id)
    {
        return await _sectorRepository.FindByIdAsync(id);
    }

  
}