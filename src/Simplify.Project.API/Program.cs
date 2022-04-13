using Simplify.Project.API.Repositories;
using Simplify.Project.Model;
using Microsoft.EntityFrameworkCore;

namespace Simplify.Project.API;

public static class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);
		
		builder.Services.AddControllers();
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddDbContext<SimplifyContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("SimplifyContext")));

		MapsterConfig.Config();

		if (builder.Environment.IsDevelopment())
		{
			builder.Services.AddSwaggerGen();
			builder.Services.AddSingleton<IClientRepository, MockClientRepository>();
			builder.Services.AddSingleton<IEstateRepository, MockEstateRepository>();
			builder.Services.AddSingleton<IHouseRepository, MockHouseRepository>();
			builder.Services.AddSingleton<IEntranceRepository, MockEntranceRepository>();
			builder.Services.AddSingleton<IApartmentRepository, MockApartmentRepository>();
			builder.Services.AddSingleton<IApartmentRelationRepository, MockApartmentRelationRepository>();
		}

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
