using System;
using System.ComponentModel.DataAnnotations;

namespace WebChat.Models
{
    public class Message
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "You cannot enter a message with less than 1 symbol.")]
        [MaxLength(500, ErrorMessage = "You cannot enter a message with more than 500 symbols.")]
        public string Content { get; set; }

        public DateTime MessageDateTime { get; set; }

        [Required]
        public string SenderId { get; set; }

        public virtual ApplicationUser Sender { get; set; }

        [Required]
        public string ReceiverId { get; set; }

        public virtual ApplicationUser Receiver { get; set; }

        public int? ChatRoomId { get; set; }

        public virtual ChatRoom ChatRoom { get; set; }
    }
}