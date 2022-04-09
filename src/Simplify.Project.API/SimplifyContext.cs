using Simplify.Project.Model;
using Microsoft.EntityFrameworkCore;

namespace Simplify.Project.API;
	
/// <summary>
/// Контекст базы данных
/// </summary>
public class SimplifyContext : DbContext
{
	/// <summary>
	/// Коллекция квартир
	/// </summary>
	public DbSet<Apartment> Apartments { get; set; } = null!;
	
	/// <summary>
	/// Коллекция отношений квартир и жильцов
	/// </summary>
	public DbSet<ApartmentRelation> ApartmentRelations { get; set; } = null!;

	/// <summary>
	/// Коллекция клиентов
	/// </summary>
	public DbSet<Client> Clients { get; set; } = null!;

	/// <summary>
	/// Коллекция работников
	/// </summary>
	public DbSet<Employee> Employees { get; set; } = null!;

	/// <summary>
	/// Коллекция подъездов
	/// </summary>
	public DbSet<Entrance> Entrances { get; set; } = null!;
	
	/// <summary>
	/// Коллеция комплексов
	/// </summary>
	public DbSet<Estate> Estates { get; set; } = null!;

	/// <summary>
	/// Коллекция домов
	/// </summary>
	public DbSet<House> Houses { get; set; } = null!;


	public SimplifyContext() { }
	public SimplifyContext(DbContextOptions<SimplifyContext> options) : base(options) { }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Apartment>(entity => 
		{
			entity.HasKey(x => x.Id);
			entity.Property(x => x.Number).IsRequired();
		});

		modelBuilder.Entity<ApartmentRelation>(entity => 
		{
			entity.HasKey(x => x.Id);
			entity.HasOne(x => x.Apartment).WithMany();
			entity.HasOne(x => x.Client).WithMany();

			entity.Property(x => x.RelationType).IsRequired();
			entity.Property(x => x.Created).IsRequired().HasDefaultValueSql("now()");

			entity.Navigation(x => x.Apartment).AutoInclude();
			entity.Navigation(x => x.Client).AutoInclude();
		});

		modelBuilder.Entity<Client>(entity =>
		{
			entity.HasKey(x => x.Id);
			entity.Property(x => x.Firstname).IsRequired();
			entity.Property(x => x.Lastname).IsRequired();
			entity.Property(x => x.Patronymic).IsRequired(false);

			entity.Property(x => x.Email).IsRequired();
			entity.Property(x => x.Phone).IsRequired();

			entity.HasMany(x => x.ApartmentsRelations).WithOne(x => x.Client);

			entity.Property(x => x.Created).IsRequired().HasDefaultValueSql("now()");
			entity.Property(x => x.IsBlocked).IsRequired().HasDefaultValueSql("false");
			entity.Property(x => x.Note).IsRequired(false);
		});

		modelBuilder.Entity<Employee>(entity =>
		{
			entity.HasKey(x => x.Id);
			entity.Property(x => x.Firstname).IsRequired();
			entity.Property(x => x.Lastname).IsRequired();
			entity.Property(x => x.Patronymic).IsRequired(false);

			entity.Property(x => x.Role).IsRequired();
			entity.Property(x => x.Login).IsRequired();
			entity.Property(x => x.Password).IsRequired();

			entity.Property(x => x.Created).IsRequired().HasDefaultValueSql("now()");
			entity.Property(x => x.IsBlocked).IsRequired().HasDefaultValueSql("false");
			entity.Property(x => x.Note).IsRequired(false);
		});

		modelBuilder.Entity<Entrance>(entity =>
		{
			entity.HasKey(x => x.Id);
			entity.Property(x => x.Number).IsRequired();

			entity.HasMany(x => x.Apartments).WithOne();

			entity.HasMany(x => x.ApartmentRelations).WithOne();
		});

		modelBuilder.Entity<Estate>(entity => 
		{
			entity.HasKey(x => x.Id);
			entity.Property(x => x.Name).IsRequired();
			entity.Property(x => x.Note).IsRequired(false);

			entity.HasMany(x => x.Houses).WithOne();
		});

		modelBuilder.Entity<House>(entity =>
		{
			entity.HasKey(x => x.Id);
			entity.Property(x => x.Street).IsRequired();
			entity.Property(x => x.Number).IsRequired();
			entity.Property(x => x.Building).IsRequired(false);

			entity.HasMany(x => x.Entrances).WithOne();
		});
	}
}
