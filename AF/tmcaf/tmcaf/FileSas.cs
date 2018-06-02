using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;

namespace tmcaf
{
    public static class FileSas
    {
        [FunctionName("FileSas")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            // parse query parameter
            string filename = req.GetQueryNameValuePairs()
                .FirstOrDefault(q => string.Compare(q.Key, "filename", true) == 0)
                .Value;

            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=targetmediacontent;AccountKey=9j2qzQ0s+yq1K+GNVuHf1y6KN3hllqR7edNXX6Jw0kHzUrWVvioBEFAMvqEJEhDVnS3Fqtr8C3Ss3dyotEy7Iw==");

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference("mediacontent");
            CloudBlockBlob blob = container.GetBlockBlobReference(filename);

            SharedAccessBlobPolicy sasConstraints = new SharedAccessBlobPolicy();
            sasConstraints.SharedAccessStartTime = DateTime.UtcNow.AddMinutes(-5);
            sasConstraints.SharedAccessExpiryTime = DateTime.UtcNow.AddHours(24);
            sasConstraints.Permissions = SharedAccessBlobPermissions.Read;

            string sasBlobToken = blob.GetSharedAccessSignature(sasConstraints);

            return filename == null
                ? req.CreateResponse(HttpStatusCode.BadRequest, "Please pass a name on the query string or in the request body")
                : req.CreateResponse(HttpStatusCode.OK, blob.Uri + sasBlobToken);

        }
    }
}
