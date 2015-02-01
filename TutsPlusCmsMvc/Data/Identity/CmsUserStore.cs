using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using TutsPlusCmsMvc.Models;

namespace TutsPlusCmsMvc.Data.Identity
{
    public class CmsUserStore : UserStore<CmsUser>
    {

        public CmsUserStore()
            :this(new CmsContext())
        {
            
        }

        public CmsUserStore(CmsContext context)
            : base(context)
        {
            
        }
    }
}