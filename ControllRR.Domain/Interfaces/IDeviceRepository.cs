using ControllRR.Domain.Entities;

namespace ControllRR.Domain.Interfaces;

public interface IDeviceRepository
{
     Task<List<Device>> FindAllAsync();
     Task<Device> FindByIdAsync(int id);
     Task InsertAsync(Device device);
     Task UpdateAsync(Device device);

     Task<Device> GetMaintenancesAsync(int id);
      Task SaveChangesAsync();

}