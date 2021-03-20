using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileDemo.Models
{
    public class AttachmentViewModel
    {
        public string FileName { set; get; }
        public string Description { set; get; }
        public IFormFile attachment { set; get; }

        public List<Attachment> attachments { get; set; }

        
    }
}
