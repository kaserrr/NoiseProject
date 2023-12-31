﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

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
