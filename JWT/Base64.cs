using System;
using System.Text;

namespace JWT
{
    /// <summary>
    /// Base64 编码助手
    /// </summary>
    public sealed class Base64
    {
        private static string _ToBase64String(string str)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(str));
        }
        private static string _FromBase64String(string str64)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(str64));
        }
        /// <summary>
        /// Base64 编码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Encode(byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }
        /// <summary>
        /// Base64 编码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Encode(string str)
        {
            return _ToBase64String(str);
        }
        /// <summary>
        /// Base64 解码
        /// </summary>
        /// <param name="str64"></param>
        /// <returns></returns>
        public static string Decode(string str64)
        {
            return _FromBase64String(str64);
        }
        /// <summary>
        /// Base64URL 编码（可用于在网络中安全顺畅传输）
        /// </summary>
        /// <param name="bytes)"></param>
        /// <returns></returns>
        public static string EncodeUrl(byte[] bytes)
        {
            /*
             1、明文使用Base64进行加密 
             2、在Base64的基础上进行以下编码：
               2.1、去除尾部的"="
               2.2、把"+"替换成"-"
               2.3、把"/"替换成"_"
             */
            return Convert.ToBase64String(bytes).TrimEnd('=').Replace('+', '-').Replace('/', '_');
        }
        /// <summary>
        /// Base64URL 编码（可用于在网络中安全顺畅传输）
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string EncodeUrl(string str)
        {
            /*
             1、明文使用Base64进行加密 
             2、在Base64的基础上进行以下编码：
               2.1、去除尾部的"="
               2.2、把"+"替换成"-"
               2.3、把"/"替换成"_"
             */
            return _ToBase64String(str).TrimEnd('=').Replace('+', '-').Replace('/', '_');
        }
        /// <summary>
        /// Base64URL 解码
        /// </summary>
        /// <param name="str64"></param>
        /// <returns></returns>
        public static string DecodeUrl(string str64)
        {
            /*
             1、把 Base64URL 的编码做如下解码：
                   1.1、把"-"替换成"+"
                   1.2、把"_"替换成"/"
                   1.3、 result=(计算BASE64URL编码长度)%4 
                      result为0，不做处理 
                      result为2，字符串添加"==" 
                      result为3，字符串添加"="

                  2、使用Base64解码密文，得到原始的明文

             */
            var b64 = str64.Replace('_', '/').Replace('-', '+');
            switch (b64.Length % 4)
            {
                case 2:
                    b64 += "==";
                    break;
                case 3:
                    b64 += "=";
                    break;
            }
            return _FromBase64String(b64);
        }

    }
}