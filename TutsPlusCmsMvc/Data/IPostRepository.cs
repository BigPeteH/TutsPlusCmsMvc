using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutsPlusCmsMvc.Models;

namespace TutsPlusCmsMvc.Data
{
    public interface IPostRepository
    {
        Post Get(string id);
        void Edit(string id, Post updatedItem);
        void Create(Post item);
        IEnumerable<Post> GetAll();
        void Delete(string id);
    }
}
