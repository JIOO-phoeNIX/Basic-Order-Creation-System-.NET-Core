using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.DataAccess.Models;

namespace Task2.DataAccess.Repository
{
    public class UserRepository
    {
        private readonly Task2DbContext task2DbContext;

        public UserRepository(Task2DbContext task2DbContext)
        {
            this.task2DbContext = task2DbContext;
        }

        public IEnumerable<User> GetAll()
        {
            return task2DbContext.User.ToList();
        }

        public User Get(User user)
        {
            var foundUser =  task2DbContext.User
                .Where(t => t.UserName == user.UserName && t.Password == user.Password);
            return foundUser.FirstOrDefault();
        }

        public User GetById(decimal? id)
        {
            return task2DbContext.User.Find(id);
        }

        public async Task<User> Create(User user)
        {
            task2DbContext.User.Add(user);
            await task2DbContext.SaveChangesAsync();

            return user;
        }
    }
}
