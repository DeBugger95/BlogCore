using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogCore.Application.Dtos
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortContent { get; set; }

        private string postDate;
        public string PostDate
        {
            get { return postDate.Replace("/","-").Substring(0,10); }
            set { postDate = value; }
        }
        public long Views { get; set; }
        public int Recommend { get; set; }
        public int Difficulty { get; set; }
        public string ImgPath { get; set; }
        public bool IsActive { get; set; }
    }
}
