﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebChat.Models
{
    public class ChatRoom
    {
        private ICollection<Message> messages;
        private ICollection<ApplicationUser> users;

        public ChatRoom()
        {
            this.messages = new HashSet<Message>();
            this.users = new HashSet<ApplicationUser>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public ApplicationUser Creator { get; set; }
        public virtual ICollection<Message> Messages
        {
            get { return this.messages; }
            set { this.messages = value; }
        }

        public virtual ICollection<ApplicationUser> Users
        {
            get { return this.users; }
            set { this.users = value; }
        }
    }
}