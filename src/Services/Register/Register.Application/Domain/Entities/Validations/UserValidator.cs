using FluentValidation;

namespace Register.Application.Domain.Entities.Validations
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {

            RuleFor(f => f.Name)
                .NotEmpty()
                    .WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 100)
                    .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(f => f.BirthDate)
                .NotEmpty()
                    .WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(f => f.Ddd)
               .NotEmpty()
                    .WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Matches("^[0-9]*$")
                    .WithMessage("O campo {PropertyName} aceita somente digitos")
               .Length(2, 3)
                    .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(f => f.Phone)
               .NotEmpty()
                    .WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Matches("^[0-9]*$")
                    .WithMessage("O campo {PropertyName} aceita somente digitos")
               .Length(2, 10)
                    .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(f => f.Email)
                .EmailAddress()
                    .WithMessage("O campo {PropertyName} precisa ser um e-mail válido")
               .Length(2, 100)
                    .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(f => f.Document.Length).Equal(CpfValidacao.TamanhoCpf)
                    .WithMessage("O campo Documento precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}.");
            RuleFor(f => CpfValidacao.Validar(f.Document)).Equal(true)
                .WithMessage("O documento fornecido é inválido.");

            RuleFor(x => x.Address).SetValidator(new AddressValidator());
        }
    }
}
