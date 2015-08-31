using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebChat.WebService.Models
{
    public class MessageBindingModels
    {
        [Required]
        public string Content { get; set; }

        [Required]
        public string Reciever { get; set; }
    }
}