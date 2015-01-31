using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TutsPlusCmsMvc.Data;
using TutsPlusCmsMvc.Models;

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
            this._repository = repository;
        }
        
        // GET: admin/post
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
            var model = new Post() {Tags= new List<string> {"test-1", "test-2"}};

            return View(model);
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

            //TODO: insert model in data store
            _repository.Create(model);

            return RedirectToAction("Index");
        }



        // /admin/post/edit/idstring
        [Route("edit/{postId}")]
        [HttpGet]
        public ActionResult Edit(string postId)
        {
            //TODO: retrieve the model for this id from the data store
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
            var post = _repository.Get(postId);

            if (post == null)
            {
                return HttpNotFound();
            }
            
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            //TODO: update model in data store
            _repository.Edit(postId, model);

            return RedirectToAction("Index");
        }
    }
}