using InventoryAndOrderingSystem.Models;
using InventoryAndOrderingSystem.Models.LoginModels;
using InventoryAndOrderingSystem.Repositories.LoginRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryAndOrderingSystem.Services.LoginServices
{
    public class LoginService : ILoginService
    {
        private readonly LoginRepository _loginRepository;
        public LoginService(LoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        public IQueryable<User> Login(LoginModel loginModel)
        {
            return _loginRepository.Login(loginModel);
        }
    }
}
