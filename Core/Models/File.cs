using System;
using System.Collections.Generic;

namespace BlogCore.Core.Models
{
    public partial class File
    {
        public File()
        {
            PostFile = new HashSet<PostFile>();
            WareFile = new HashSet<WareFile>();
        }

        public int Id { get; set; }
        public string Type { get; set; }
        public string Path { get; set; }
        public DateTime CreateDate { get; set; }

        public ICollection<PostFile> PostFile { get; set; }
        public ICollection<WareFile> WareFile { get; set; }
    }
}
