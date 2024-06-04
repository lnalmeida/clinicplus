using ClinicPlus.Contexts;
using ClinicPlus.Models.Entities;
using ClinicPlus.ViewModels.Monitoramentos;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;

namespace ClinicPlus.Controllers;

[Route("Monitoramento")]
public class MonitoramentosPacientesController : Controller
{
    private readonly ClinicPlusContext _context;
    private readonly IValidator<AddMonitoramentoPacienteViewModel> _addMonitoramentoValidator;
    private readonly IValidator<UpdateMonitoramentoPacienteViewModel> _updateMonitoramentoValidator;

    public MonitoramentosPacientesController(ClinicPlusContext context, IValidator<AddMonitoramentoPacienteViewModel> addMonitoramentoValidator, IValidator<UpdateMonitoramentoPacienteViewModel> updateMonitoramentoValidator)
    {
        _context = context;
        _addMonitoramentoValidator = addMonitoramentoValidator;
        _updateMonitoramentoValidator = updateMonitoramentoValidator;
    }

    // GET
    public IActionResult Index(int idPaciente)
    {
        ViewBag.IdPaciente = idPaciente;
        var paciente = _context.Pacientes.Find(idPaciente);
        ViewBag.NomePaciente = paciente?.Nome;
        ViewBag.ReturnUrl = Url.Action("Index", "Pacientes");
        
        var monitoramentos = _context.MonitoramentosPacientes.AsNoTracking()
            .Where(mp => mp.IdPaciente == idPaciente)
            .Select(mp => new ListarMonitoramentoPacienteViewModel
            {
                Id = mp.Id,
                PressaoArterial = mp.PressaoArterial,
                Temperatura = mp.Temperatura,
                Saturacao = mp.Saturacao,
                FrequenciaCardiaca = mp.FrequenciaCardiaca,
                DataAfericao = mp.DataAfericao
            });
        
        return View(monitoramentos);
    }

    [Route("add")]
    public IActionResult Add(int idPaciente)
    {
        ViewBag.IdPaciente = idPaciente;
        var paciente = _context.Pacientes.Find(idPaciente);
        ViewBag.NomePaciente = paciente?.Nome;
        ViewBag.ReturnUrl = Url.Action(nameof(Index), "MonitoramentosPacientes", new {idPaciente});
        return View();
    }

    [Route("add")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Add(int idPaciente, AddMonitoramentoPacienteViewModel dados)
    {
        var validator = _addMonitoramentoValidator.Validate(dados);
        if (!validator.IsValid)
        {
            validator.AddToModelState(ModelState, string.Empty);
            return View(dados);

        }
        
        var monitoramento = new MonitoramentoPaciente()
        {
            PressaoArterial = dados.PressaoArterial,
            Temperatura = dados.Temperatura,
            Saturacao = dados.Saturacao,
            FrequenciaCardiaca = dados.FrequenciaCardiaca,
            DataAfericao = dados.DataAfericao,
            IdPaciente =  idPaciente
        };
            
        _context.MonitoramentosPacientes.Add(monitoramento);
        _context.SaveChanges();
        
        return RedirectToAction(nameof(Index), new { idPaciente });
    }
    
    [Route("update/{id}")]
    public IActionResult Update(int id)
    {
        var monitoramento = _context.MonitoramentosPacientes.Find(id);
        if (monitoramento is null)
        {
            return NotFound();
        }
        ViewBag.IdPaciente = monitoramento.IdPaciente;
        var paciente = _context.Pacientes.Find(monitoramento.IdPaciente);
        if (paciente == null)
        {
            return NotFound();
        }
        ViewBag.NomePaciente = paciente?.Nome;
        ViewBag.ReturnUrl = Url.Action(nameof(Index), "MonitoramentosPacientes", new {monitoramento.IdPaciente});
        
        return View(new UpdateMonitoramentoPacienteViewModel()
        {
            Id = monitoramento.Id,
            PressaoArterial = monitoramento.PressaoArterial,
            Temperatura = monitoramento.Temperatura,
            FrequenciaCardiaca = monitoramento.FrequenciaCardiaca,
            Saturacao = monitoramento.Saturacao,
            DataAfericao = monitoramento.DataAfericao
        });
    }
    
    [Route("update/{id}")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Update(int id, UpdateMonitoramentoPacienteViewModel dados)
    {
        var validator = _updateMonitoramentoValidator.Validate(dados);
        if (!validator.IsValid)
        {
            validator.AddToModelState(ModelState, string.Empty);
            return View(dados);
        }
        
        var monitoramento = _context.MonitoramentosPacientes.Find(id);
        if (monitoramento is not null)
        {
            monitoramento.PressaoArterial = dados.PressaoArterial;
            monitoramento.Temperatura = dados.Temperatura;
            monitoramento.FrequenciaCardiaca = dados.FrequenciaCardiaca;
            monitoramento.Saturacao = dados.Saturacao;
            monitoramento.DataAfericao = dados.DataAfericao;
            
            _context.MonitoramentosPacientes.Update(monitoramento);
            _context.SaveChanges();
            
            return RedirectToAction(nameof(Index), new {monitoramento.IdPaciente});
        }
        
        return NotFound();
    }
    
    [HttpPost]
    public JsonResult Delete(int id)
    {
        try
        {
            var monitoramento = _context.MonitoramentosPacientes.Find(id);
            if (monitoramento == null)
            {
                return Json(new { success = false });
            }
    
            _context.MonitoramentosPacientes.Remove(monitoramento);
            _context.SaveChanges();
    
            return Json(new { success = true });
        }
        catch
        {
            return Json(new { success = false });
        }
    }
}