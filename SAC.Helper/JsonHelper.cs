using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace SAC.Json
{
    public static class JsonHelper
    {
        /// <summary>
        /// 格式化成Json字符串
        /// </summary>
        /// <param name="item">需要格式化的对象</param>
        /// <returns>Json字符串</returns>
        public static string ToJsonItem(this object item)
        {
            return JsonConvert.SerializeObject(item, new IsoDateTimeConverter());
        }

        /// <summary>
        /// 根据Json字符串返回泛型集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonString">Json字符串</param>
        /// <returns>泛型集合</returns>
        public static T FromJsonTo<T>(this string jsonString)
        {
            return JsonConvert.DeserializeObject<T>(jsonString, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });

        }
    }
}
