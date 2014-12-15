using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adzerk.Api.Models
{
    public class ResultWrapper<T>
    {
        public int pageNumber;
        public int pageSize;
        public int totalPages;
        public int totalItems;

        public IEnumerable<T> items;
    }
}
