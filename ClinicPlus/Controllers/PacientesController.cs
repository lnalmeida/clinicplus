using System.Text.RegularExpressions;
using ClinicPlus.Contexts;
using ClinicPlus.Models.Entities;
using ClinicPlus.ViewModels.Pacientes;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace ClinicPlus.Controllers;

public class PacientesController : Controller
{
    private const int PAGE_SIZE = 10;
    
    private readonly ClinicPlusContext _context;
    private readonly IValidator<AddPacienteViewModel> _addPacienteValidator;
    private readonly IValidator<UpdatePacienteViewModel> _updatePacienteValidator;


    public PacientesController(ClinicPlusContext context, IValidator<AddPacienteViewModel> addPacienteValidator, IValidator<UpdatePacienteViewModel> updatePacienteValidator)
    {
        _context = context;
        _addPacienteValidator = addPacienteValidator;
        _updatePacienteValidator = updatePacienteValidator;
    }

    // GET
    public IActionResult Index(string filter, int page = 1)
    {
        var pacientes = _context.Pacientes
            .Where(p => p.Nome.Contains(filter) || p.Cpf.Contains(filter))
            .Select(p =>
                new ListarPacienteViewModel
                {
                    Id = p.Id,
                    Cpf = p.Cpf,
                    Nome = p.Nome
                }
            );
        ViewBag.Filter = filter;
        ViewBag.PageNumber = page;
        ViewBag.TotalPages = Math.Ceiling((decimal)pacientes.Count() /  PAGE_SIZE);
        
        return View(pacientes.Skip((page -1) *  PAGE_SIZE).Take( PAGE_SIZE));
    }
    
    public IActionResult Add()
    {
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Add(AddPacienteViewModel dados)
    {
        var sanitizedCpf = Regex.Replace(dados.Cpf, @"[./-]", "");
        dados.Cpf = sanitizedCpf;
        var validator = _addPacienteValidator.Validate(dados);
        if (!validator.IsValid)
        {
            validator.AddToModelState(ModelState, string.Empty);
            return View(dados);

        }
        var paciente = new Paciente()
        {
            Cpf = dados.Cpf,
            Nome = dados.Nome,
            DataNascimento = dados.DataNascimento
        };

        _context.Pacientes.Add(paciente);
        _context.SaveChanges();
        
        return RedirectToAction(nameof(Index));
    }
    
    public IActionResult Update(int id)
    {
        var paciente = _context.Pacientes.Find(id);
        if (paciente is null)
        {
            return NotFound();
        }

        return View(new UpdatePacienteViewModel
        {
            Id = paciente.Id,
            Cpf = paciente.Cpf,
            Nome = paciente.Nome,
            DataNascimento = paciente.DataNascimento
        });
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Update(int id, UpdatePacienteViewModel dados)
    {
        var sanitizedCpf = Regex.Replace(dados.Cpf, @"[./-]", "");
        dados.Cpf = sanitizedCpf;
        var validator = _updatePacienteValidator.Validate(dados);
        if (!validator.IsValid)
        {
            validator.AddToModelState(ModelState, string.Empty);
            return View(dados);
        }
        
        var paciente = _context.Pacientes.Find(id);
        if (paciente is not null)
        {
            paciente.Cpf = dados.Cpf;
            paciente.Nome = dados.Nome;
            paciente.DataNascimento = dados.DataNascimento;
            
            _context.Pacientes.Update(paciente);
            _context.SaveChanges();
            
            return RedirectToAction(nameof(Index));
        }
        
        return NotFound();
    }
    
    [HttpPost]
    public JsonResult Delete(int id)
    {
        try
        {
            var paciente = _context.Pacientes.Find(id);
            if (paciente == null)
            {
                return Json(new { success = false });
            }
    
            _context.Pacientes.Remove(paciente);
            _context.SaveChanges();
    
            return Json(new { success = true });
        }
        catch
        {
            return Json(new { success = false });
        }
    }
}