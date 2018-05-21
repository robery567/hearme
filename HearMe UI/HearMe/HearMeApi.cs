using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HearMe
{
    class HearMeApi
    {
        public string CallApi(Dictionary<string, string> values)
        {
            WebRequest request = WebRequest.Create("http://sandbox.robertcolca.me/request");
            request.Method = "POST";
            byte[] json = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(values));
            request.ContentType = "application/json";
            request.ContentLength = json.Length;

            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(json, 0, json.Length);
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(responseStream))
                        {
                            string responseFromServer = reader.ReadToEnd();
                            MessageBox.Show(responseFromServer);
                            JsonStatusCut result = JsonConvert.DeserializeObject<JsonStatusCut>(responseFromServer);
                            return result.response;
                        }
                    }
                }
            }
        }
    }
}
