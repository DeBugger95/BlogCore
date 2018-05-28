using System;
using System.Collections.Generic;

namespace BlogCore.Core.Models
{
    public partial class Ware
    {
        public Ware()
        {
            WareFile = new HashSet<WareFile>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        public int Degree { get; set; }
        public DateTime BuyDate { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public bool IsActive { get; set; }

        public ICollection<WareFile> WareFile { get; set; }
    }
}
