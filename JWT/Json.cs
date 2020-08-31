using Newtonsoft.Json;

namespace JWT
{
    /// <summary>
    /// Json 助手
    /// </summary>
    public sealed class Json
    {
        /// <summary>
        /// 将对象序列化为 Json 字符串
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>json字符串</returns>
        public static string Serialize(object obj)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            return JsonConvert.SerializeObject(obj, settings);
        }

        /// <summary>
        /// 将对象序列化为 Json 字符串
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>json字符串</returns>
        public static string Serialize(object obj, JsonSerializerSettings settings)
        {
            return JsonConvert.SerializeObject(obj, settings);
        }

        /// <summary>
        /// 解析 Json 字符串，生成T对象实体
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json字符串。例如：{"ID":"1","Name":"2"} </param>
        /// <returns>T对象实体</returns>
        public static T Deserialize<T>(string json) where T : class
        {
            JsonSerializer serializer = new JsonSerializer();
            System.IO.StringReader sr = new System.IO.StringReader(json);
            return serializer.Deserialize<T>(new JsonTextReader(sr));
        }

        /// <summary>
        /// 反序列化JSON到给定的匿名对象.
        /// </summary>
        /// <typeparam name="T">匿名对象类型</typeparam>
        /// <param name="json">json字符串</param>
        /// <param name="anonymousTypeObject">匿名对象</param>
        /// <returns>匿名对象</returns>
        public static T DeserializeAnonymousType<T>(string json, T anonymousTypeObject)
        {
            return JsonConvert.DeserializeAnonymousType(json, anonymousTypeObject);
        }
    }
}