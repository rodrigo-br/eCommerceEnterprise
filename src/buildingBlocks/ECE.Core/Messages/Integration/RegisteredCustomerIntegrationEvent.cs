namespace ECE.Core.Messages.Integration
{
    public class RegisteredCustomerIntegrationEvent : IntegrationEvent
    {
        public Guid Id { get; private set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }

        public RegisteredCustomerIntegrationEvent(Guid id, string name, string email, string cpf)
        {
            Id = id;
            Name = name;
            Email = email;
            Cpf = cpf;
        }
    }
}
