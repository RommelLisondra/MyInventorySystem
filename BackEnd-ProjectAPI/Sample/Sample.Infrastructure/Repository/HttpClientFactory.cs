
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Sample.Infrastructure.Repository
{
    public class HttpClientFactory //: IHttpClientFactory
    {
        //    static readonly IDictionary<string, HttpClient> cache = new ConcurrentDictionary<string, HttpClient>();
        //    private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(HttpClientFactory));

        //    public HttpClient Create(string endpoint)
        //    {
        //        if (cache.TryGetValue(endpoint, out HttpClient client))
        //        {
        //            logCentral.Debug("HttpClient from cache - " + endpoint);
        //            return client;
        //        }

        //        logCentral.Debug("HttpClient created - " + endpoint);
        //        client = new HttpClient
        //        {
        //            BaseAddress = new Uri(endpoint),
        //        };
        //        cache[endpoint] = client;

        //        return client;
        //    }

        //    public HttpClient Create(Constant.ApiConfig apiConfig)
        //    {
        //        string endpoint = ConfigurationManager.AppSettings[apiConfig.ToString() + "ApiBaseUrl"].ToString();
        //        if (cache.TryGetValue(endpoint, out HttpClient client))
        //        {
        //            logCentral.Debug("HttpClient from cache - " + endpoint);
        //            return client;
        //        }

        //        logCentral.Debug("HttpClient created - " + endpoint);
        //        client = new HttpClient
        //        {
        //            BaseAddress = new Uri(endpoint),
        //        };
        //        cache[endpoint] = client;


        //        client.DefaultRequestHeaders.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
        //            "Basic",
        //            Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}",
        //                ConfigurationManager.AppSettings[apiConfig.ToString() + "ApiUserName"].ToString(),
        //                ConfigurationManager.AppSettings[apiConfig.ToString() + "ApiPassword"].ToString()))));

        //        return client;
        //    }

        //    public T GetService<T>(Constant.ApiConfig apiConfig, string servicePath, string uriParam, string propertyPath = "")
        //    {
        //        try
        //        {
        //            var apiClient = Create(apiConfig);
        //            logCentral.Debug("GetAsync - " + servicePath);
        //            HttpResponseMessage responseResult = apiClient.GetAsync(
        //                string.Format("{0}?{1}", servicePath, uriParam)).Result;
        //            if (string.IsNullOrEmpty(propertyPath))
        //            {
        //                logCentral.Debug("ReadAsAsync - " + servicePath);
        //                return responseResult.Content.ReadAsAsync<T>().Result;
        //            }
        //            else
        //            {
        //                logCentral.Debug("ReadAsStringAsync - " + servicePath);
        //                var responseContent = responseResult.Content.ReadAsStringAsync().Result;
        //                logCentral.Debug("JObject.Parse - ");
        //                JObject content = JObject.Parse(responseContent);
        //                logCentral.Debug("SelectToken - " + propertyPath);
        //                JToken selectedContent = content.SelectToken(propertyPath);
        //                if (selectedContent != null)
        //                {
        //                    logCentral.Debug("DeserializeObject - ");
        //                    if (typeof(T).IsGenericType &&
        //                        typeof(T).GetGenericTypeDefinition() == typeof(List<>) &&
        //                        !(selectedContent is JArray))
        //                    {
        //                        return JsonConvert.DeserializeObject<T>("[" + selectedContent.ToString() + "]");
        //                    }
        //                    return JsonConvert.DeserializeObject<T>(selectedContent.ToString());
        //                }
        //                else
        //                    return default;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception(string.Format("Error connecting to {0} [{1}]", apiConfig, servicePath), ex);
        //        }
        //    }

        //    public T PostService<T>(Constant.ApiConfig apiConfig, string servicePath, string paramJsonObject, string propertyPath = "")
        //    {
        //        try
        //        {
        //            var apiClient = Create(apiConfig);
        //            logCentral.Debug("PostAsync - " + servicePath);
        //            HttpResponseMessage responseResult = apiClient.PostAsync(
        //                string.Format("{0}{1}", apiClient.BaseAddress.ToString(), servicePath),
        //                new StringContent(paramJsonObject, Encoding.UTF8, "application/json")).Result;
        //            if (string.IsNullOrEmpty(propertyPath))
        //            {
        //                var responseContent = responseResult.Content;
        //                logCentral.Debug("ReadAsAsync - " + servicePath + "::" + responseContent);
        //                return responseResult.Content.ReadAsAsync<T>().Result;
        //            }
        //            else
        //            {
        //                logCentral.Debug("ReadAsStringAsync - " + servicePath);
        //                var responseContent = responseResult.Content.ReadAsStringAsync().Result;
        //                logCentral.Debug("JObject.Parse - ");
        //                JObject content = JObject.Parse(responseContent);
        //                logCentral.Debug("SelectToken - " + propertyPath);
        //                JToken selectedContent = content.SelectToken(propertyPath);
        //                if (selectedContent != null)
        //                {
        //                    logCentral.Debug("DeserializeObject - ");
        //                    if (typeof(T).IsGenericType && 
        //                        typeof(T).GetGenericTypeDefinition() == typeof(List<>) &&
        //                        !(selectedContent is JArray))
        //                    {
        //                        return JsonConvert.DeserializeObject<T>("[" + selectedContent.ToString() + "]");
        //                    }
        //                    return JsonConvert.DeserializeObject<T>(selectedContent.ToString());
        //                }
        //                else
        //                  return default;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            logCentral.Error("PostService " + ex);
        //            throw new Exception(string.Format("Error connecting to {0} [{1}]", apiConfig, servicePath), ex);
        //        }
        //    }
    }
}
