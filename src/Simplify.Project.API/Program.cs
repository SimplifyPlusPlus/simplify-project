using System.Text.Json.Serialization;
using Simplify.Project.API.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Simplify.Project.API;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);
		
		builder.Services.AddControllers()
			.AddJsonOptions(options =>
			{
				options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
			});
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddDbContext<SimplifyContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("SimplifyContext")));
		AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

		MapsterConfig.Config();

		builder.Services.AddSwaggerGen();
		builder.Services.AddScoped<IClientRepository, ClientRepository>();
		builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
		builder.Services.AddScoped<IEstateRepository, EstateRepository>();
		builder.Services.AddScoped<IHouseRepository, HouseRepository>();
		builder.Services.AddScoped<IEntranceRepository, EntranceRepository>();
		builder.Services.AddScoped<IApartmentRepository, ApartmentRepository>();
		builder.Services.AddScoped<IApartmentRelationRepository, ApartmentRelationRepository>();
		builder.Services.AddScoped<IEventRepository, EventRepository>();

		var app = builder.Build();

		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		app.UseHttpsRedirection();

		app.UseAuthorization();

		app.MapControllers();

		app.UseCors(option => option
			.AllowAnyMethod()
			.AllowAnyHeader()
			.SetIsOriginAllowed(origin => true)
			.AllowCredentials()
		);
		
		app.Run();
	}
}
