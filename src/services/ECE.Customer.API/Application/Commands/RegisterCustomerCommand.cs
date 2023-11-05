using ECE.Core.Messages;
using FluentValidation;

namespace ECE.Customer.API.Application.Commands
{
	public class RegisterCustomerCommand : Command
	{
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Cpf { get; private set; }

        public RegisterCustomerCommand(Guid id, string name, string email, string cpf)
        {
            AggregateId = id;
            Id = id;
            Name = name;
            Email = email;
            Cpf = cpf;
        }

		public override bool IsValid()
		{
			ValidationResult = new RegisterCustomerValidation().Validate(this);
            return ValidationResult.IsValid;
		}
	}

    public class RegisterCustomerValidation : AbstractValidator<RegisterCustomerCommand>
    {
        public RegisterCustomerValidation()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do cliente inválido");

            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Nome do cliente deve ser informado");

            RuleFor(c => c.Cpf)
                .Must(HasValidCpf)
                .WithMessage("O CPF informado é inválido");

            RuleFor(c => c.Email)
                .Must(HasValidEmail)
                .WithMessage("O e-mail informado é inválido");

        }

        protected static bool HasValidCpf(string cpf)
        {
            return Core.DomainObjects.Cpf.Validate(cpf);
        }

        protected static bool HasValidEmail(string email)
        {
            return Core.DomainObjects.Email.Validate(email);
        }
    }
}
