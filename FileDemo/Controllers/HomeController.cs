using FileDemo.Models;
using FileDemo.SpecialClass;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FileDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly AppDBContext _appContext;

        public HomeController(ILogger<HomeController> logger,
             IWebHostEnvironment hostingEnvironment,
             AppDBContext appContext)
        {
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
            _appContext = appContext;
        }

        public IActionResult Index()
        {
            AttachmentViewModel model = new AttachmentViewModel();
            model.attachments = _appContext.attachments.Select(m => m).ToList();
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(AttachmentViewModel model)
        {
            if (model.attachment != null)
            {
                var uniqueFileName = SPClass.CreateUniqueFileExtension(model.attachment.FileName);
                var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "attachment");
                var filePath = Path.Combine(uploads, uniqueFileName);
                model.attachment.CopyTo(new FileStream(filePath, FileMode.Create));


                Attachment attachment = new Attachment();
                attachment.FileName = uniqueFileName;
                attachment.Description = model.Description;
                attachment.attachment = SPClass.GetByteArrayFromImage(model.attachment);

                _appContext.attachments.Add(attachment);
                _appContext.SaveChanges();
            }
            return RedirectToAction("index");
        }

        [HttpGet]
        public FileResult GetFileResultDemo(string filename)
        {
            string path = "/attachment/" + filename;
            string contentType = SPClass.GetContenttype(filename);
            return File(path, contentType);
        }

        [HttpGet]
        public FileContentResult GetFileContentResultDemo(string filename)
        {
            string path = "wwwroot/attachment/" + filename;
            byte[] fileContent = System.IO.File.ReadAllBytes(path);
            string contentType = SPClass.GetContenttype(filename);
            return new FileContentResult(fileContent, contentType);
        }
        [HttpGet]
        public async Task<FileContentResult> GetFileContentResultDemoAsync(string filename)
        {
            string path = "wwwroot/attachment/" + filename;
            byte[] data = await System.IO.File.ReadAllBytesAsync(path);
            string contentType = SPClass.GetContenttype(filename);
            return new FileContentResult(data, contentType);
        }
        [HttpGet]
        public FileStreamResult GetFileStreamResultDemo(string filename) //download file
        {
            string path = "wwwroot/attachment/" + filename;
            var stream = new MemoryStream(System.IO.File.ReadAllBytes(path));
            string contentType = SPClass.GetContenttype(filename);
            return new FileStreamResult(stream, new MediaTypeHeaderValue(contentType))
            {
                FileDownloadName = filename
            };
        }

        [HttpGet]
        public VirtualFileResult GetVirtualFileResultDemo(string filename)
        {
            string path = "attachment/" + filename;
            string contentType = SPClass.GetContenttype(filename);
            return new VirtualFileResult(path, contentType);
        }

        [HttpGet]
        public PhysicalFileResult GetPhysicalFileResultDemo(string filename)
        {
            string path = "/wwwroot/attachment/" + filename;
            string contentType = SPClass.GetContenttype(filename);
            return new PhysicalFileResult(_hostingEnvironment.ContentRootPath
                + path, contentType);
        }

        [HttpGet]
        public ActionResult GetAttachment(int ID)
        {

            byte[] fileContent;
            string fileName = string.Empty;

            Attachment attachment = new Attachment();

             attachment = _appContext.attachments.Select(m=>m).Where(m=>m.id == ID).FirstOrDefault();

            string contentType = SPClass.GetContenttype(attachment.FileName);
            fileContent = (byte[])attachment.attachment;


            return new FileContentResult(fileContent, contentType);
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
    }

    
}
