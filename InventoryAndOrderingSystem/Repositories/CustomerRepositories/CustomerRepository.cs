using InventoryAndOrderingSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryAndOrderingSystem.Repositories.CustomerRepositories
{
    public class CustomerRepository
    {
        private readonly IOSContext _context;
        public CustomerRepository(IOSContext context)
        {
            _context = context;
        }

        public IQueryable<User> Customer()
        {
            return _context.Users
                           .Where(u => u.RoleID == 2);
        }

        public bool Create(User user)
        {
            _context.Users.Add(user);
            try
            {
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public User FindByID(int customerID)
        {
            return _context.Users
                           .Find(customerID);
        }

        public bool Edit(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            try
            {
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Delete(int customerID)
        {
            User user = FindByID(customerID);

            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }
}
