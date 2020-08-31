using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Ajax返回值格式
/// </summary>
[Serializable]
public class AjaxResult
{
    public bool success;
    public string message;
    public object data;

    /// <summary>
    /// 构造函数
    /// </summary>
    public AjaxResult()
    {
        success = false;
        message = string.Empty;
        data = null;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="success">调用成功否</param>
    /// <param name="message">消息</param>
    /// <param name="data">数据</param>
    public AjaxResult(bool success, string message = "", object data = null)
    {
        this.success = success;
        this.message = message;
        this.data = data;
    }
}
