using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data
{
    public interface IAuthRepository
    {
        public Task<User> Register(User user, string password);
        public Task<User> Login(string userName, string password);
        public Task<bool> VerifyUser(string userName);
    }
}
