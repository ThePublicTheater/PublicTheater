using System.Collections.Generic;
using System.Collections.Specialized;
using Newtonsoft.Json;

namespace PublicTheater.Custom.Mail2REST {
    public class Arguments {
        private NameValueCollection args = new NameValueCollection();

        public NameValueCollection NameValueCollection {
            get { return args; }
        }

        public Arguments Add(string name, string value) {
            args.Add(name, value);
            return this;
        }

        public Arguments Add(string name, bool value) {
            args.Add(name, value ? "true" : "false");
            return this;
        }

        public Arguments Add(string name, int value) {
            args.Add(name, value.ToString());
            return this;
        }

        public Arguments Add(string name, object obj) {
            if (obj != null) {
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.NullValueHandling = NullValueHandling.Ignore;
                string t = JsonConvert.SerializeObject(obj, Formatting.None, settings);
                args.Add(name, t);
            }
            return this;
        }

//        public Arguments Add(string name, Dictionary<string, string> parameters) {
//            if (parameters != null) {
//                foreach (KeyValuePair<string, string> kv in parameters) {
//                    args.Add(name + "[" + kv.Key + "]", kv.Value);
//                }
//            }
//            return this;
//        }

        public Arguments Add(string name, IEnumerable<string> parameters) {
            if(parameters != null) {
                int c = 0;
                foreach (string s in parameters) {
                    args.Add(name + "[" + c + "]", s);
                    ++c;
                }
            }
            return this;
        }

        private Arguments Add<T>(string name, IEnumerable<T> parameters) {
            if (parameters != null) {
                int c = 0;
                foreach (T s in parameters) {
                    args.Add(name + "[" + c + "]", s.ToString());
                    ++c;
                }
            }
            return this;
        }

    }
}
