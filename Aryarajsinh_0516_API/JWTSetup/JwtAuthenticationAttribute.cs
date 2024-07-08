using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Aryarajsinh_0516_API.JWTSetup
{

    public class JwtAuthenticationAttribute : AuthorizationFilterAttribute
    {

        public override void OnAuthorization(HttpActionContext actionContext)
        {

            try
            {

                var authHeader = actionContext.Request.Headers.Authorization;

                if (authHeader == null || authHeader.Scheme != "Bearer" || string.IsNullOrEmpty(authHeader.Parameter))

                {

                    SetUnauthorizedResponse(actionContext, "Token is missing or invalid.");

                    return;

                }

                var token = authHeader.Parameter.Trim();

                var userName = Authentication.ValidateToken(token);

                if (userName == null)
                {

                    SetUnauthorizedResponse(actionContext, "Invalid token.");

                    return;

                }

                

            }
            catch (Exception ex)
            {
                SetErrorResponse(actionContext, "An error occurred during token validation."+ex);
            }

        }

        private void SetUnauthorizedResponse(HttpActionContext actionContext, string message)

        {

            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, message);

        }

        private void SetErrorResponse(HttpActionContext actionContext, string message)

        {

            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.InternalServerError, message);

        }

    }
}
