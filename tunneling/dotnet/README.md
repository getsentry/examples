# Tunnel events through a .NET process

ASP.NET Core is the [second fastest web server](https://www.techempower.com/benchmarks/#section=data-r20&hw=ph&test=plaintext).

This example shows how you can use it to proxy events to Sentry.

### To run this example:

1. Download .NET: [dot.net](https://dot.net)
2. `$ dotnet run`
3. Run the test JS events from the parent directory [..](..)

### Version requirements:

As described in the `csproj` file, you need at least .NET Core 3.1 to run this, and at least C# 9 to compile it.
In other words:

1. Make sure you have at least **.NET Core 3.1 runtime** on the server
2. To build you need .NET 5 SDK which includes C# 9.
