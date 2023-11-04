using ECE.Core.DomainObjects;

namespace ECE.Customer.API.Models
{
	public class Customer : Entity, IAggregateRoot
	{
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Cpf { get; private set; }
        public bool Deleted { get; private set; }
        public Address Address { get; private set; }

        protected Customer() { }

        public Customer(string name, string email, string cpf)
        {
            Name = name;
            Email = email;
            Cpf = cpf;
            Deleted = false;
        }
    }
}
