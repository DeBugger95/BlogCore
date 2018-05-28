using System;
using System.Collections.Generic;

namespace BlogCore.Core.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public int AccessFailedCount { get; set; }
        public bool? LockoutEnabled { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public bool IsActive { get; set; }
    }
}
