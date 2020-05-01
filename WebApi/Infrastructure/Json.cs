/*******************************************************************************
 * 
 * 
 * 
 * 
*********************************************************************************/
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;

namespace Infrastructure
{
    public static class Json
    {
        public static dynamic ToDynamic(this DataTable dataTable)
        {
            return JsonConvert.DeserializeObject<ExpandoObject>(JsonConvert.SerializeObject(dataTable)) as dynamic;
        }

        public static List<dynamic> ToDynamicList(this DataTable dataTable)
        {

            var _list = new List<dynamic>();
            var _dataList = JsonConvert.DeserializeObject<dynamic[]>(JsonConvert.SerializeObject(dataTable));
            foreach (var _data in _dataList)
            {
                var json = JsonConvert.SerializeObject(_data);
                _list.Add(JsonConvert.DeserializeObject<ExpandoObject>(json) as dynamic);
            }
            return _list;
        }



        public static object ToJson(this string Json)
        {
            return Json == null ? null : JsonConvert.DeserializeObject(Json);
        }
        public static string ToJson(this object obj)
        {
            var timeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };
            return JsonConvert.SerializeObject(obj, timeConverter);
        }
        public static string ToJson(this object obj, string datetimeformats)
        {
            var timeConverter = new IsoDateTimeConverter { DateTimeFormat = datetimeformats };
            return JsonConvert.SerializeObject(obj, timeConverter);
        }
        public static T ToObject<T>(this string Json)
        {
            return Json == null ? default(T) : JsonConvert.DeserializeObject<T>(Json);
        }
        public static List<T> ToList<T>(this string Json)
        {
            return Json == null ? null : JsonConvert.DeserializeObject<List<T>>(Json);
        }
        public static DataTable ToTable(this string Json)
        {
            return Json == null ? null : JsonConvert.DeserializeObject<DataTable>(Json);
        }
        public static JObject ToJObject(this string Json)
        {
            return Json == null ? JObject.Parse("{}") : JObject.Parse(Json.Replace("&nbsp;", ""));
        }
    }
}
