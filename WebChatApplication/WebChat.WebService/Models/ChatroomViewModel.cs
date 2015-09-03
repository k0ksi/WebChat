using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebChat.WebService.Models
{
    public class ChatroomViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(100)]
        public string Name { get; set; }

        public ICollection<UserInfoShortViewModel> Users { get; set; }
    }
}