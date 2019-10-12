using System.Text;

namespace MyBuyListShare.Classes
{
    public class Json
    {
        public static string JsonSerializer<T>(T t)
        {
            System.Runtime.Serialization.Json.DataContractJsonSerializer ser = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(T));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            ser.WriteObject(ms, t);
            string jsonString = Encoding.UTF8.GetString(ms.ToArray());
            ms.Close();
            return jsonString;
        }

        public static T JsonDeserializer<T>(string t)
        {
            if (t == null)
            {
                return default(T);
            }

            System.Runtime.Serialization.Json.DataContractJsonSerializer ser = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(T));
            System.IO.MemoryStream ms = new System.IO.MemoryStream(Encoding.Unicode.GetBytes(t));
            var result = ser.ReadObject(ms);
            ms.Close();
            return (T)result;
        }
    }
}
