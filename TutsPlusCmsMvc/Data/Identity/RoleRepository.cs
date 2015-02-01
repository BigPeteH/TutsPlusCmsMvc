using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TutsPlusCmsMvc.Data.Identity
{
    public class RoleRepository : IRoleRepository, IDisposable
    {
        private readonly RoleStore<IdentityRole> _store;
        private readonly RoleManager<IdentityRole> _manager;

        public RoleRepository()
        {
            _store = new RoleStore<IdentityRole>();
            _manager = new RoleManager<IdentityRole>(_store);
        }

        public IdentityRole GetRoleByName(string roleName)
        {
            return _store.FindByNameAsync(roleName).Result;
        }

        public IEnumerable<IdentityRole> GetAllRoles()
        {
            return _store.Roles.ToArray();
        }

        public void Create(IdentityRole role)
        {
            _manager.Create(role);
        }

        public void Delete(IdentityRole role)
        {
            _manager.Delete(role);
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