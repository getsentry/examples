using System;
using System.Web.Http;

public class HandledController : ApiController
{
    public void Get()
    {
        try
        {
            throw new Exception("handled");
        }
        catch
        {
            // This Exception is logged by AppDomain.FirstChanceException (See Startup.cs)
            // If there was logging here, the Exception could be captured with a Logger integration instead of FirstChanceException
        }
    }
}
