using ClinicPlus.ViewModels.Monitoramentos;
using ClinicPlus.ViewModels.Pacientes;
using FluentValidation;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;

namespace ClinicPlus.Validators.Monitoramentos;

public class UpdateMonitoramentoValidator : AbstractValidator<UpdateMonitoramentoPacienteViewModel>
{
    public UpdateMonitoramentoValidator()
    {
        RuleFor(mp => mp.PressaoArterial)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("A pressão arterial deve ser informada.")
            .MaximumLength(7).WithMessage("O formato desse campo é 999/999");

        RuleFor(mp => mp.FrequenciaCardiaca)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("A frquência cardíaca deve ser informada.")
            .Must(freqCardiaca => freqCardiaca >= 0)
            .WithMessage("A frequência cardíaca deve ser maior ou igual a 0.");

        RuleFor(mp => mp.Temperatura)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("A temperatura corporal deve ser informada.")
            .Must(temperatura => temperatura > 0)
            .WithMessage("A temperatura deve ser maior que 0.");
        
        RuleFor(mp => mp.Saturacao)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("A pressão arterial deve ser informada.")
            .Must(saturacao => saturacao >= 0 && saturacao <= 100)
            .WithMessage("A saturação do oxigênio deve estar entre 0% e 100%.");

        RuleFor(mp => mp.DataAfericao)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("A data da aferição deve ser informada")
            .Must(afericao => afericao <= DateTime.Now)
            .WithMessage("A data da aferição deve ser menor ou igual à data de hoje.");
    }
}