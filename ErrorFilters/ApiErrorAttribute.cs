using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

/// <summary>
/// 错误统一处理
/// </summary>
public class ApiErrorAttribute: ExceptionFilterAttribute
{
    public override void OnException(System.Web.Http.Filters.HttpActionExecutedContext actionExecutedContext)
    {
        Exception exp = actionExecutedContext.Exception;

        // 取得由 API 返回的状态代码
        //HttpStatusCode StatusCode = actionExecutedContext.ActionContext.Response.StatusCode; 
        HttpStatusCode StatusCode = HttpStatusCode.InternalServerError;

        if (exp is NotImplementedException)
        {
            StatusCode = HttpStatusCode.NotImplemented;
        }
        else if (exp is TimeoutException)
        {
            StatusCode = HttpStatusCode.RequestTimeout;
        }
        else if (exp is UnauthorizedAccessException) {
            StatusCode = HttpStatusCode.Unauthorized;
        }
        else if (exp is NotSupportedException)
        {
            StatusCode = HttpStatusCode.NotImplemented;
        }
        

        // 重新打包回传的讯息
        actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(StatusCode, new AjaxResult
        {
            success = false,
            message = exp.Message,
            data = exp
            /*,data = new {
                ExceptionData = exp.Data,
                ExceptionSource = exp.Source,
                ExceptionTargetSite = exp.TargetSite,
                ExceptionStackTrace = exp.StackTrace
            }*/
        });
        

        base.OnException(actionExecutedContext);

        
    }



}