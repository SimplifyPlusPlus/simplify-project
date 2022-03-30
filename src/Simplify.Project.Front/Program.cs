using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Simplify.Project.Front;

public static class Program
{
	public static async Task Main(string[] args)
	{
		var builder = WebAssemblyHostBuilder.CreateDefault(args);
		builder.RootComponents.Add<App>("#app");
		builder.RootComponents.Add<HeadOutlet>("head::after");

		if (builder.HostEnvironment.IsDevelopment())
		{
			var baseAddress = builder.Configuration.GetValue<string>("BaseAddressDevelopment") ?? builder.HostEnvironment.BaseAddress;
			builder.Services.AddScoped(_ => new HttpClient {BaseAddress = new Uri(baseAddress)});
		}

		await builder.Build().RunAsync();
	}
}

