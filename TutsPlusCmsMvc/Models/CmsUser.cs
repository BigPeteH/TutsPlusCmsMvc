using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TutsPlusCmsMvc.Models
{
    public class CmsUser : IdentityUser
    {
        public string DisplayName { get; set; }
    }
}