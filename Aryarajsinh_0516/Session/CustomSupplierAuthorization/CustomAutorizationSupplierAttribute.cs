using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Aryarajsinh_0516.Session.CustomSupplierAuthorization
{
    public class CustomAutorizationSupplierAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)

        {

            if (SessionHelper.UserId != 0 && SessionHelper.Email != "" && SessionHelper.Role == "Supplier")

            {

                return true;

            }

            return false;

        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)

        {

            filterContext.Result = new RedirectToRouteResult(

            new RouteValueDictionary

            {

                    { "controller", "Login" },

                    { "action", "Login" }

            });

        }
    }
}