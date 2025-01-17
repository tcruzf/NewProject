using ControllRR.Domain.Entities;

namespace ControllRR.Application.Interfaces;
public interface ISectorService
{
    Task<List<Sector>> FindAllAsync();
    Task<Sector> FindByIdAsync(int id);

    Task InsertAsync(Sector sector);
}