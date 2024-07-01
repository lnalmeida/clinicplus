using ClinicPlus.Contexts;
using ClinicPlus.Models.Entities;
using ClinicPlus.ViewModels.Consultas;
using ClinicPlus.ViewModels.Consultss;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ClinicPlus.Controllers;

public class ConsultasController : Controller
{
    private const int PAGE_SIZE = 10;
    
    private readonly ClinicPlusContext _context;
    private readonly IValidator<AddConsultaViewModel> _addConsultaValidator;
    private readonly IValidator<UpdateConsultaViewModel> _updateConsultaValidator;

    public ConsultasController(ClinicPlusContext context, IValidator<AddConsultaViewModel> addConsultaValidator, IValidator<UpdateConsultaViewModel> updateConsultaValidator)
    {
        _context = context;
        _addConsultaValidator = addConsultaValidator;
        _updateConsultaValidator = updateConsultaValidator;
    }

    // GET
    public IActionResult Index(string filter, int page = 1)
    {
        ViewBag.ReturnUrl = Url.Action(nameof(Index), "Home") ?? string.Empty;
        
        var consultasQuery = _context.Consultas.AsQueryable();

        if (!string.IsNullOrEmpty(filter))
        {
            consultasQuery = consultasQuery.Include(c => c.Especialidade)
                .Include(c => c.Paciente)
                .Include(c => c.Medico)
                .Where(c => c.Especialidade.Titulo.Contains(filter) || c.Paciente.Nome.Contains(filter) ||
                            c.Medico.Nome.Contains(filter));
        }
        var consultas = consultasQuery
                    .Select(c => new ListarConsultasViewModel
                    {
                        Id = c.Id,
                        Data = c.Data,
                        Especialidade = c.Especialidade.Titulo,
                        CorEspecialidade = c.Especialidade.Cor,
                        Paciente = c.Paciente.Nome,
                        Medico = c.Medico.Nome
                    }
                    
                    );
        
        ViewBag.Filter = filter;
        ViewBag.PageNumber = page;
        ViewBag.TotalPages = Math.Ceiling((decimal)consultas.Count() /  PAGE_SIZE);
        
        return View(consultas.Skip((page -1) * PAGE_SIZE).Take(PAGE_SIZE));
    }

    
    public IActionResult Add()
    {
        ViewBag.ReturnUrl = Url.Action(nameof(Index), "Consultas") ?? string.Empty;
        PopulateViewBags();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(AddConsultaViewModel dados)
    {
        var validator = await _addConsultaValidator.ValidateAsync(dados);
        if (!validator.IsValid)
        {
            PopulateViewBags();
            validator.AddToModelState(ModelState, string.Empty);
            return View(dados);
        }

        var consulta = new Consulta
        {
            Data = dados.Data,
            EspecialidadeId = dados.EspecialidadeId,
            MedicoId = dados.MedicoId,
            PacienteId = dados.PacienteId
        };

        _context.Consultas.Add(consulta);
        await _context.SaveChangesAsync();
        
        
        return RedirectToAction(nameof(System.Index));
    }

    public IActionResult Update(int id)
    {
        ViewBag.ReturnUrl = Url.Action(nameof(Index), "Consultas") ?? string.Empty;
        var consulta = _context.Consultas
                .Include(c => c.Especialidade)
                .Include(c => c.Medico)
                .Include(c => c.Paciente)
                .FirstOrDefault(c => c.Id == id);
        if (consulta is null)
        {
            return NotFound();
        }

        // PopulateViewBags();
        ViewBag.EspecialidadeAtualId = consulta.EspecialidadeId;
        ViewBag.MedicoAtualId = consulta.MedicoId;
        ViewBag.PacienteAtualId = consulta.PacienteId;
        
        return View(new UpdateConsultaViewModel()
        {
            Id = consulta.Id,
            Data = consulta.Data,
            EspecialidadeId = consulta.EspecialidadeId,
            MedicoId = consulta.MedicoId,
            PacienteId = consulta.PacienteId
        });
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(int id, UpdateConsultaViewModel dados)
    {
        var validator = await _updateConsultaValidator.ValidateAsync(dados);
        if (!validator.IsValid)
        {
            validator.AddToModelState(ModelState, string.Empty);
            return View(dados);
        }
        
        var consulta = _context.Consultas
            .Include(c => c.Especialidade)
            .Include(c => c.Medico)
            .Include(c => c.Paciente)
            .FirstOrDefault(c => c.Id == id);
        if (consulta is not null)
        {
            consulta.Data = dados.Data;
            consulta.EspecialidadeId = dados.EspecialidadeId;
            consulta.MedicoId = dados.MedicoId;
            consulta.PacienteId = dados.PacienteId;
            
            _context.Update(consulta);
            await _context.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }
        
        return NotFound();
    }
    
    [HttpPost]
    public JsonResult Delete(int id)
    {
        try
        {
            var consulta = _context.Consultas.Find(id);
            if (consulta == null)
            {
                return Json(new { success = false });
            }
    
            _context.Consultas.Remove(consulta);
            _context.SaveChanges();
    
            return Json(new { success = true });
        }
        catch
        {
            return Json(new { success = false });
        }
    }



    #region Ajax Functions

    public JsonResult GetEspecialidadeAtualById(int especialidadeAtualId)
    {
        var especialidadeAtual = _context.Especialidades
            .Where(e => e.Id == especialidadeAtualId)
            .Select(e => new { e.Id, e.Titulo, e.Cor });
        return Json(especialidadeAtual);
    }

    public JsonResult GetPacienteAtualById(int pacienteAtualId)
    {
        var especialidadeAtual = _context.Pacientes
            .Where(p => p.Id == pacienteAtualId)
            .Select(p => new { p.Id, p.Nome });
        return Json(especialidadeAtual);
    }
    
    public JsonResult GetMedicoAtualById(int medicoAtualId)
    {
        var medicoAtual = _context.Medicos
            .Where(m => m.Id == medicoAtualId)
            .Select(m => new { m.Id, m.Nome });
        return Json(medicoAtual);
    }
    
    public JsonResult GetEspecialidades()
    {
        var especialidades = _context.Especialidades
            .Select(e => new { e.Id, e.Titulo, e.Cor })
            .ToList();
        return Json(especialidades);
    }

    public JsonResult GetMedicosByEspecialidade(int especialidadeSelecionadaId)
    {
        var medicos = _context.Medicos
            .Where(m => m.EspecialidadeId == especialidadeSelecionadaId)
            .Select(m => new { m.Id, m.Nome })
            .ToList();
        return Json(medicos);
    }
    
    public JsonResult GetPacientes()
    {
        var pacientes = _context.Pacientes
            .Select(p => new { p.Id, p.Nome })
            .ToList();
        return Json(pacientes);
    }
    

    #endregion


    
    private void PopulateViewBags(int? especialidadeId = null, int? pacienteId = null)
    {
        var especialidadesQuery = _context.Especialidades.AsQueryable();

        if (especialidadeId != null)
        {
            especialidadesQuery = especialidadesQuery
                .Where(especialidade => especialidade.Id == especialidadeId)
                .OrderBy(especialidade => especialidade.Titulo);
        }
        
        ViewBag.ListaEspecialidades = especialidadesQuery
            .Select(e => new SelectListItem
            {
                Text = e.Titulo,
                Value = e.Id.ToString(),
            });
        
        var pacientesQuery = _context.Pacientes.AsQueryable();

        if (pacienteId != null)
        {
            pacientesQuery = pacientesQuery
                .Where(paciente => paciente.Id == pacienteId)
                .OrderBy(paciente => paciente.Nome);
        }
        
        ViewBag.ListaPacientes = pacientesQuery
            .Select(p => new SelectListItem { Text = p.Nome, Value = p.Id.ToString() });
    }
    
}