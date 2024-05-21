using System.Text.RegularExpressions;
using ClinicPlus.Contexts;
using ClinicPlus.ViewModels.Medicos;
using FluentValidation;
using Microsoft.CodeAnalysis.Differencing;

namespace ClinicPlus.Validators.Medicos;

public class UpdateMedicoValidator : AbstractValidator<UpdateMedicoViewModel>
{
    public UpdateMedicoValidator(ClinicPlusContext context)
    {
        RuleFor(m => m.CRM)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("O CRM do médico é obrigatório.")
            .MaximumLength(13).WithMessage("O CRM deve ter no máximo {MaxLength} caracteres.")
            .Must(crm => IsCrm(crm)).WithMessage("O formato do CRM não é válido. Deve ser : [4-10]9999999999/AA");

        RuleFor(m => m)
            .Must(m => !context.Medicos.Any(medico => medico.CRM == m.CRM && medico.Id != m.Id))
            .WithMessage("Este CRM pertence a outro médico já cadastrado.");
        
        RuleFor(m => m.Nome)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("O nome completo do médico é obrigatório.")
            .MinimumLength(3).WithMessage("O nome do médico deve ter no mínimo {MinLength} caracteres.")
            .MaximumLength(150).WithMessage("O nome do médico não pode exceder {MaxLength} caracteres.");
    }
    
    private bool IsCrm(string crm)
    {
        string pattern = @"^\d{4,10}/[A-Z]{2}$";
        Match match = Regex.Match(crm, pattern);
        return match.Success;
    }
}