using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using TutsPlusCmsMvc.Areas.Admin.ViewModels;
using TutsPlusCmsMvc.Data.Identity;
using TutsPlusCmsMvc.Models;
using TutsPlusCmsMvc.Areas.Admin.Services;
using System.Threading.Tasks;

namespace TutsPlusCmsMvc.Areas.Admin.Controllers
{
    [RouteArea("admin")]
    [RoutePrefix("user")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly UserService _users;

        public UserController()
        {
            _roleRepository = new RoleRepository();
            _userRepository = new UserRepository();
            _users = new UserService(ModelState, _userRepository, _roleRepository);
        }


            // GET: Admin/User
        [Route("")]
        [HttpGet]
        public ActionResult Index()
        {
            using (var userManager = new CmsUserManager())
            {
                var users = userManager.Users.ToList();
                return View(users);
            }
        }

        // GET: Admin/User/Create
        [Route("Create")]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/User/Create
        [Route("Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UserViewModel model)
        {
            if (await _users.CreateAsync(model))
            {
                return RedirectToAction("index");
            }

            return View(model);
        }



        // GET: Admin/User/Edit/{userName}
        [Route("Edit/{userName}")]
        [HttpGet]
        public ActionResult Edit(string userName)
        {
            using(var userStore = new CmsUserStore())
            using (var userManager = new CmsUserManager(userStore))
            {
                var user = userStore.FindByNameAsync(userName).Result;

                if (user == null)
                {
                    return HttpNotFound();
                }

                var viewModel = new UserViewModel()
                {
                    UserName = userName,
                    DisplayName = user.DisplayName,
                    Email = user.Email
                };

                return View(viewModel);
            }
        }

        // POST: Admin/User/Edit/{userName}
        [Route("Edit/{userName}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserViewModel model)
        {
            using (var userStore = new CmsUserStore())
            using (var userManager = new CmsUserManager(userStore))
            {
                var user = userStore.FindByNameAsync(model.UserName).Result;
                var hasher = userManager.PasswordHasher;

                if (user == null)
                {
                    return HttpNotFound();
                }

                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                if (!string.IsNullOrWhiteSpace(model.NewPassword))
                {
                    if (string.IsNullOrWhiteSpace(model.CurrentPassword))
                    {
                        ModelState.AddModelError(string.Empty, "The current password must be entered.");
                        return View(model);
                    }

                    var passwordCheckResult = hasher
                        .VerifyHashedPassword(user.PasswordHash,model.CurrentPassword);

                    if (passwordCheckResult != PasswordVerificationResult.Success)
                    {
                        ModelState.AddModelError(string.Empty, "The current password does not match our records.");
                        return View(model);
                    }

                    var newHashedPassword = hasher.HashPassword(model.NewPassword);
                    user.PasswordHash = newHashedPassword;
                }

                user.Email = model.Email;
                user.DisplayName = model.DisplayName;

                var updateResult = userManager.UpdateAsync(user).Result;

                if (!updateResult.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, "An unknown error occurred.");
                    return View(model);
                }

                return RedirectToAction("index");
            }
        }

        // POST: Admin/User/Delete/{userName}
        [Route("Delete/{userName}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string userName)
        {
            using (var userStore = new CmsUserStore())
            using (var userManager = new CmsUserManager(userStore))
            {
                var user = userStore.FindByNameAsync(userName).Result;
                var hasher = userManager.PasswordHasher;

                if (user == null)
                {
                    return HttpNotFound();
                }

                var deleteResult = userManager.DeleteAsync(user).Result;

                return RedirectToAction("index");
            }
        }

        private bool _isDisposed;

        protected override void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                _roleRepository.Dispose();
                _userRepository.Dispose();
            }
            
            _isDisposed = true;
            base.Dispose(disposing);
        }
    }
}