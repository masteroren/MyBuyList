using System;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

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
        T results = default(T);

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

                results = readTask.Result;
            }
        }

        return results;
    }
}