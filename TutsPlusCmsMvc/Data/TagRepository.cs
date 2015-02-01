using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.UI.WebControls;
using TutsPlusCmsMvc.Models;

namespace TutsPlusCmsMvc.Data
{
    public class TagRepository : ITagRepository
    {
        public void Delete(string item)
        {
            using (var db = new CmsContext())
            {
                var posts = db.Posts.Where(p => p.CombinedTags.Contains(item)).ToList();
                
                posts = posts.Where(p =>
                    p.Tags.Contains(item, StringComparer.CurrentCultureIgnoreCase))
                    .ToList();

                if (!posts.Any())
                {
                    throw new KeyNotFoundException(String.Format("The tag {0} does not exist.", item));
                }

                foreach (var post in posts)
                {
                    post.Tags.Remove(item);
                }

                db.SaveChanges();
            }
        }

        public void Edit(string existingItem, string newItem)
        {
            using (var db = new CmsContext())
            {
                var posts = db.Posts.Where(p => p.CombinedTags.Contains(existingItem)).ToList();
                
                posts = posts.Where(p => 
                    p.Tags.Contains(existingItem, StringComparer.CurrentCultureIgnoreCase))
                    .ToList();

                if (!posts.Any())
                {
                    throw new KeyNotFoundException(String.Format("The tag {0} does not exist.", existingItem));
                }

                foreach (var post in posts)
                {
                    post.Tags.Remove(existingItem);
                    post.Tags.Add(newItem);
                }

                db.SaveChanges();
            }
        }

        public void Add(string item)
        {
            throw new NotImplementedException();
        }

        public string Get(string item)
        {
            using (var db = new CmsContext())
            {
                var posts = db.Posts.Where(p => p.CombinedTags.Contains(item)).ToList();

                posts = posts.Where(p =>
                    p.Tags.Contains(item, StringComparer.CurrentCultureIgnoreCase))
                    .ToList();

                if (!posts.Any())
                {
                    throw new KeyNotFoundException(String.Format("The tag {0} does not exist.", item));
                }

                return item.ToLower();
            }
        }

        public IEnumerable<string> GetAll()
        {
            using (var db = new CmsContext())
            {
                var tagsCollection = db.Posts.Select(p => p.CombinedTags).ToList();
                return string.Join(",", tagsCollection).Split(',').Distinct();
            }
        }
    }
}