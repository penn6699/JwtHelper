using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JWT.Entities
{
    /// <summary>
    /// 标准负载
    /// </summary>
    [Serializable]
    public class JwtStandardPayload
    {
        /// <summary>
        /// 签发者
        /// </summary>
        public string iss = null;
        /// <summary>
        /// 所面向的用户
        /// </summary>
        public string sub = null;
        /// <summary>
        /// 接收jwt的一方
        /// </summary>
        public string aud = null;
        /// <summary>
        /// 过期时间，这个过期时间必须要大于签发时间
        /// </summary>
        public DateTime exp;
        /// <summary>
        /// 定义在什么时间之前，该jwt都是不可用的.
        /// </summary>
        public string nbf = null;
        /// <summary>
        /// 签发时间
        /// </summary>
        public DateTime iat;
        /// <summary>
        /// 唯一身份标识，主要用来作为一次性token,从而回避重放攻击。
        /// </summary>
        public string jti = null;

    }
}