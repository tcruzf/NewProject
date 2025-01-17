using ControllRR.Application.Interfaces;
using ControllRR.Domain.Entities;
namespace ControllRR.Application.Services;
using ControllRR.Domain.Interfaces;

public class DeviceService : IDeviceService
{
    private readonly IDeviceRepository _deviceRepository;

    public DeviceService(IDeviceRepository deviceRepository)
    {
        _deviceRepository = deviceRepository;
    }
    public async Task<List<Device>> FindAllAsync()
    {
        return await _deviceRepository.FindAllAsync();
    }

    public async Task<Device> FindByIdAsync(int id)
    {
        return await _deviceRepository.FindByIdAsync(id);
    }

    public async Task<Device> GetMaintenancesAsync(int id)
    {
        throw new NotImplementedException();
    }
 
    public async Task InsertAsync(Device device)
    {
    
         _deviceRepository.InsertAsync(device);
       
    }

  public async Task UpdateAsync(Device device)
  {
    _deviceRepository.UpdateAsync(device);
  }
  

}