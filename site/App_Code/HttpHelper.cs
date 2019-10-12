using MyBuyListShare.Models;
using System;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

public class Response<T>
{
    public MetaData metadata { get; set; }
    public T[] results { get; set; }
}

/// <summary>
/// Summary description for HttpHelper
/// </summary>
public class HttpHelper
{
    private static string ApiUrl = ConfigurationManager.AppSettings["ApiUrl"];

    public HttpHelper()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static T Get<T>(string uri)
    {
        T response = default(T);

        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(string.Format("{0}/api/", ApiUrl));
            Task<HttpResponseMessage> responseTask = client.GetAsync(uri);
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                Task<T> readTask = result.Content.ReadAsAsync<T>();
                readTask.Wait();

                response = readTask.Result;
            }
        }

        return response;
    }

    public static Response<T> GetMeny<T>(string uri)
    {
        Response<T> response = default(Response<T>);

        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(string.Format("{0}/api/", ApiUrl));
            Task<HttpResponseMessage> responseTask = client.GetAsync(uri);
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                Task<Response<T>> readTask = result.Content.ReadAsAsync<Response<T>>();
                readTask.Wait();

                response = readTask.Result;
            }
        }

        return response;
    }

    public static string JsonSerializer<T>(T t)
    {
        System.Runtime.Serialization.Json.DataContractJsonSerializer ser = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(T));
        System.IO.MemoryStream ms = new System.IO.MemoryStream();
        ser.WriteObject(ms, t);
        string jsonString = System.Text.Encoding.UTF8.GetString(ms.ToArray());
        ms.Close();
        return jsonString;
    }
}