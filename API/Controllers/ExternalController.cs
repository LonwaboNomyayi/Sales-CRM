using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace API.Controllers
{
    public class ExternalController : ApiController 
    {
        //
        private readonly static HttpClient client = new HttpClient();
        public async Task<HttpResponseMessage> Get()
        {
            //client.BaseAddress = new Uri(ConfigurationManager.AppSettings.Get("ExternalApiEndpoint"));
            HttpResponseMessage response = await client.GetAsync(ConfigurationManager.AppSettings.Get("ExternalApiEndpoint"));
            return response;
        }
    }
}