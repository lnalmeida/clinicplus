using ClinicPlus.Contexts;
using ClinicPlus.ViewModels.Consultas;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ClinicPlus.Validators.Consultas;

public class AddConsultaValidator : AbstractValidator<AddConsultaViewModel>
{
    private readonly ClinicPlusContext _context;
    
    public AddConsultaValidator(ClinicPlusContext context)
    {
        _context = context;
        
        RuleFor(c => c.Data)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("A data da consulta precisa ser informada.")
            .Must(BeValidDate).WithMessage("A data da consulta não é válida.");

        RuleFor(c => c.EspecialidadeId)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("A especialidade médica precisa ser informada");

        RuleFor(c => c.MedicoId)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("O Informe o médico para a consulta.")
            .MustAsync(BeMedicAvailableForConsultation).WithMessage("O médico selecionado já tem uma consulta marcada nesse horário.");

        RuleFor(c => c.PacienteId)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("Informe o paciente a ser consultado.")
            .MustAsync(BePacientAvailableForConsultation)
            .WithMessage("O paciente informado já tem uma consulta marcada nesse horário.");

    }
    
    private bool BeValidDate(DateTime data)
    {
        return data > DateTime.Now.AddHours(1);
    }

    private async Task<bool> BeMedicAvailableForConsultation(AddConsultaViewModel model, int medicoId, ValidationContext<AddConsultaViewModel> context, CancellationToken cancellationToken)
    {
        var startTime = model.Data;
        var endTime = startTime.AddMinutes(30);
        
        var exists = await _context.Consultas
            .AnyAsync(c => c.MedicoId == medicoId &&
                           c.Data.Date == model.Data.Date &&
                           (c.Data.TimeOfDay <= startTime.TimeOfDay &&
                            endTime.TimeOfDay > startTime.TimeOfDay || 
                            (startTime.TimeOfDay <= c.Data.TimeOfDay && endTime.TimeOfDay > c.Data.TimeOfDay))
                            , cancellationToken: cancellationToken);

        return !exists;
    }
    
    private async Task<bool> BePacientAvailableForConsultation(AddConsultaViewModel model, int pacienteId, ValidationContext<AddConsultaViewModel> context, CancellationToken cancellationToken)
    {
        var startTime = model.Data;
        var endTime = startTime.AddMinutes(30);
        
        var exists = await _context.Consultas
            .AnyAsync(c => c.PacienteId == pacienteId &&
                           c.Data.Date == model.Data.Date &&
                           (c.Data.TimeOfDay <= startTime.TimeOfDay &&
                            endTime.TimeOfDay > startTime.TimeOfDay ||
                            (startTime.TimeOfDay <= c.Data.TimeOfDay && endTime.TimeOfDay > c.Data.TimeOfDay))
                            , cancellationToken: cancellationToken);

        return !exists;
    }
}