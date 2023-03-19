using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ABCD_Mall_Online.MVC
{
    public static class GlobalVariables
    {
        public static HttpClient client = new HttpClient();

        static GlobalVariables()
        {
            client.BaseAddress = new Uri("https://localhost:44301/api/");
        }
    }
}
