using ECE.Core.Data;

namespace ECE.Customer.API.Models
{
	public interface ICustomerRepository : IRepository<Customer>
	{
		void Add(Customer customer);
		Task<IEnumerable<Customer>> GetAll();
		Task<Customer> GetByCpf(string cpf);
	}
}
