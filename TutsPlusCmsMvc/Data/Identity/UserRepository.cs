using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TutsPlusCmsMvc.Models;

namespace TutsPlusCmsMvc.Data.Identity
{
    public class UserRepository : IUserRepository, IDisposable
    {
        private readonly CmsUserStore _store;
        private readonly CmsUserManager _manager;

        public UserRepository()
        {
            _store = new CmsUserStore();
            _manager = new CmsUserManager();
        }

        public CmsUser GetUserByName(string userName)
        {
            return _store.FindByNameAsync(userName).Result;
        }

        public IEnumerable<CmsUser> GetAllUsers()
        {
            return _store.Users.ToArray();
        }

        public async Task CreateAsync(CmsUser user, string passWord)
        {
            await _manager.CreateAsync(user, passWord);
        }

        public async Task DeleteAsync(CmsUser user)
        {
            await _manager.DeleteAsync(user);
        }

        public async Task UpdateAsync(CmsUser user)
        {
            await _manager.UpdateAsync(user);
        }

        private bool _disposed;

        public void Dispose()
        {
            if (!_disposed)
            {
                _store.Dispose();
                _manager.Dispose();
            }

            _disposed = true;
        }
    }
}