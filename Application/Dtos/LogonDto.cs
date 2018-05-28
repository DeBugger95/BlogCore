using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogCore.Application.Dtos
{
    public class LogonDto
    {
        [Required]
        [EmailAddress]
        public string UserName { get; set; }
        [Required]
        [MinLength(8)]
        [RegularExpression(@"^(?![0-9]+$)(?![a-zA-Z]+$)[0-9A-Za-z]{2,}$")]
        public string PassWord { get; set; }
    }
}
