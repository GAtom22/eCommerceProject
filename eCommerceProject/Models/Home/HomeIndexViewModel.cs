using eCommerceProject.DAL;
using eCommerceProject.Repository;
using PagedList;
using PagedList.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace eCommerceProject.Models.Home
{
    public class HomeIndexViewModel
    {
        dbMyOnlineShopEntities context = new dbMyOnlineShopEntities();
        public IPagedList<Product> ListOfProducts { get; set; }
        public HomeIndexViewModel CreateModel(string search, int pageSize, int? page)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@search", search??(object)DBNull.Value)
            };
            IPagedList<Product> data = context.Database.SqlQuery<Product>("GetBySearch @search", param).ToList().ToPagedList(page?? 1, pageSize);
            return new HomeIndexViewModel()
            {
                ListOfProducts = data
            };
        }
    }
}