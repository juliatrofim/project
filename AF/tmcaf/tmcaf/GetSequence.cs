using System;
using System.IO;
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
    public static class GetSequence
    {
        [FunctionName("GetSequence")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            // parse UserID from query
            string UserID = req.GetQueryNameValuePairs()
                .FirstOrDefault(q => string.Compare(q.Key, "UserID", true) == 0)
                .Value;

            int age = GetAge(UserID);
            string gender = GetGender(UserID);
            DateTime date = DateTime.Now;

            RecordsContext context = new RecordsContext();

            var mcCount = context.MediaContents.Where(p => p.FileName == "a.mp4");

            string SASTokens = "";

            var TMClist = context.TargetMediaContents.Where(p => p.StartDate <= date && p.EndDate >= date && p.TargetGroup.Gender == gender && p.TargetGroup.StartAge <= age && p.TargetGroup.EndAge >= age);

            foreach (var TMC in TMClist)
            {
                string url = "http://localhost:7071/api/FileSas?filename=" + TMC.MediaContent.FileName;
                WebRequest getRequest = WebRequest.Create(url);
                Stream stream = getRequest.GetResponse().GetResponseStream();
                StreamReader streamReader = new StreamReader(stream);
                string token = streamReader.ReadLine();
                SASTokens += token + '\n';
            }

            return req.CreateResponse(HttpStatusCode.OK, SASTokens);
        }

        private static int GetAge(string userID)
        {
            return 15;
        }
        private static string GetGender(string userID)
        {
            return "male";
        }
    }
}
