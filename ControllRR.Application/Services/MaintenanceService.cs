
using ControllRR.Application.Interfaces;
using ControllRR.Domain.Entities;
using ControllRR.Domain.Interfaces;

namespace ControllRR.Application.Services;


public class MaintenanceService : IMaintenanceService
{
    private readonly IMaintenanceRepository _maintenanceRepository;
    
    public MaintenanceService(IMaintenanceRepository maintenanceRepository)
    {
        _maintenanceRepository = maintenanceRepository;
    }

    public async Task<List<Maintenance>> FindAllAsync()
    {

        return await _maintenanceRepository.FindAllAsync();
    }

    public async Task<Maintenance> FindByIdAsync(int id)
    {
        return await _maintenanceRepository.FindByIdAsync(id);
    }
    
    public async Task InsertAsync(Maintenance maintenance)
    {
       _maintenanceRepository.InsertAsync(maintenance);
       await _maintenanceRepository.SaveChangesAsync();
    }

    public async Task RemoveAsync(int id)
    {
        

    }

    public async Task UpdateAsync(Maintenance maintenance)
    {
       
    }

    public async Task FinalizeAsync(int id)
    {
      
    }

}