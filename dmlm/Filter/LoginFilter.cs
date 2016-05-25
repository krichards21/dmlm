using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace dmlm.Filter
{
    public class LoginFilter: ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            base.OnActionExecuting(actionContext);
            try
            {
                var accessToken = actionContext.Request.Headers.GetValues("AccessToken").First();
                var userID = actionContext.Request.Headers.GetValues("UserID").First();
                if (!new Models.UserModel().CheckAccessToken(accessToken, userID, new dmlm.Models.ApplicationDbContext()))
                {
                    actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Forbidden, new UnauthorizedAccessException());
                }
            }
            catch (Exception ex)
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Forbidden, new UnauthorizedAccessException());
            }
        }
    }
}