using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BASMVC.ViewModel
{
    public class MessageVM
    {
        [Required(ErrorMessage = "Povinný parameter")]
        public string Name {get;set; }

        public string Phone { get; set; }
        [Required(ErrorMessage = "Povinný parameter")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Povinný parameter")]
        public string Message { get; set; }
    }
}