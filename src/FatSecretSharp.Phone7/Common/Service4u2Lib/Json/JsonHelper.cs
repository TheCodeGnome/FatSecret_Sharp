using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Service4u2.Json
{
    public static class JsonHelper
    {
        public static T Deserialize<T>(string jsonString)
        {
            using(MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(jsonString)))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));

                T val;
                try
                {
                    val = (T)serializer.ReadObject(ms);
                }
                catch
                {
                    val = default(T);                    
                }

                return val;
            }
        }
    }
}
