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
        public string CallApi(NameValueCollection values)
        {
            string json = JsonConvert.SerializeObject(values);

            WebRequest request = WebRequest.Create("http://sandbox.robertcolca.me/request");
            request.Method = "GET";
            using (WebResponse response = request.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    var streamReader = new StreamReader(stream);
                    var partialResult = streamReader.ReadToEnd();
                    JsonStatusCut result = JsonConvert.DeserializeObject<JsonStatusCut>(partialResult);
                    return result.response;
                }
            }
        }


    }
}
