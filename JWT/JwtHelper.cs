using JWT.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace JWT
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class JwtHelper
    {
        /// <summary>
        /// 
        /// </summary>
        private static string JwtSecret { get { return "jwt"; } }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="header"></param>
        /// <param name="payload"></param>
        /// <param name="secret"></param>
        /// <returns></returns>
        private static byte[] HS256(string header, string payload, string secret)
        {
            using (var hasher = new System.Security.Cryptography.HMACSHA256(Encoding.UTF8.GetBytes(secret)))
            {
                var bytes = Encoding.UTF8.GetBytes(string.Format("{0}.{1}", header, payload));
                return hasher.ComputeHash(bytes);
            }
        }

        /// <summary>
        /// 生成 Json Web Token
        /// </summary>
        /// <returns></returns>
        public static string CreateJWT(object payloadObj, string secret = null)
        {
            /*
             payload 标准中注册的声明 (建议但不强制使用) ：
                iss: jwt签发者
                sub: jwt所面向的用户
                aud: 接收jwt的一方
                exp: jwt的过期时间，这个过期时间必须要大于签发时间
                nbf: 定义在什么时间之前，该jwt都是不可用的.
                iat: jwt的签发时间
                jti: jwt的唯一身份标识，主要用来作为一次性token,从而回避重放攻击。
             */

            string header = Base64.EncodeUrl(Json.Serialize(new JwtHeader
            {
                typ = "JWT",
                alg = "HS256"
            }));
            string payload = Base64.EncodeUrl(Json.Serialize(payloadObj));

            string signature = Base64.EncodeUrl(HS256(header, payload, string.IsNullOrEmpty(secret) ? JwtSecret : secret));

            return string.Format(@"{0}.{1}.{2}", header, payload, signature);
        }
        /// <summary>
        /// 生成 Json Web Token
        /// </summary>
        /// <returns></returns>
        public static string CreateJwtDictionary(Dictionary<string, object> payloadDIC)
        {
            return CreateJWT(payloadDIC);
        }
        /// <summary>
        /// Json Web Token 签名验证 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static bool VerifyJwtSignature(string token, string secret = null)
        {
            if (string.IsNullOrEmpty(token))
            {
                return false;
            }

            var split = token.Split('.');
            if (split.Length != 3)
            {
                return false;
            }

            string signature = Base64.EncodeUrl(HS256(split[0], split[1], string.IsNullOrEmpty(secret) ? JwtSecret : secret));
            return signature == split[2];
        }
        /// <summary>
        /// 验证 Json Web Token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static bool VerifyJWT(string token,string secret = null)
        {
            if (string.IsNullOrEmpty(token))
            {
                return false;
            }

            var split = token.Split('.');
            if (split.Length != 3)
            {
                return false;
            }

            string signature = Base64.EncodeUrl(HS256(split[0], split[1], string.IsNullOrEmpty(secret) ? JwtSecret : secret));
            return signature == split[2];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static T DecodeJWT<T>(string token, string secret = null) where T : class, new()
        {
            if (string.IsNullOrEmpty(token))
            {
                return default(T); 
            }

            var split = token.Split('.');
            if (split.Length != 3)
            {
                return default(T);
            }

            string signature = Base64.EncodeUrl(HS256(split[0], split[1], string.IsNullOrEmpty(secret) ? JwtSecret : secret));
            return signature == split[2] ? Json.Deserialize<T>(Base64.DecodeUrl(split[1])) : default(T);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static Dictionary<string, object> DecodeJwtDictionary(string token,string secret=null)
        {
            if (string.IsNullOrEmpty(token))
            {
                return null;
            }

            var split = token.Split('.');
            if (split.Length != 3)
            {
                return null;
            }

            string signature = Base64.EncodeUrl(HS256(split[0], split[1], string.IsNullOrEmpty(secret) ? JwtSecret : secret));
            return signature == split[2] ? Json.Deserialize<Dictionary<string, object>>(Base64.DecodeUrl(split[1])) : null;
        }











    }
}