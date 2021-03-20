using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FileDemo.Models
{
    public class Attachment
    {
        [Key]
        public int id { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string FileName { set; get; }
        [Column(TypeName = "nvarchar(50)")]
        public string Description { set; get; }
        [Column(TypeName = "varbinary(MAX)")]
        public byte[] attachment { set; get; }
    }
}
