using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyMVCWebsite.Filters
{
    public class ErrorAttribute : FilterAttribute, IExceptionFilter
    {
        /// <summary>
        /// Custom error handeling filter, that is responsible for redirecting to a an error page according to error code
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled && filterContext.Exception is HttpException)
            {

                HttpException httpException = (HttpException)(filterContext.Exception);

                switch (httpException.GetHttpCode())
                {
                    case 400:
                        filterContext.Result = new RedirectResult(ConfigurationManager.AppSettings["BadRequestErroPage"]);
                        break;
                    case 500:
                        filterContext.Result = new RedirectResult(ConfigurationManager.AppSettings["InternalServerErrorPage"]);
                        break;

                    default:
                        filterContext.Result = new RedirectResult(ConfigurationManager.AppSettings["BadRequestErroPage"]);
                    
                        break;
                }
                filterContext.ExceptionHandled = true;
            }
        }
    }
}