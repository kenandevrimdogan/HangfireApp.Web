using HangfireApp.Web.BackgroundJobs;
using HangfireApp.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HangfireApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> SignUp()
        {
            FireAndForgetJobs.EmailSendToUserJob("", "Welcome to Our Web Page");

            return View();
        }

        public async Task<IActionResult> PictureSave()
        {
            RecurringJobs.ReportingJobs();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PictureSave(IFormFile file)
        {
            string newFileName = String.Empty;

            if (file != null && file.Length > 0)
            {
                newFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/pictures", newFileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var stringJobId = DealayedJobs.AddWatermarkJob(newFileName, "www.hangfiretest.com");

                ContinuationsJobs.WriteWatermarkStatusJob(stringJobId, newFileName);
            }

            return View();
        }
    }
}
