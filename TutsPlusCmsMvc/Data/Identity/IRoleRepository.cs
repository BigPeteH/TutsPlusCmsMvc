using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TutsPlusCmsMvc.Data.Identity
{
    public interface IRoleRepository
    {
        IdentityRole GetRoleByName(string roleName);
        IEnumerable<IdentityRole> GetAllRoles();
        void Create(IdentityRole role);
        void Delete(IdentityRole role);
        void Dispose();
    }
}