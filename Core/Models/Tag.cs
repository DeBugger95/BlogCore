using System;
using System.Collections.Generic;

namespace BlogCore.Core.Models
{
    public partial class Tag
    {
        public Tag()
        {
            PostTag = new HashSet<PostTag>();
        }

        public int Id { get; set; }
        public string TagName { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public bool IsActive { get; set; }
        public int SortNo { get; set; }

        public ICollection<PostTag> PostTag { get; set; }
    }
}
