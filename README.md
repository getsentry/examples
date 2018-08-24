# Sample .NET applications integrated with Sentry

This repository include multiple samples using the .NET Sentry SDK. Differently than [the samples in the repository of the SDK](https://github.com/getsentry/sentry-dotnet/tree/master/samples) 
the samples here consume the SDK via NuGet.

When you clone this repository and build a sample, you will not be building the SDK.

All the features you'll see in action in these samples are ready to be used in your app.

# Each sample contains a description of what it demonstrates and how it works.

## Index:

### AspNetCoreMvcSerilog 

ASP.NET Core MVC 2.1 running on the CoreCLR.
It also includes Serilog which replaces the standard ASP.NET Core logging backend.
That means the default behavior (when not using Serilog) of getting the request's logs as breadcrumbs do not happen.
This will change once the Serilog Sink for Sentry is implemented.


### AspNetMvc5Ef6

ASP.NET MVC 5 with Entity Framework 6 targeting .NET Framework 4.6.2
This sample demonstrates the package that integrates with EF6 to send the queries as breadcrumbs.