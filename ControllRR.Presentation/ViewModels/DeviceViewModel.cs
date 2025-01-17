using ControllRR.Domain.Entities;

namespace ControllRR.Presentation.ViewModels;


public class DeviceViewModel
{
    public Device Device { get; set; }
    public ICollection<Sector> Sector { get; set; }
}