using System;
using System.Collections.Generic;

namespace BlogCore.Core.Models
{
    public partial class WareFile
    {
        public int WareId { get; set; }
        public int FileId { get; set; }

        public File File { get; set; }
        public Ware Ware { get; set; }
    }
}
