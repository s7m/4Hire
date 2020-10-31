using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data {
        public class AuthRepository : IAuthRepository {
            private readonly UserContext _context;

            public AuthRepository (UserContext context) {
                _context = context;
            }

            public Task<User> Login (string userName, string password) {
                throw new NotImplementedException ();
            }

            public async Task<User> Register (User user, string password) {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash (password, out passwordHash, out passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

                await _context.Users.AddAsync (user);
                await _context.SaveChangesAsync ();
                return user;
            }

            private void CreatePasswordHash (string password, out byte[] passwordHash, out byte[] passwordSalt) {
                using (var hmac = new System.Security.Cryptography.HMACSHA512 ()) {
                    passwordSalt = hmac.Key;
                    passwordHash = hmac.ComputeHash (System.Text.Encoding.UTF8.GetBytes (password));
                }
            }

            public async Task<bool> VerifyUser (string userName) {
                if (await _context.Users.AnyAsync (u => u.UserName == userName)) {
                        return true;
                    }
                    return false;
                }
            }
        }