using ClinicPlus.Contexts;
using ClinicPlus.ViewModels;
using FluentValidation;

namespace ClinicPlus.Validators.Especialidades;

public class AddEspecialidadeValidator : AbstractValidator<AddEspecialidadeViewModel>
{
    public AddEspecialidadeValidator(ClinicPlusContext context)
    {
        RuleFor(e => e.Titulo)
            .Cascade(CascadeMode.Stop)
            .Must(title => !context.Especialidades.Any(e => e.Titulo == title))
            .WithMessage("Essa especialidade já está cadastrada.")
            .NotEmpty().WithMessage("O título da especialidade deve ser preenchido.")
            .MinimumLength(5).WithMessage("O tamanho mínimo do título são 5 caracteres")
            .MaximumLength(50).WithMessage("O tamanhpo máximo para o título sao 50 caracteres.");

        RuleFor(e => e.Cor)
            .Cascade(CascadeMode.Stop)
            .Must(cor => !context.Especialidades.Any(e => e.Cor == cor))
            .WithMessage("Essa cor já está sendo usada por outra especialidade.")
            .NotNull().WithMessage("Escolha uma cor ou azul será o padrão.")
            .Length(7).WithMessage("O tamanho máximo de um código de cores HEX são 7 caracteres.");
            
        
    }
    
}