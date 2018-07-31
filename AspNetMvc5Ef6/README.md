## Sentry sample for ASP.NET MVC 5 and Entity Framework 6

This sample targets .NET Framework 4.6.2

### Make sure to add your DSN to appSettings on `web.config`

This repository contains a sample of using Sentry with ASP.NET MVC 5 and Entity Framework 6.

Please note that the ASP.NET MVC 5 integration is not full yet, as it still lacks request related metadata. This will be added in a future commit.

All necessary code for the integration is inside `Global.asax.cs`

### In this sample we demonstrate the following:

* Adding custom data to be reported with the exception (See `HomeController.cs` `PostIndex` action, the catch block adding to `Data` property of the exception)
* Capturing an error which includes the EF6 queries executed in the request (see `HomeController.cs` action `ThrowEntityFramework`)
* Capturing unhandled exceptions via `Application_Error` on `Global.asax.cs`


Example event created by this sample:
![Example event in Sentry](Sentry%20EF6%20sample%20event.png)