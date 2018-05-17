using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HearMe
{
    class HearMeApi
    {
        public string CallApi(NameValueCollection values)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://sandbox.robertcolca.me/");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            string json = JsonConvert.SerializeObject(values);

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var partialResult = streamReader.ReadToEnd();
                JsonStatusCut result = JsonConvert.DeserializeObject<JsonStatusCut>(partialResult);
                return result.message;
            }
        }


    }
}
