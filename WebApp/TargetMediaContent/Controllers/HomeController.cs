using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.AspNetCore.Mvc;
using MediaContentHSE.Models;
using Microsoft.AspNetCore.Identity;
using MediaContentHSE.Data;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace MediaContentHSE.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public HomeController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
                      ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }
        public IActionResult Index()
        {
            var TG = _context.TargetGroups.Last();
            ViewData["TG"] = TG;
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult AddTMC(IFormFile Video, string StartDate, string EndDate,
            string Gender, string StartAge, string EndAge,
            string Button, string LikeMessage, string DislikeMessage, string Place)
        {
            TargetMediaContent tmc = new TargetMediaContent();
            tmc.TargetMediaContentId = Guid.NewGuid();
            tmc.StartDate = DateTime.ParseExact(StartDate, "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture);
            tmc.EndDate = DateTime.ParseExact(EndDate, "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture);
            tmc.SequenceNumber = 0;

            MediaContent mc = new MediaContent();
            mc.MediaContentId = Guid.NewGuid();
            mc.FileName = Video.FileName + "_" + mc.MediaContentId;
            tmc.MediaContentId = mc.MediaContentId;
            _context.Add(mc);

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=targetmediacontent;AccountKey=9j2qzQ0s+yq1K+GNVuHf1y6KN3hllqR7edNXX6Jw0kHzUrWVvioBEFAMvqEJEhDVnS3Fqtr8C3Ss3dyotEy7Iw==");
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("mediacontent");
            container.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
            CloudBlockBlob blob = container.GetBlockBlobReference(mc.FileName);

            using (var fileStream = Video.OpenReadStream())
            using (var ms = new MemoryStream())
            {
                fileStream.CopyTo(ms);
                var fileBytes = ms.ToArray();
                blob.UploadFromByteArrayAsync(fileBytes, 0, fileBytes.Length);
                blob.Properties.ContentType = Video.ContentType;
                blob.SetPropertiesAsync();
            }

            var existingTG = _context.TargetGroups.Where(g =>
            g.StartAge == Convert.ToInt32(StartAge, 10) &&
            g.EndAge == Convert.ToInt32(EndAge, 10) && g.Gender == Gender).FirstOrDefault();
            if (existingTG != null)
            {
                tmc.TargetGroupId = existingTG.TargetGroupId;
            }
            else
            {
                TargetGroup tg = new TargetGroup();
                tg.TargetGroupId = Guid.NewGuid();
                tg.StartAge = Convert.ToInt32(StartAge, 10);
                tg.EndAge = Convert.ToInt32(EndAge, 10);
                tg.Gender = Gender;
                tg.GroupName = Gender + StartAge + "-" + EndAge;
                tmc.TargetGroupId = tg.TargetGroupId;
                _context.Add(tg);
            }

            TargetMediaContentInterface ti = new TargetMediaContentInterface();
            ti.TargetMediaContentInterfaceId = Guid.NewGuid();
            ti.Button = Button;
            ti.Message = LikeMessage + "#" + DislikeMessage;
            if ((Place == "up") || (Place == "Up"))
            {
                ti.Place = 1;
            } else
            {
                ti.Place = 0;
            }
            tmc.TargetMediaContentInterfaceId = ti.TargetMediaContentInterfaceId;
            _context.Add(ti);
            _context.Add(tmc);

            _context.SaveChanges();
            return RedirectToAction("About");
        }
    }
}
