using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _5032_MVC_News.Models
{
    public class EmailFormModel
    {
        [Required, Display(Name = "Please enter Your name")]
        public string FromName { get; set; }
        [Required, Display(Name = "Please enter your email"), EmailAddress]
        public string FromEmail { get; set; }
        [Required]
        public string Message { get; set; }
    }
}