﻿using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Shiny.Helper
{
    /// <summary>
    /// json序列化
    /// </summary>
    public class JsonConvertHelper
    {
        /// <summary>
        /// body转json
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static string BodyToJson(HttpContext httpContext)
        {
            httpContext.Request.Body.Position = 0;
            var reader = new StreamReader(httpContext.Request.Body);
            try
            {
                return reader.ReadToEndAsync().Result;
            }
            catch (Exception ex)
            {

                return "";
            }


        }

        /// <summary>
        /// 转Json
        /// </summary>
        /// <param name="code"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static string ToJson(object result)
        {
            return JsonConvert.SerializeObject(result);
        }


        /// <summary>
        /// 转json忽略null值
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static string ToJsonIgnoreNull(object result)
        {
            JsonSerializerSettings jss = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            return JsonConvert.SerializeObject(result, jss);
        }

        /// <summary>
        /// 转换对象为JSON格式数据
        /// </summary>
        /// <typeparam name="T">类</typeparam>
        /// <param name="obj">对象</param>
        /// <returns>字符格式的JSON数据</returns>
        public static string GetJSON<T>(object obj)
        {
            string result = string.Empty;
            try
            {
                System.Runtime.Serialization.Json.DataContractJsonSerializer serializer =
                new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(T));
                using MemoryStream ms = new MemoryStream();
                serializer.WriteObject(ms, obj);
                result = System.Text.Encoding.UTF8.GetString(ms.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        /// <summary>
        /// 转换List<T>的数据为JSON格式
        /// </summary>
        /// <typeparam name="T">类</typeparam>
        /// <param name="vals">列表值</param>
        /// <returns>JSON格式数据</returns>
        public static string JSON<T>(List<T> vals)
        {
            System.Text.StringBuilder st = new System.Text.StringBuilder();
            try
            {
                System.Runtime.Serialization.Json.DataContractJsonSerializer s = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(T));

                foreach (T city in vals)
                {
                    using MemoryStream ms = new MemoryStream();
                    s.WriteObject(ms, city);
                    st.Append(System.Text.Encoding.UTF8.GetString(ms.ToArray()));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return st.ToString();
        }
        /// <summary>
        /// JSON格式字符转换为T类型的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonStr"></param>
        /// <returns></returns>
        public static T ParseFormByJson<T>(string jsonStr)
        {

            using MemoryStream ms =
            new MemoryStream(System.Text.Encoding.UTF8.GetBytes(jsonStr));
            System.Runtime.Serialization.Json.DataContractJsonSerializer serializer =
            new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(T));
            return (T)serializer.ReadObject(ms);
        }

        /// <summary>
        /// JSON格式字符列表转换为lIST<T>类型的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonStr"></param>
        /// <returns></returns>
        public static List<T> ParseFormByJson<T>(List<string> jsonList)
        {
            List<T> Tenity = new List<T>();

            foreach (var item in jsonList)
            {
                using MemoryStream ms =
            new MemoryStream(System.Text.Encoding.UTF8.GetBytes(item));
                System.Runtime.Serialization.Json.DataContractJsonSerializer serializer =
            new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(T));
                Tenity.Add((T)serializer.ReadObject(ms));
            }
            return Tenity;
        }
    }
}
