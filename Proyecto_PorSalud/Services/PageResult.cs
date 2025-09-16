using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_PorSalud.Services
{
    public class PagedResult<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages => (TotalItems + PageSize - 1) / PageSize;
    }
}