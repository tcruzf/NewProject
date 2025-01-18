using ControllRR.Application.Dto;
using ControllRR.Domain.Entities;

namespace ControllRR.Presentation.ViewModels;


public class MaintenanceViewModel
{
    public MaintenanceDto MaintenanceDto { get; set; }
    public ICollection<User> User { get; set; }
    public ICollection<Device>? Device { get; set; }

}