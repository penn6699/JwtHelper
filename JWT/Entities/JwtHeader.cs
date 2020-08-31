using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JWT.Entities
{
    /// <summary>
    /// 头部
    /// </summary>
    [Serializable]
    public class JwtHeader
    {
        /// <summary>
        /// 声明令牌的类型，JWT令牌默认统一写为JWT
        /// </summary>
        public string typ = "JWT";
        /// <summary>
        /// 声明加密的算法 通常直接使用 HMAC SHA256 默认为HMAC SHA256（写为HS256）
        /// </summary>
        public string alg = "HS256";
    }

}