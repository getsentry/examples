using System.ComponentModel.Design;
using System.Web.Http;

public class DefaultController : ApiController
{
    public void Get() => throw new CheckoutException("check it out");
}
