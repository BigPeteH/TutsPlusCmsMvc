using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutsPlusCmsMvc.Data
{
    public interface ITagRepository
    {
        void Delete(string item);
        void Edit(string existingItem, string newItem);
        void Add(string item);
        IEnumerable<string> GetAll();
        string Get(string item);
    }
}
