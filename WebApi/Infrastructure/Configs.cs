/*******************************************************************************
 * 
 * 
 * 
 * 
*********************************************************************************/
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Configuration;
using System.IO;
using System.Text;
using System.Web;
namespace Infrastructure
{
    public class Configs
    {
        /// <summary>
        /// 根据Key获取Value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hostEnvironments">访问路劲的对象</param>
        /// <returns></returns>
        public static string GetValue(string key, IHostingEnvironment hostEnvironments)
        {
            var filePath = hostEnvironments.ContentRootPath + @"\Configs\system.json";   //项目根目录
            string value = "";
            using (StreamReader file = new StreamReader(filePath))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                JObject obj = (JObject)JToken.ReadFrom(reader);
                value = (string)obj["SystemConfig"][key];
            }
            return value;
        }

        /// <summary>
        /// 根据Key修改Value
        /// </summary>
        /// <param name="key">要修改的Key</param>
        /// <param name="value">要修改为的值</param>
        public static void SetValue(string key, string value, IHostingEnvironment hostEnvironments)
        {
            var filePath = hostEnvironments.ContentRootPath + @"\Configs\system.json";//项目根目录
            JObject jsonObject;
            using (StreamReader file = new StreamReader(filePath, Encoding.UTF8))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                jsonObject = (JObject)JToken.ReadFrom(reader);
                jsonObject["SystemConfig"][key] = value;
            }
            using (var writer = new StreamWriter(filePath))
            using (JsonTextWriter jsonwriter = new JsonTextWriter(writer))
            {
                jsonObject.WriteTo(jsonwriter);
            }
        }
    }
}
