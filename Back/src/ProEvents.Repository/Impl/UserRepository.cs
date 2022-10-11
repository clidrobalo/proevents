using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEvents.Domain.Identity;
using ProEvents.Repository.Contexts;
using ProEvents.Repository.Interfaces;

namespace ProEvents.Repository.Impl
{
    public class UserRepository : GenericRepository, IUserRepository
    {
        private readonly ProEventsContext _proEventsContext;

        public UserRepository(ProEventsContext proEventsContext) : base(proEventsContext)
        {
            _proEventsContext = proEventsContext;
        }
        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _proEventsContext.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _proEventsContext.Users.FindAsync(id);
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _proEventsContext.Users.SingleOrDefaultAsync(user => user.UserName.ToLower().Equals(username.ToLower()));
        }

    }
}