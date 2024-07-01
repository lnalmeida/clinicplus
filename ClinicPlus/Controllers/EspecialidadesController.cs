using ClinicPlus.Contexts;
using ClinicPlus.Models.Entities;
using ClinicPlus.ViewModels;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace ClinicPlus.Controllers;

public class EspecialidadesController : Controller
{
    private const int PAGE_SIZE = 10;
    
    private readonly ClinicPlusContext _context;
    private readonly IValidator<AddEspecialidadeViewModel> _addEspecialidadeValidator;
    private readonly IValidator<UpdateEspecialidadeViewModel> _updateEspecialidadeValidator;

    public EspecialidadesController(ClinicPlusContext context, IValidator<AddEspecialidadeViewModel> addEspecialidadeValidator, IValidator<UpdateEspecialidadeViewModel> updateEspecialidadeValidator)
    {
        _context = context;
        _addEspecialidadeValidator = addEspecialidadeValidator;
        _updateEspecialidadeValidator = updateEspecialidadeValidator;
    }

    //GET
    public IActionResult Index(string filter, int page = 1)
    {
        ViewBag.ReturnUrl = Url.Action(nameof(Index), "Home");
        
        var especialidadesQuery = _context.Especialidades.AsQueryable();

        if (!string.IsNullOrEmpty(filter))
        {
            especialidadesQuery = especialidadesQuery.Where(e => e.Titulo.Contains(filter) || e.Cor.Contains(filter));
        }
        
        var especialidades = especialidadesQuery
            .Select(e =>
                new ListarEspecialidadesViewModel
                {
                    Id = e.Id,
                    Titulo = e.Titulo,
                    Cor = e.Cor
                }
            );
        
        ViewBag.Filter = filter;
        ViewBag.PageNumber = page;
        ViewBag.TotalPages = Math.Ceiling((decimal)especialidades.Count() /  PAGE_SIZE);
        
        return View(especialidades.Skip((page -1) *  PAGE_SIZE).Take( PAGE_SIZE));
    }
    
    public IActionResult Add()
    {
        ViewBag.ReturnUrl = Url.Action(nameof(Index), "Especialidades") ?? string.Empty;
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Add(AddEspecialidadeViewModel dados)
    {
        var validator = _addEspecialidadeValidator.Validate(dados);
        if (!validator.IsValid)
        {
            validator.AddToModelState(ModelState, string.Empty);
            return View(dados);

        }
        var especialidade = new Especialidade()
        {
            Titulo = dados.Titulo,
            Cor = dados.Cor
        };

        _context.Especialidades.Add(especialidade);
        _context.SaveChanges();
        
        return RedirectToAction(nameof(Index));
    }
    
    public IActionResult Update(int id)
    {
        ViewBag.ReturnUrl = Url.Action(nameof(Index), "Especialidades") ?? string.Empty;
        var especialidade = _context.Especialidades.Find(id);
        if (especialidade is null)
        {
            return NotFound();
        }

        return View(new UpdateEspecialidadeViewModel()
        {
            Id = especialidade.Id,
            Titulo = especialidade.Titulo,
            Cor = especialidade.Cor
        });
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Update(int id, UpdateEspecialidadeViewModel dados)
    {
        var validator = _updateEspecialidadeValidator.Validate(dados);
        if (!validator.IsValid)
        {
            validator.AddToModelState(ModelState, string.Empty);
            return View(dados);
        }
        
        var especialidade = _context.Especialidades.Find(id);
        if (especialidade is not null)
        {
            especialidade.Titulo = dados.Titulo;
            especialidade.Cor = dados.Cor;
            
            _context.Especialidades.Update(especialidade);
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
            var especialidade = _context.Especialidades.Find(id);
            if (especialidade == null)
            {
                return Json(new { success = false });
            }
    
            _context.Especialidades.Remove(especialidade);
            _context.SaveChanges();
    
            return Json(new { success = true });
        }
        catch
        {
            return Json(new { success = false });
        }
    }
}