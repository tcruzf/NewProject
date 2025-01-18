using System.Diagnostics;
using ControllRR.Application.Interfaces;
using ControllRR.Domain.Entities;
using ControllRR.Infrastructure.Data.Context;
using ControllRR.Presentation.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;


namespace ControlRR.Controllers;

public class DevicesController : Controller
{

    private readonly IDeviceService _deviceService;
    private readonly ISectorService _sectorService;
    private readonly ControllRRContext _controllRRContext;

    public DevicesController(IDeviceService deviceService, ISectorService sectorService, ControllRRContext controllRRContext)
    {
        _deviceService = deviceService;
        _sectorService = sectorService;
        _controllRRContext = controllRRContext;
    }
   
    public async Task<IActionResult> Index()
    {
        var devices = await _deviceService.FindAllAsync();
        return View(devices);
    }
   
    public async Task<IActionResult> List()
    {

        return View();
    }
   
    [HttpPost]
    public async Task<JsonResult> GetList()
    {
        var draw = Request.Form["draw"].FirstOrDefault();
        var start = Convert.ToInt32(Request.Form["start"].FirstOrDefault() ?? "0");
        var length = Convert.ToInt32(Request.Form["length"].FirstOrDefault() ?? "10");
        var searchValue = Request.Form["search[value]"].FirstOrDefault()?.ToLower();
        var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"] + "][data]"].FirstOrDefault();
        var sortDirection = Request.Form["order[0][dir]"].FirstOrDefault();

        // Obter a consulta base diretamente do contexto
        var dataQuery = _controllRRContext.Devices.Include(x => x.Sector).AsQueryable();

        // Filtragem
        if (!string.IsNullOrEmpty(searchValue))
        {
            dataQuery = dataQuery.Where(x =>
                x.Id != null && x.Id.ToString().Contains(searchValue) ||
                x.SerialNumber != null && x.SerialNumber.ToLower().Contains(searchValue) ||
                x.Sector != null && x.Sector.Name.ToLower().Contains(searchValue) ||
                x.Type != null && x.Type.ToLower().Contains(searchValue) ||
                x.Identifier != null && x.Identifier.ToLower().Contains(searchValue) ||
                x.Model != null && x.Model.ToLower().Contains(searchValue));
            // return Json(dataQuery);
        }
        // Ordenação
        if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortDirection))
        {
            dataQuery = dataQuery.OrderBy($"{sortColumn} {sortDirection}");
        }
        else
        {
            // Ordenação padrão (caso nenhuma seja fornecida)
            dataQuery = dataQuery.OrderBy(x => x.Id);
        }

        // Contagem total após filtro
        var filteredCount = await dataQuery.CountAsync();

        // Paginação
        //var data = await dataQuery.Skip(start).Take(length).ToListAsync();
        var data = await dataQuery
        .Skip(start)
        .Take(length)
        .Select(x => new
        {
            Id = x.Id,
            Type = x.Type,
            Identifier = x.Identifier,
            Model = x.Model,
            SerialNumber = x.SerialNumber,
            Sector = x.Sector != null ? x.Sector.Name : null,
            Description = x.DeviceDescription
        })
        .ToListAsync();

        // Montar o JSON de retorno
        return Json(new
        {
            draw,
            recordsFiltered = filteredCount,
            recordsTotal = await _controllRRContext.Devices.CountAsync(),
            data
        });
    }// End GetList

   
    [HttpGet]
    public async Task<IActionResult> Search(string term)
    {
        if (string.IsNullOrWhiteSpace(term))
        {
            return Json(new List<object>());
        }

        var devices = await _controllRRContext.Devices
            .Where(d => d.Model.Contains(term) || d.SerialNumber.Contains(term) || d.Type.Contains(term) || d.Identifier.Contains(term))
            .Select(d => new { d.Id, d.Model, d.SerialNumber, d.Type, d.Identifier })
            .ToListAsync();

        return Json(devices);
    }

   
    public async Task<IActionResult> Details(int id)
    {
        var device = await _deviceService.FindByIdAsync(id);

        if (id == null)
        {
            return RedirectToAction(nameof(Error), new { message = "Id não fornecido." });
        }
        if (device == null)
        {
            return RedirectToAction(nameof(Error), new { message = "Identificador não encontrado!" });
        }
        return View(device);
    }
    
    public async Task<IActionResult> GetMaintenances(int id)
    {
        var device = await _deviceService.GetMaintenancesAsync(id);
        if (id == null)
        {
            return RedirectToAction(nameof(Error), new { message = "Id não fornecido!" });
        }
        if (device == null)
        {
            return RedirectToAction(nameof(Error), new { message = "Dispositivo não encontrado!" });
        }
        return View(device);

    }
    
    [HttpGet]
    public async Task<IActionResult> New()
    {
        var sectors = await _sectorService.FindAllAsync();
        var viewModel = new DeviceViewModel { Sector = sectors };
        return View(viewModel);
    }

   
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> New(Device device)
    {
        System.Console.WriteLine(device);
        if (!ModelState.IsValid)
        {

            // Minha unica forma de debug fora do debug
            //System.Console.WriteLine(device.Description);

            var sector = await _sectorService.FindAllAsync();
            var viewModel = new DeviceViewModel { Sector = sector };
            // Printar System.Console.WriteLine(viewModel);
            return View(viewModel);
        }
        await _deviceService.InsertAsync(device);
        return RedirectToAction(nameof(Index));

    }
   
    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return RedirectToAction(nameof(Error), new { message = "Não foi fornecido nenhum id para ser editado!" });
        }
        var device = await _deviceService.FindByIdAsync(id.Value);
        if (device == null)
        {
            return RedirectToAction(nameof(Error), new { message = "Dispositivo não encontrado" });
        }

        List<Sector> sector = await _sectorService.FindAllAsync();
        DeviceViewModel viewModel = new DeviceViewModel { Sector = sector, Device = device };
        return View(viewModel);
    }
   
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, Device device)
    {
        if (!ModelState.IsValid)
        {
            List<Sector> sector = await _sectorService.FindAllAsync();
            var viewModel = new DeviceViewModel { Device = device, Sector = sector };
            return View(viewModel);
        }
        if (id != device.Id)
        {
            return RedirectToAction(nameof(Error), new { message = "Id fornecido não corresponde ao id do dispositivo!" });
        }
        try
        {
            _deviceService.UpdateAsync(device);
            return RedirectToAction(nameof(Index));
        }
        catch (ApplicationException e)
        {
            return RedirectToAction(nameof(Error), new { message = e.Message });
        }


    }

   
    public async Task<IActionResult> Error(string message)
    {
        var viewModel = new ErrorViewModel
        {
            Message = message,
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        };
        return View(viewModel);
    }
}