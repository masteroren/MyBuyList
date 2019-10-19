using MyBuyListShare.Classes;
using MyBuyListShare.Models;
using System;
using System.Configuration;
using System.Net.Http;
using System.Text;
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

    private static async Task<T> PostURI<T>(Uri u, HttpContent c)
    {
        T response = default(T);

        using (var client = new HttpClient())
        {
            HttpResponseMessage result = await client.PostAsync(u, c);
            if (result.IsSuccessStatusCode)
            {
                Task<T> readTask = result.Content.ReadAsAsync<T>();
                readTask.Wait();

                response = readTask.Result;
            }
        }
        return response;
    }

    public static int Post<T>(string uri, T payload)
    {
        Uri u = new Uri(string.Format("{0}/api/{1}", ApiUrl, uri));
        string _payload = Json.JsonSerializer(payload);
        HttpContent c = new StringContent(Json.JsonSerializer(payload), Encoding.UTF8, "application/json");
        Task<int> t = Task.Run(() => PostURI<int>(u, c));
        t.Wait();
        return t.Result;
    }

    private static async Task<string> PutURI(Uri u, HttpContent c)
    {
        var response = string.Empty;
        using (var client = new HttpClient())
        {
            HttpResponseMessage result = await client.PutAsync(u, c);
            if (result.IsSuccessStatusCode)
            {
                response = result.StatusCode.ToString();
            }
        }
        return response;
    }

    public static void Put<T>(string uri, T payload)
    {
        Uri u = new Uri(string.Format("{0}/api/{1}", ApiUrl, uri));
        HttpContent c = new StringContent(Json.JsonSerializer(payload), Encoding.UTF8, "application/json");
        Task<string> t = Task.Run(() => PutURI(u, c));
        t.Wait();

        var result = t.Result;
    }

    private static async Task<string> DeleteURI(Uri u)
    {
        var response = string.Empty;
        using (var client = new HttpClient())
        {
            HttpResponseMessage result = await client.DeleteAsync(u);
            if (result.IsSuccessStatusCode)
            {
                response = result.StatusCode.ToString();
            }
        }
        return response;
    }

    public static Task<string> Delete(string uri)
    {
        Uri u = new Uri(string.Format("{0}/api/{1}", ApiUrl, uri));
        return Task.Run(() => DeleteURI(u));
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