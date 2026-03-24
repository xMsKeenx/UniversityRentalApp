using System;
using System.Collections.Generic;
using System.Text;
using UniversityRentalApp.Models;

namespace UniversityRentalApp.Repositories
{
    public class UserRepository
    {
        private List<User> _users = new List<User>();

        public void Add(User user)
        {
            _users.Add(user);
        }

        public User GetById(Guid id)
        {
           
            foreach (var user in _users)
            {
                if (user.Id == id)
                {
                    return user;
                }
            }
            return null; 
        }

        public List<User> GetAll()
        {
            return _users;
        }
    }
}
