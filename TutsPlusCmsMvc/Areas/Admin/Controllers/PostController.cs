using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using TutsPlusCmsMvc.Data;
using TutsPlusCmsMvc.Models;
using TutsPlusCmsMvc.Utility;

namespace TutsPlusCmsMvc.Areas.Admin.Controllers
{
    // /admin/post
    
    [RouteArea("Admin")]
    [RoutePrefix("Post")]
    public class PostController : Controller
    {
        private readonly IPostRepository _repository;
        
        public PostController() : this(new PostRepository())
        {
            
        }

        public PostController(IPostRepository repository)
        {
            _repository = repository;
        }
        
        // GET: admin/post
        [Route("")]
        public ActionResult Index()
        {
            var allPosts = _repository.GetAll();
            return View(allPosts);
        }


        // /admin/post/create
        [Route("create")]
        [HttpGet]
        public ActionResult Create()
        {
            return View(new Post());
        }

        // /admin/post/create
        [Route("create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Post model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (string.IsNullOrWhiteSpace(model.Id))
            {
                model.Id = model.Title;
            }

            model.Id = model.Id.MakeUrlFriendly();
            model.Tags = model.Tags.Select(t => t.MakeUrlFriendly()).ToList();
            model.Created = DateTime.Now;

            model.AuthorId = "8dc5e0f1-9ffb-4eb6-bcd8-dfd3c5162c62";

            //TODO: insert model in data store
            try
            {
                _repository.Create(model);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(model);
            }
        }



        // /admin/post/edit/idstring
        [Route("edit/{postId}")]
        [HttpGet]
        public ActionResult Edit(string postId)
        {
            var post = _repository.Get(postId);

            if (post == null)
            {
                return HttpNotFound();
            }

            return View(post);
        }

        // /admin/post/edit/idstring
        [Route("edit/{postId}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string postId, Post model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (string.IsNullOrWhiteSpace(model.Id))
            {
                model.Id = model.Title;
            }

            model.Id = model.Id.MakeUrlFriendly();
            model.Tags = model.Tags.Select(t => t.MakeUrlFriendly()).ToList();

            try
            {
                _repository.Edit(postId, model);

                return RedirectToAction("Index");
            }
            catch (KeyNotFoundException)
            {
                return HttpNotFound();
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(model);
            }
        }

        // /admin/post/delete/idstring
        [Route("delete/{postId}")]
        [HttpGet]
        public ActionResult Delete(string postId)
        {
            var post = _repository.Get(postId);

            if (post == null)
            {
                return HttpNotFound();
            }

            return View(post);
        }

        // /admin/post/delete/idstring
        [Route("delete/{postId}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string postId, bool? confirm)
        {
            try
            {
                _repository.Delete(postId);
                return RedirectToAction("Index");
            }
            catch (KeyNotFoundException)
            {
                return HttpNotFound();
            }
        }
    }
}