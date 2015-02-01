using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using TutsPlusCmsMvc.Models;

namespace TutsPlusCmsMvc.Data.Identity
{
    public class CmsUserManager : UserManager<CmsUser>
    {
        public CmsUserManager()
            : this(new CmsUserStore())
        {
            
        }

        public CmsUserManager(UserStore<CmsUser> userStore)
            : base(userStore)
        {
            
        }
    }
}