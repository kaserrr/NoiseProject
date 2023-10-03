using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

class Program
{
    static void Main(string[] args)
    {
        var host = new WebHostBuilder()
            .UseKestrel()
            .ConfigureServices(services =>
            {
                services.AddControllers();
            })
            .Configure(app =>
            {
                app.UseRouting();
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
            })
            .Build();

        host.Run();
    }
}
