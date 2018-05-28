using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogCore.Application.Dtos
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        private string createDate;
        public string CreateDate
        {
            get { return createDate==null?null:createDate.Replace("/", "-").Substring(0, 10); }
            set { createDate = value; }
        }
        public bool IsActive { get; set; }
        public int SortNo { get; set; }
    }
}
