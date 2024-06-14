using Hangfire.Dashboard;
using Microsoft.Owin;
using System.Collections.Generic;

namespace PizzaPlace.Web.App_Start
{
    public class HangFireAuthorizationFilter : IAuthorizationFilter
    {

        public bool Authorize(IDictionary<string, object> owinEnvironment)
        {
            // In case you need an OWIN context, use the next line,
            // `OwinContext` class is the part of the `Microsoft.Owin` package.
            dynamic context = new OwinContext(owinEnvironment);

            // Allow all authenticated users to see the Dashboard (potentially dangerous).
            return context.Authentication.User.Identity.IsAuthenticated;
        }

    }
}