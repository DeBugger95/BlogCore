using System;
using System.Collections.Generic;

namespace BlogCore.Core.Models
{
    public partial class PostCategory
    {
        public int PostId { get; set; }
        public int CategoryId { get; set; }

        public Category Category { get; set; }
        public Post Post { get; set; }
    }
}
