using ECE.Core.Data;
using Microsoft.EntityFrameworkCore;
using ECE.Customer.API.Models;

namespace ECE.Customer.API.Data
{
	public sealed class CustomerContext : DbContext, IUnitOfWork
	{
        public CustomerContext(DbContextOptions<CustomerContext> options) : base(options)
        {
			ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
			ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<Models.Customer> Customers { get; set; }
		public DbSet<Address> Address { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(e =>
				e.GetProperties().Where(p => p.ClrType == typeof(string))))
			{
				property.SetColumnType("varchar(100)");
			}

			foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
			{
				relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
			}

			modelBuilder.ApplyConfigurationsFromAssembly(typeof(CustomerContext).Assembly);
		}

		public async Task<bool> Commit()
		{
			var success = await base.SaveChangesAsync() > 0;

			return success;
		}
	}
}
