using ControllRR.Domain.Entities;

namespace ControllRR.Domain.Interfaces;
public interface IMaintenanceRepository
{
    Task<List<Maintenance>> FindAllAsync();
    Task<Maintenance> FindByIdAsync(int id);
    Task InsertAsync (Maintenance maintenance);
    Task RemoveAsync(int id);
    Task UpdateAsync(Maintenance maintenance);
    Task FinalizeAsync(int id);
     Task SaveChangesAsync();
}