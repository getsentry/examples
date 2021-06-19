using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

var client = new HttpClient();
WebHost.CreateDefaultBuilder(args).Configure(a => 
    a.Run(async context =>
    {
        context.Request.EnableBuffering();
        using var reader = new StreamReader(context.Request.Body);
        var header = await reader.ReadLineAsync();
        var headerJson = JsonSerializer.Deserialize<Dictionary<string, object>>(header);
        if (headerJson.TryGetValue("dsn", out var dsnString) 
            && Uri.TryCreate(dsnString.ToString(), UriKind.Absolute, out var dsn))
        {
            var projectId = dsn.AbsolutePath.Trim('/');
            context.Request.Body.Position = 0;
            await client.PostAsync($"https://{dsn.Host}/api/{projectId}/envelope/",
                new StreamContent(context.Request.Body));
        }
    })).Build().Run();
