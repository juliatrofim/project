using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using MediaContentHSE.Data;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace tmcaf
{
    public static class MCCount
    {
        [FunctionName("MCCount")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            // parse query parameter
            string name = req.GetQueryNameValuePairs()
                .FirstOrDefault(q => string.Compare(q.Key, "name", true) == 0)
                .Value;

            // Get request body
            dynamic data = await req.Content.ReadAsAsync<object>();

            // Set name to query string or body data
            name = name ?? data?.name;

            RecordsContext context = new RecordsContext();

            var mcCount = context.MediaContents.Where(p => p.FileName == "a.mp4");
            


            return req.CreateResponse(HttpStatusCode.OK, "Mc count" + mcCount.Count());
        }
    }
}
