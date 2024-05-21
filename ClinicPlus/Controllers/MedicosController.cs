using ClinicPlus.Contexts;
using ClinicPlus.Models.Entities;
using ClinicPlus.ViewModels.Medicos;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace ClinicPlus.Controllers;

public class MedicosController : Controller
{
    private const int PAGE_SIZE = 10;
    
    private readonly ClinicPlusContext _context;
    private readonly IValidator<AddMedicoViewModel> _addMedicoValidator;
    private readonly IValidator<UpdateMedicoViewModel> _updateMedicoValidator;

    public MedicosController(ClinicPlusContext context, IValidator<AddMedicoViewModel> addMedicoValidator, IValidator<UpdateMedicoViewModel> updateMedicoValidator)
    {
        _context = context;
        _addMedicoValidator = addMedicoValidator;
        _updateMedicoValidator = updateMedicoValidator;
    }

    // GET
    public IActionResult Index(string filter, int page = 1)
    {
        var medicos = _context.Medicos
            .Where(m => m.Nome.Contains(filter) || m.CRM.Contains(filter))
            .Select(m =>
            new ListarMedicosViewModel
            {
                Id = m.Id,
                CRM = m.CRM,
                Nome = m.Nome
            }
        );
        ViewBag.Filter = filter;
        ViewBag.PageNumber = page;
        ViewBag.TotalPages = Math.Ceiling((decimal)medicos.Count() /  PAGE_SIZE);
        
        return View(medicos.Skip((page -1) *  PAGE_SIZE).Take( PAGE_SIZE));
    }
    
    public IActionResult Add()
    {
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Add(AddMedicoViewModel dados)
    {
        var validator = _addMedicoValidator.Validate(dados);
        if (!validator.IsValid)
        {
            validator.AddToModelState(ModelState, string.Empty);
            return View(dados);

        }
        var medico = new Medico
        {
            CRM = dados.CRM,
            Nome = dados.Nome
        };

        _context.Medicos.Add(medico);
        _context.SaveChanges();
        
        return RedirectToAction(nameof(Index));
    }
    
    public IActionResult Update(int id)
    {
        var medico = _context.Medicos.Find(id);
        if (medico is null)
        {
            return NotFound();
        }

        return View(new UpdateMedicoViewModel
        {
            Id = medico.Id,
            CRM = medico.CRM,
            Nome = medico.Nome
        });
    }
    
    

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Update(int id, UpdateMedicoViewModel dados)
    {
        var validator = _updateMedicoValidator.Validate(dados);
        if (!validator.IsValid)
        {
            validator.AddToModelState(ModelState, string.Empty);
            return View(dados);
        }
        
        var medico = _context.Medicos.Find(id);
        if (medico is not null)
        {
            medico.CRM = dados.CRM;
            medico.Nome = dados.Nome;
            
            _context.Medicos.Update(medico);
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
            var medico = _context.Medicos.Find(id);
            if (medico == null)
            {
                return Json(new { success = false });
            }
    
            _context.Medicos.Remove(medico);
            _context.SaveChanges();
    
            return Json(new { success = true });
        }
        catch
        {
            return Json(new { success = false });
        }
    }
}