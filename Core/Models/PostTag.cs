using System;
using System.Collections.Generic;

namespace BlogCore.Core.Models
{
    public partial class PostTag
    {
        public int TagId { get; set; }
        public int PostId { get; set; }

        public Post Post { get; set; }
        public Tag Tag { get; set; }
    }
}
