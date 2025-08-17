using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebGerarPDF
{
    public class FileObject
    {
        public byte[] MemoryStream { get; set; }
        public string ContantType { get; set; }
        public string NamePdfFile { get; set; }
    }
}
