using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supernova.Core
{
    public interface IRepository<T> where T : IContent
    {
        T Item(object id);
        List<T> List(out int total, int pageNumber, int pageSize);
        List<T> All();
    }
}
