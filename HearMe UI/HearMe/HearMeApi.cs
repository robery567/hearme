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
        public string SendData(NameValueCollection values)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://127.0.0.1/");
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
                var result = streamReader.ReadToEnd();
                return result.ToString();
            }
        }


    }
}
