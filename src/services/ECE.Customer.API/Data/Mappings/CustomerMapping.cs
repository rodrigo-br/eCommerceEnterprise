using ECE.Core.DomainObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECE.Customer.API.Data.Mappings
{
	public class CustomerMapping : IEntityTypeConfiguration<Models.Customer>
	{
		public void Configure(EntityTypeBuilder<Models.Customer> builder)
		{
			builder.HasKey(c => c.Id);

			builder.Property(c => c.Name)
				.IsRequired()
				.HasColumnType("varchar(200)");

			builder.OwnsOne(c => c.Cpf, tf =>
			{
				tf.Property(c => c.Number)
					.IsRequired()
					.HasMaxLength(Cpf.CpfMaxLength)
					.HasColumnName("Cpf")
					.HasColumnType($"varchar({Cpf.CpfMaxLength})");
			});

			builder.OwnsOne(c => c.Email, tf =>
			{
				tf.Property(c => c.EmailAddress)
					.IsRequired()
					.HasColumnName("Email")
					.HasColumnType($"varchar({Email.EmailMaxLength})");
			});

			builder.HasOne(c => c.Address)
				.WithOne(a => a.Customer);

			builder.ToTable("Customers");
		}
	}
}
