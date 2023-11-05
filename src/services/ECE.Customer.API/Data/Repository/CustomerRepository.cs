using ECE.Core.Data;
using ECE.Customer.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ECE.Customer.API.Data.Repository
{
	public class CustomerRepository : ICustomerRepository
	{
		private readonly CustomerContext _context;

		public CustomerRepository(CustomerContext context)
		{
			_context = context;
		}

		public IUnitOfWork UnitOfWork => _context;

		public void Add(Models.Customer customer)
		{
			_context.Customers.Add(customer);
		}

		public void Dispose()
		{
			_context.Dispose();
		}

		public async Task<IEnumerable<Models.Customer>> GetAll()
		{
			return await _context.Customers.AsNoTracking().ToListAsync();
		}

		public async Task<Models.Customer> GetByCpf(string cpf)
		{
			return await _context.Customers.FirstOrDefaultAsync(c => c.Cpf.Number == cpf);
		}
	}
}
