using ClinicPlus.Contexts;
using ClinicPlus.ViewModels.Pacientes;
using FluentValidation;

namespace ClinicPlus.Validators.Pacientes;

public class AddPacienteValidator : AbstractValidator<AddPacienteViewModel>
{
    public AddPacienteValidator(ClinicPlusContext context)
    {
        RuleFor(p => p.Cpf)
            .Cascade(CascadeMode.Stop)
            .Must(cpf => !context.Pacientes.Any(p => p.Cpf == cpf))
            .WithMessage("O CPF já pertence a um paciente já cadastrado")
            .NotEmpty().WithMessage("O CPF do paciente deve ser informado");
        
        RuleFor(p => p.Nome)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("O nome do paciente deve ser informado.");

        RuleFor(p => p.DataNascimento)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("A data de nascimento do paciente deve ser informada.")
            .Must(dataNascimento => dataNascimento <= DateTime.Now.AddYears(-18))
            .WithMessage("O paciente deve ter pelo menos 18 anos completos");
    }
}