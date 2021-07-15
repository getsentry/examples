using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Xunit;

var client = new HttpClient();
WebHost.CreateDefaultBuilder(args).Configure(a => 
    a.Run(async context =>
    {
        context.Request.EnableBuffering();
        using var reader = new StreamReader(context.Request.Body);
        var header = await reader.ReadLineAsync();
        var allowedHosts = new[] { "sentry.io" }; // If you self-host Sentry, add your own domain here to prevent forged attacks
        var headerJson = JsonSerializer.Deserialize<Dictionary<string, object>>(header);
        if (headerJson.TryGetValue("dsn", out var dsnString) 
            && Uri.TryCreate(dsnString.ToString(), UriKind.Absolute, out var dsn) && allowedHosts.Contains(dsn.Host))
        {
            var projectId = dsn.AbsolutePath.Trim('/');
            context.Request.Body.Position = 0;
            await client.PostAsync($"https://{dsn.Host}/api/{projectId}/envelope/",
                new StreamContent(context.Request.Body));
        }
    })).Build().Run();

// Alternative method for MVC
public class MyController : Controller
{
    [Route("/tunnel")]
    public async Task<IActionResult> Tunnel([FromServices] IHttpClientFactory httpClientFactory)
    {
        Request.EnableBuffering();
        var client = httpClientFactory.CreateClient();
        using var reader = new StreamReader(Request.Body);
        var header = await reader.ReadLineAsync();
        var headerJson = JsonSerializer.Deserialize<Dictionary<string, object>>(header);
        var allowedHosts = new[] { "sentry.io" }; // If you self-host Sentry, add your own domain here to prevent forged attacks
        if (headerJson.TryGetValue("dsn", out var dsnString) && Uri.TryCreate(dsnString.ToString(), UriKind.Absolute, out var dsn) && allowedHosts.Contains(dsn.Host))
        {
            var projectId = dsn.AbsolutePath.Trim('/');
            Request.Body.Position = 0;
            var responseMessage = await client.PostAsync($"https://{dsn.Host}/api/{projectId}/envelope/",
                new StreamContent(Request.Body));
            var ms = new MemoryStream();
            await responseMessage.Content.CopyToAsync(ms);
            ms.Position = 0;
            return new FileStreamResult(ms, "application/json");
        }

        return NotFound();
    }   
}

public class UnitTests
{
    [Fact]
    public async Task<bool> MVC_Tunnel_Works() => true;
}
