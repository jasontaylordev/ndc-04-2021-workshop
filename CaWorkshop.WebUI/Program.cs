﻿using CaWorkshop.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace CaWorkshop.WebUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var env = services.GetRequiredService<IWebHostEnvironment>();

                    if (env.IsDevelopment())
                    {
                        var context = services
                            .GetRequiredService<ApplicationDbContext>();

                        context.Database.Migrate();

                        ApplicationDbContextSeeder.Seed(context);
                    }
                }
                catch (Exception ex)
                {
                    var logger = services
                        .GetRequiredService<ILogger<Program>>();

                    logger.LogError(ex, "An error occurred while " +
                        "migrating or initializing the database.");

                    throw;
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
