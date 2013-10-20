using System;

namespace SmartHealth.Web.Areas.Admin.Models
{
    public class FileViewModel
    {
        public string FileName { get; set; }
        public string Size { get; set; }
        public string Height { get; set; }
        public string Width { get; set; }
        public DateTime LastWriteDate { get; set; }
        public string Url { get; set; }
    }
}