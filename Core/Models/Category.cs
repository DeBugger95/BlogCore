using System;
using System.Collections.Generic;

namespace BlogCore.Core.Models
{
    public partial class Category
    {
        public Category()
        {
            PostCategory = new HashSet<PostCategory>();
        }

        public int Id { get; set; }
        public string CategoryName { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public bool IsActive { get; set; }
        public int SortNo { get; set; }

        public ICollection<PostCategory> PostCategory { get; set; }
    }
}
