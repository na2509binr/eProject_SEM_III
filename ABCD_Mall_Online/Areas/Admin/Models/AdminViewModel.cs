using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABCD_Mall_Online.Models;
using X.PagedList;

namespace ABCD_Mall_Online.Areas.Admin.Models
{
    public class AdminViewModel
    {
        public Category category { get; set; }
        public IPagedList<Category> catList { get; set; }

        public Store store { get; set; }
        public IPagedList<Store> stList { get; set; }

        public Genre genre { get; set; }
        public IPagedList<Genre> genreList { get; set; }

        public Film film { get; set; }
        public IPagedList<Film> filmList { get; set; }
    }
}
