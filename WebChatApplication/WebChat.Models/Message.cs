using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

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

        [JsonIgnore]
        public virtual ApplicationUser Sender { get; set; }

        public string ReceiverId { get; set; }

        [JsonIgnore]
        public virtual ApplicationUser Receiver { get; set; }

        public int? ChatRoomId { get; set; }

        [JsonIgnore]
        public virtual ChatRoom ChatRoom { get; set; }
    }
}