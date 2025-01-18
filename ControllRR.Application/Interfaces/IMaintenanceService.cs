using ControllRR.Domain.Entities;

namespace ControllRR.Application.Interfaces;
public interface IMaintenanceService
{
    Task<List<Maintenance>> FindAllAsync();
    Task<Maintenance> FindByIdAsync(int id);
    Task InsertAsync (Maintenance maintenance);
    Task RemoveAsync(int id);
    Task UpdateAsync(Maintenance maintenance);
    Task FinalizeAsync(int id);
      Task<object> GetMaintenanceDataTableAsync( int start,
    int length,
    string searchValue,
    string sortColumn,
    string sortDirection);
}