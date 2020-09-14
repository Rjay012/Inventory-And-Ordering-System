using InventoryAndOrderingSystem.Models;
using InventoryAndOrderingSystem.Models.LoginModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryAndOrderingSystem.Repositories.LoginRepositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly IOSContext _context;

        public LoginRepository(IOSContext context)
        {
            _context = context;
        }

        public IQueryable<User> Login(LoginModel loginModel)
        {
            return  _context.Users
                            .Where(u => u.Username == loginModel.Username && u.Password == loginModel.Password);
        }
    }
}
