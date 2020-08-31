using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

/// <summary>
/// 异常处理。找不到与请求 URI“http://localhost/lab20Api/TestManager/testApiErrorHandler”匹配的 HTTP 资源。
/// </summary>
public class ApiErrorHandler : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var response = await base.SendAsync(request, cancellationToken);
        return BuildApiResponse(request, response);
    }

    private static HttpResponseMessage BuildApiResponse(HttpRequestMessage request, HttpResponseMessage response)
    {
        bool IsNewResponse = false;
        HttpResponseMessage hrm = new HttpResponseMessage();
        dynamic content = null;
        try
        {

            if (response.IsSuccessStatusCode)
            {
                hrm = response;
            }
            else if (response.TryGetContentValue(out content))
            {
                AjaxResult data = new AjaxResult
                {
                    success = false
                };
                HttpError error = content as HttpError;
                if (error != null)
                {
                    content = null;
                    data.message = string.Concat(error.Message, error.ExceptionMessage, error.StackTrace);
                    data.data = error;
                }
                else if (content?.GetType().Name.Equals("AjaxResult"))
                {
                    data = content;
                }
                else
                {
                    data.message = JsonHelper.Serialize(content);
                }

                hrm = request.CreateResponse(response.StatusCode, data);
                IsNewResponse = true;
            }
            else
            {
                hrm = request.CreateResponse(HttpStatusCode.InternalServerError, new AjaxResult
                {
                    success = false,
                    message = "服务器上发生了错误。"
                });
                IsNewResponse = true;
            }
        }
        catch (Exception exp)
        {
            hrm = request.CreateResponse(HttpStatusCode.InternalServerError, new AjaxResult
            {
                success = false,
                message = exp.Message,
                data = exp
            });
            IsNewResponse = true;
        }

        if (IsNewResponse)
        {
            foreach (var header in response.Headers)
            {
                hrm.Headers.Add(header.Key, header.Value);
            }
        }
        return hrm;
    }

}
