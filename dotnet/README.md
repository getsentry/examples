# Sample .NET applications integrated with Sentry

This repository include multiple samples using the .NET Sentry SDK. Differently than [the samples in the repository of the SDK](https://github.com/getsentry/sentry-dotnet/tree/master/samples) 
the samples here consume the SDK via NuGet.

When you clone this repository and build a sample, you will not be building the SDK.

All the features you'll see in action in these samples are ready to be used in your app.

# Each sample contains a description of what it demonstrates and how it works.

## Index:

### AspNetCoreMvc

ASP.NET Core MVC 5.0 running on the CoreCLR.

### AspNetCoreMvcSerilog 

ASP.NET Core MVC 5.0 running on the CoreCLR.
It also includes Serilog and Sentry's integration with Serilog.

### AspNetMvc5Ef6

ASP.NET MVC 5 with Entity Framework 6 targeting .NET Framework 4.6.2
This sample demonstrates the package that integrates with EF6 to send the queries as breadcrumbs.

### AspNetOwinWebApi

ASP.NET Web API 2 (OWIN)
Integration of an OWIN app with a middleware and a ExceptionHandler. Reads data from the request into the event. 
It also demonstrates how to capture exceptions that were handled by user code (`FirstChanceException`). Request data is also included as it's added via `SentryEventProcessor` in the `SentryMiddleware`.

### UwpCSharp

UWP (Universal Windows Platform) Desktop/Mobile/Xbox app, on .NET Native

### WpfDotNetCoreCSharp

WPF (Windows Presentation Foundation) Desktop Windows app, on .NET 5.0
