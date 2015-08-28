using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WebChat.Models
{
    public class ApplicationUser : IdentityUser
    {
        private ICollection<ChatRoom> joinedChatRooms;
        private ICollection<ChatRoom> createdChatRooms; 
        private ICollection<Message> sentMessages;
        private ICollection<Message> receivedMessages;

        public ApplicationUser()
        {
            this.sentMessages = new HashSet<Message>();
            this.receivedMessages = new HashSet<Message>();
            this.joinedChatRooms = new HashSet<ChatRoom>();
            this.createdChatRooms = new HashSet<ChatRoom>();
        }

        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Username { get; set; }

        public string ImageUrl { get; set; }

        [DataType(DataType.PhoneNumber)]
        [MaxLength(20)]
        public string PhoneNum { get; set; }
        
        public DateTime? BirthDate { get; set; }

        [InverseProperty("Sender")]
        public virtual ICollection<Message> SentMessages
        {
            get { return this.sentMessages; }
            set { this.sentMessages = value; }
        }

        [InverseProperty("Receiver")]
        public virtual ICollection<Message> ReceivedMessages
        {
            get { return this.receivedMessages; }
            set { this.receivedMessages = value; }
        }

        public virtual ICollection<ChatRoom> JoinedChatRooms
        {
            get { return this.joinedChatRooms; }
            set { this.joinedChatRooms = value; }
        }

        public virtual ICollection<ChatRoom> CreatedChatRooms
        {
            get { return this.createdChatRooms; }
            set { this.createdChatRooms = value; }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }  
    }
}