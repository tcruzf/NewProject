using ControllRR.Domain.Entities;

namespace ControllRR.Domain.Interfaces;
public interface ISectorRepository
{
    Task<List<Sector>> FindAllAsync();
    Task<Sector> FindByIdAsync(int id);

    Task InsertAsync(Sector sector);
     Task SaveChangesAsync();
}