using ControllRR.Application.Interfaces;
using ControllRR.Infrastructure.Repositories;
using ControllRR.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using ControllRR.Infrastructure.Data.Context;
using ControllRR.Presentation.ViewModels;
using System.Diagnostics;
using ControllRR.Domain.Entities;

namespace ControlRR.Controllers;

public class MaintenancesController : Controller
{

    private readonly IMaintenanceService _maintenanceService;
    private readonly IUserService _userService;
    private readonly IDeviceService _deviceService;
    
    public MaintenancesController(IMaintenanceService maintenanceService, IUserService userService, IDeviceService deviceService, ControllRRContext controllRRContext)
    {
        _maintenanceService = maintenanceService;
        _userService = userService;
        _deviceService = deviceService;
       
    }

    public async Task<IActionResult> Index()
    {
        var obj = await _maintenanceService.FindAllAsync();
        return View(obj);
    }

    public async Task<IActionResult> Details(int id)
    {
        // PAREI AQUI var obj = new MaintenanceViewModel { Maintenance = }
        var list = await _maintenanceService.FindByIdAsync(id);
        if (id == null)
        {
            return RedirectToAction(nameof(Error), new { message = "ID não pode ser nulo!" });
        }
        if (list == null)
        {
            return RedirectToAction(nameof(Error), new { message = "Identificador não encontrado!" });
        }
        return View(list);

    }
    [HttpGet]
    public async Task<IActionResult> New()
    {

        var users = await _userService.FindAllAsync();
        var device = await _deviceService.FindAllAsync();
        var viewModel = new MaintenanceViewModel { User = users, Device = device };
        return View(viewModel);

    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> New(Maintenance maintenance)
    {
        if (!ModelState.IsValid)
        {
            var user = await _userService.FindAllAsync();
            var device = await _deviceService.FindAllAsync();
            var viewModel = new MaintenanceViewModel { Maintenance = maintenance, User = user, Device = device };


            return View(viewModel);
        }

        await _maintenanceService.InsertAsync(maintenance);
        return RedirectToAction(nameof(Index));
    }


    

    [HttpGet]
    public async Task<IActionResult> MaintenanceList()
    {
        return View();
    }
    [HttpPost]
    public async Task<JsonResult> AllMaintenances()
    {
        var draw = Request.Form["draw"].FirstOrDefault();
        var start = Convert.ToInt32(Request.Form["start"].FirstOrDefault() ?? "0");
        var length = Convert.ToInt32(Request.Form["length"].FirstOrDefault() ?? "10");
        var searchValue = Request.Form["search[value]"].FirstOrDefault()?.ToLower();
        var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"] + "][data]"].FirstOrDefault();
        var sortDirection = Request.Form["order[0][dir]"].FirstOrDefault();

        var result = await _maintenanceService.GetMaintenanceDataTableAsync(
            start, length, searchValue, sortColumn, sortDirection);

        return Json(result);
    } // Fim do método

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
