using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace MyMVCWebsite.Filters
{
    public class AuthenticationAttribute : ActionFilterAttribute, IAuthenticationFilter
    {
        private bool _auth;
        // This filter is responsible for checking if the users are loged in or not
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            _auth = (filterContext.ActionDescriptor.GetCustomAttributes(typeof(OverrideAuthenticationAttribute), true).Length == 0);
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            var user = filterContext.HttpContext.User;

            if (user == null || !user.Identity.IsAuthenticated)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }
    }
}