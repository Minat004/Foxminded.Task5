﻿using Calculator;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Wrapper.Console;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(config =>
    {
        config.SetBasePath(Directory.GetCurrentDirectory());
        config.AddJsonFile("appsettings.json");
        config.AddEnvironmentVariables();
    })
    .ConfigureServices(services =>
    {
        services.AddTransient<App>();
        services.AddSingleton<IConsoleIO, ConsoleIO>();
    })
    .Build();

var app = host.Services.GetRequiredService<App>();

await app.RunAsync();