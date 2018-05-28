using System;
using System.Collections.Generic;

namespace BlogCore.Core.Models
{
    public partial class Post
    {
        public Post()
        {
            PostCategory = new HashSet<PostCategory>();
            PostFile = new HashSet<PostFile>();
            PostTag = new HashSet<PostTag>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortContent { get; set; }
        public string Content { get; set; }
        public DateTime PostDate { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public long Views { get; set; }
        public int Recommend { get; set; }
        public int Difficulty { get; set; }
        public bool IsActive { get; set; }
        public string ImgPath { get; set; }

        public ICollection<PostCategory> PostCategory { get; set; }
        public ICollection<PostFile> PostFile { get; set; }
        public ICollection<PostTag> PostTag { get; set; }
    }
}
