using System.Collections.Generic;
using System.Threading.Tasks;
using TutsPlusCmsMvc.Models;

namespace TutsPlusCmsMvc.Data.Identity
{
    public interface IUserRepository
    {
        CmsUser GetUserByName(string userName);
        IEnumerable<CmsUser> GetAllUsers();
        Task CreateAsync(CmsUser user, string passWord);
        Task DeleteAsync(CmsUser user);
        Task UpdateAsync(CmsUser user);
        void Dispose();
    }
}