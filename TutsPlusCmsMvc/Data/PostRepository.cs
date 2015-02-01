using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TutsPlusCmsMvc.Models;
using TutsPlusCmsMvc.Utility;

namespace TutsPlusCmsMvc.Data
{
    public class PostRepository : IPostRepository
    {
        public Post Get(string id)
        {
            using (var db = new CmsContext())
            {
                return db.Posts.Include("Author").SingleOrDefault(p => p.Id == id);
            }
        }

        public void Edit(string id, Post updatedItem)
        {
            using (var db = new CmsContext())
            {
                var post = db.Posts.SingleOrDefault(p => p.Id == id);

                if (post == null)
                {
                    throw new KeyNotFoundException(string.Format("A post with the id of {0} does not exist in the data store", id));
                }

                post.Id = updatedItem.Id;
                post.Title = updatedItem.Title;
                post.Content = updatedItem.Content;
                post.Published = updatedItem.Published;
                post.Tags = updatedItem.Tags;

                db.SaveChanges();

            }
        }

        public void Create(Post item)
        {
            using (var db = new CmsContext())
            {
                var post = db.Posts.SingleOrDefault(p => p.Id == item.Id);

                if (post != null)
                {
                    throw new ArgumentException(String.Format("A post with id {0} already exists.", item.Id));
                }

                db.Posts.Add(item);
                db.SaveChanges();
            }
        }

        public IEnumerable<Post> GetAll()
        {
            using (var db = new CmsContext())
            {
                return db.Posts.Include("Author")
                    .OrderByDescending(p => p.Created)
                    .ToArray();
            }
        }


        public void Delete(string id)
        {
            using (var db = new CmsContext())
            {
                var post = db.Posts.SingleOrDefault(p => p.Id == id);

                if (post == null)
                {
                    throw new KeyNotFoundException(String.Format("A post with the id of {0} does not exist in the data store", id));
                }

                db.Posts.Remove(post);
                db.SaveChanges();
            }
        }
    }
}