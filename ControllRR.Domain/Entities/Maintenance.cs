using System.ComponentModel.DataAnnotations;
using ControllRR.Domain.Enums;

namespace ControllRR.Domain.Entities;

public class Maintenance
{
    public int Id { get; set; }
    [Display(Name = "Descrição Simples")]
    [Required(ErrorMessage = "O campo {0} é obrigatorio ")]
    [StringLength(20, MinimumLength = 5, ErrorMessage = "{0} minimo {2} e no maximo {1} caracteres")]
    public string SimpleDesc { get; set; }
    [Display(Name = "Descrição")]
    [Required(ErrorMessage = "O campo {0} é obrigatorio!")]
    [StringLength(300, MinimumLength = 3, ErrorMessage = "{0} Deve ter entre {2} e {1} caracteres")]
    public string Description { get; set; }
    [DataType(DataType.Date)]
    [Display(Name = "Abertura")]
    [Required(ErrorMessage = "O campo {0} é obrigatorio!")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime OpenDate { get; set; }
    [DataType(DataType.Date)]
    [Display(Name = "Fechamento")]
    [Required(ErrorMessage = "O campo {0} é obrigatorio!")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime CloseDate { get; set; }
    public MaintenanceStatus Status { get; set; }

    public int UserId { get; set; }
    [Display(Name = "Usuario")]
    public User? User { get; set; }
    [Display(Name = "Dispositivo")]
    public Device? Device { get; set; }
    public int DeviceId { get; set; }

    [Display(Name = "Dispositivo")]


    public int MaintenanceNumber { get; set; }


    public Maintenance()
    {

    }
    public Maintenance(int id, string description, string simpleDesc, DateTime openDate, DateTime closeDate, MaintenanceStatus status, User user, Device device, int maintenanceNumber)
    //public Maintenance(int id, string description, string simpleDesc, DateTime openDate, DateTime closeDate, MaintenanceStatus status, int maintenanceNumber)
    {
        Id = id;
        Description = description;
        SimpleDesc = simpleDesc;
        OpenDate = openDate;
        CloseDate = closeDate;
        Status = status;
        User = user;
        Device = device;
        MaintenanceNumber = maintenanceNumber;
    }


}
