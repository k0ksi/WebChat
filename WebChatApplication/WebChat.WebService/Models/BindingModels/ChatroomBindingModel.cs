using System.ComponentModel.DataAnnotations;

namespace WebChat.WebService.Models
{
    public class ChatroomBindingModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}