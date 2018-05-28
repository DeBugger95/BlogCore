using System;
using System.Collections.Generic;

namespace BlogCore.Core.Models
{
    public partial class PostFile
    {
        public int PostId { get; set; }
        public int FileId { get; set; }

        public File File { get; set; }
        public Post Post { get; set; }
    }
}
