using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using PublicTheater.Custom.Mail2REST;

namespace PublicTheater.Custom.Mail2REST
{
    public class Transport
    {
        private string api_key;
        private bool use_https;
        private string version;
        private string url = "api.emailcampaigns.net/2/REST/";

        public Transport(string api_key, bool use_https, string version, string apiUrl = "")
        {
            if (!string.IsNullOrEmpty(apiUrl))
            {
                url = apiUrl;
            }
            this.api_key = api_key;
            this.use_https = use_https;
        }

        private string MakePOSTCall(string command, NameValueCollection data)
        {
            System.Net.ServicePointManager.Expect100Continue = false;
            WebClient client = new WebClient();
            client.Headers.Add("Content-type", "application/x-www-form-urlencoded");
            client.Headers.Add("User-Agent", "mail2 .NET Wrapper" + version);
            NameValueCollection args = new NameValueCollection();
            args.Add("method", command);
            args.Add("key", api_key);
            args.Add(data);
            byte[] responseArray = client.UploadValues((use_https ? "https://" : "http://") + url, args);

            return Encoding.UTF8.GetString(responseArray);
        }

        public object MakeCall(string command, Arguments data)
        {
            string s = MakePOSTCall(command, data.NameValueCollection);
            return JsonConvert.DeserializeObject(s);
        }

        public object MakeCall(string command, Arguments args, Dictionary<string, string> optional_parameters)
        {
            args.Add("optionalParameters", optional_parameters);
            return MakeCall(command, args);
        }

        public object MakeCall(string command, Dictionary<string, string> optional_parameters)
        {
            return MakeCall(command, new Arguments(), optional_parameters);
        }

        public object MakeCall(string command)
        {
            return MakeCall(command, new Arguments());
        }
    }
}
