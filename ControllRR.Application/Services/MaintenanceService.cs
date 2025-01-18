
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
        throw new NotImplementedException();

    }

    public async Task UpdateAsync(Maintenance maintenance)
    {
        throw new NotImplementedException();
    }

    public async Task FinalizeAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<object> GetMaintenanceDataTableAsync(
           int start,
           int length,
           string searchValue,
           string sortColumn,
           string sortDirection)
    {
        (IEnumerable<object> data, int totalRecords, int filteredRecords) =
            await _maintenanceRepository.GetMaintenancesAsync(start, length, searchValue, sortColumn, sortDirection);

        return new
        {
            draw = Guid.NewGuid().ToString(), // Pode ajustar conforme necessário
            recordsTotal = totalRecords,
            recordsFiltered = filteredRecords,
            data
        };
    }
}