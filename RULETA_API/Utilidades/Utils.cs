using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RULETA_API.Utilidades
{
    public class Utils
    {
        internal JArray FormatJson(string json)
        {
            dynamic parsedJson = JsonConvert.DeserializeObject(json);
            return parsedJson;
        }
    }
}