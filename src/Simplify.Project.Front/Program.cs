using Simplify.Project.Front;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("Simplify.Project.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
		
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("Simplify.Project.ServerAPI"));

await builder.Build().RunAsync();
