using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using System;

namespace Net6Demo.Tests;

internal class Net6DemoApplication : WebApplicationFactory<Program>
{
    private readonly string _environment;

    public Net6DemoApplication(string environment)
    {
        _environment = environment;
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.UseEnvironment(_environment);

        try
        {
            return base.CreateHost(builder);
        }
        catch (Exception)
        {
            throw;
        }
    }
}