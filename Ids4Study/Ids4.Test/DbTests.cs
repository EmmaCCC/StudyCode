using Ids4.Data;
using Ids4.Data.Entitys;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ids4.Test
{
    public class DbTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var context = new GwlDbContext();
            var list = context.AppClients.ToList();
        }

        [Test]
        public void AddUser()
        {
            var context = new GwlDbContext();
            var users = new List<User>()
            {
                    new User()
                {
                    UserId = Guid.NewGuid().ToString(),
                    UserName = "songlin",
                    Password = "123"
                },
                    new User()
                {
                    UserId = Guid.NewGuid().ToString(),
                    UserName = "lisi",
                    Password = "456"
                }
            };

            context.Users.AddRange(users);
            context.SaveChanges();
        }
    }
}