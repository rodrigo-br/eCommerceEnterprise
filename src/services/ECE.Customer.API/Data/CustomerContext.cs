using ECE.Core.Data;
using Microsoft.EntityFrameworkCore;
using ECE.Customer.API.Models;
using ECE.Core.Mediator;
using ECE.Core.DomainObjects;

namespace ECE.Customer.API.Data
{
	public sealed class CustomerContext : DbContext, IUnitOfWork
	{
		private readonly IMediatorHandler _mediatorHandler;

		public CustomerContext(DbContextOptions<CustomerContext> options,
								IMediatorHandler mediatorHandler) : base(options)
		{
			ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
			ChangeTracker.AutoDetectChangesEnabled = false;
			_mediatorHandler = mediatorHandler;
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
			if (success)
			{
				await _mediatorHandler.PublishEvents(this);
			}

			return success;
		}
	}

	public static class MediatorExtension
	{
		public static async Task PublishEvents<T>(this IMediatorHandler mediator, T context) where T : DbContext
		{
			var domainEntities = context.ChangeTracker
				.Entries<Entity>()
				.Where(x => x.Entity.Notifications != null && x.Entity.Notifications.Any());

			var domainEvents = domainEntities
				.SelectMany(x => x.Entity.Notifications)
				.ToList();

			domainEntities.ToList()
				.ForEach(e =>  e.Entity.ClearEvents());

			var tasks = domainEvents
				.Select(async (domainEvent) =>
				{
					await mediator.PublishEvent(domainEvent);
				});

			await Task.WhenAll(tasks);
		}
	}
}
