﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using WebChat.Data.Repositories;
using WebChat.Models;

namespace WebChat.Data
{
    public class WebChatData : IWebChatData
    {
        private DbContext context;
        private IDictionary<Type, object> repositories;

        public WebChatData()
            : this(new WebChatContext())
        {
            
        }

        public WebChatData(DbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IRepository<ApplicationUser> Users
        {
            get { return this.GetRepository<ApplicationUser>(); }
        }



        public IRepository<ChatRoom> Chatrooms
        {
            get { return this.GetRepository<ChatRoom>(); }
        }

        public IRepository<Message> Messages
        {
            get { return this.GetRepository<Message>(); }
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>()
            where T : class 
        {
            var typeOfRepository = typeof(T);
            if (!this.repositories.ContainsKey(typeOfRepository))
            {
                var newRepository = Activator.CreateInstance(typeof(GenericRepository<T>), this.context);
                this.repositories.Add(typeOfRepository, newRepository);
            }

            return (IRepository<T>)this.repositories[typeOfRepository];
        }
    }
}