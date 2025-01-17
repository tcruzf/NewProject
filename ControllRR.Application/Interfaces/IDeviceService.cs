using ControllRR.Domain.Entities;

namespace ControllRR.Application.Interfaces;

public interface IDeviceService
{
     Task<List<Device>> FindAllAsync();
     Task<Device> FindByIdAsync(int id);
     Task InsertAsync(Device device);
     Task UpdateAsync(Device device);

     Task<Device> GetMaintenancesAsync(int id);

}