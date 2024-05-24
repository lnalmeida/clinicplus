using ClinicPlus.Contexts;
using ClinicPlus.ViewModels.Pacientes;
using FluentValidation;

namespace ClinicPlus.Validators.Pacientes;

public class UpdatePacienteValidator : AbstractValidator<UpdatePacienteViewModel>
{
    public UpdatePacienteValidator(ClinicPlusContext context)
    {
        RuleFor(p => p.Cpf)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("O CPF do paciente deve ser informado");

        RuleFor(p => p)
            .Must(p => !context.Pacientes.Any(paciente => p.Cpf == paciente.Cpf && p.Id != paciente.Id))
            .WithMessage("O CPF pertence a um paciente já cadastrado.");
        
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