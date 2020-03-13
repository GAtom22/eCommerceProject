using eCommerceProject.DAL;
using eCommerceProject.Models;
using eCommerceProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace eCommerceProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();
        
        public List<SelectListItem> GetCategory()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var cat = _unitOfWork.GetRepositoryInstance<Category>().GetAllRecords();
            foreach (var item in cat)
            {
                list.Add(new SelectListItem { Value = item.CategoryId.ToString(), Text = item.CategoryName });
            }
            return list;
        }
        
        // GET: Admin
        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult Categories()
        {
            List<Category> allcategories = _unitOfWork.GetRepositoryInstance<Category>().GetAllRecordsIQueryable().Where(i => i.IsDelete == false).ToList();
            return View(allcategories);
        }
        public ActionResult AddCategory()
        {
            return UpdateCategory(0);
        }
        public ActionResult UpdateCategory(int categoryId)
        {
            CategoryDetail cd;
                if (categoryId != 0)
            {
                //cd = JsonConvert.DeserializeObject<CategoryDetail>(JsonConvert.SerializeObject(_unitOfWork.GetRepositoryInstance<Category>().GetFirstOrDefault(categoryId)));
                var cat = _unitOfWork.GetRepositoryInstance<Category>().GetFirstOrDefault(categoryId);
                cd = new CategoryDetail{ 
                    CategoryId = cat.CategoryId,
                    CategoryName = cat.CategoryName,
                    IsActive = cat.IsActive,
                    IsDelete = cat.IsDelete
                };
            }
            else
            {
                cd = new CategoryDetail();
            }
            return View("UpdateCategory",cd);
        }

        public ActionResult CategoryEdit(int categoryId)
        {
            ViewBag.CategoryList = GetCategory();
            var cat = _unitOfWork.GetRepositoryInstance<Category>().GetFirstOrDefault(categoryId);
            return View(cat);
        }
        [HttpPost]
        public ActionResult CategoryEdit(Category category)
        {
            _unitOfWork.GetRepositoryInstance<Category>().Update(category);
            return RedirectToAction("Categories");
        }

        public ActionResult CategoryDelete(int categoryId)
        {
            ViewBag.CategoryList = GetCategory();
            var cat = _unitOfWork.GetRepositoryInstance<Category>().GetFirstOrDefault(categoryId);
            return View(cat);
        }
        [HttpPost]
        public ActionResult CategoryDelete(Category category)
        {
            category.IsDelete = true; 
            _unitOfWork.GetRepositoryInstance<Category>().Update(category);
            return RedirectToAction("Categories");
        }


        public ActionResult Products()
        {
            List<Product> allproducts = _unitOfWork.GetRepositoryInstance<Product>().GetAllRecordsIQueryable().Where(i => i.IsDelete == false).ToList();
            return View(allproducts);
        }

        public ActionResult ProductEdit(int productId)
        {
            ViewBag.CategoryList = GetCategory();
            var prod = _unitOfWork.GetRepositoryInstance<Product>().GetFirstOrDefault(productId);
                return View(prod);
        }
        [HttpPost]
        public ActionResult ProductEdit(Product product, HttpPostedFileBase file)
        {
            string pic = null;
            if (file != null)
            {
                pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/Img/ProductImg"), pic);
                file.SaveAs(path);
            }
            product.ProductImage = file!=null ? pic : product.ProductImage;
            product.ModifiedDate = DateTime.Now;
            product.IsDelete = false;
            _unitOfWork.GetRepositoryInstance<Product>().Update(product);
            return RedirectToAction("Products");
        }

        public ActionResult AddProduct()
        {
            ViewBag.CategoryList = GetCategory();
            return View();
        }
        [HttpPost]
        public ActionResult AddProduct(Product product, HttpPostedFileBase file)
        {
            string pic = null;
            if (file != null)
            {
                pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/Img/ProductImg"), pic);
                file.SaveAs(path);
            }
            product.ProductImage = pic; 
            product.CreatedDate = DateTime.Now;
            product.IsDelete = false;
            _unitOfWork.GetRepositoryInstance<Product>().Add(product);
            return RedirectToAction("Products");
        }

        public ActionResult ProductDelete(int productId)
        {
            ViewBag.CategoryList = GetCategory();
            var prod = _unitOfWork.GetRepositoryInstance<Product>().GetFirstOrDefault(productId);
            return View(prod);
        }
        [HttpPost]
        public ActionResult ProductDelete(Product product)
        {
            var prod = _unitOfWork.GetRepositoryInstance<Product>().GetFirstOrDefault(product.ProductId);
            prod.IsDelete = true;
            _unitOfWork.GetRepositoryInstance<Product>().Update(prod);

            return RedirectToAction("Products");
        }

        public ActionResult Members()
        {
            List<Member> allmembers = _unitOfWork.GetRepositoryInstance<Member>().GetAllRecordsIQueryable().Where(i => i.IsDelete == false).ToList();
            return View(allmembers);
        }

        public ActionResult MemberEdit(int memberId)
        {
            var member = _unitOfWork.GetRepositoryInstance<Member>().GetFirstOrDefault(memberId);
            return View(member);
        }
        [HttpPost]
        public ActionResult MemberEdit(Member member)
        {
            member.PasswordHash = HashPassword(member.PasswordHash);
            member.ModifiedOn = DateTime.Now;
            member.IsDelete = false;
            _unitOfWork.GetRepositoryInstance<Member>().Update(member);
            return RedirectToAction("Members");
        }

        public ActionResult AddMember()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddMember(Member member)
        {

    
            member.CreatedOn = DateTime.Now;
            member.IsDelete = false;
            member.PasswordHash = HashPassword(member.PasswordHash);
            _unitOfWork.GetRepositoryInstance<Member>().Add(member);
            return RedirectToAction("Members");
        }

        public ActionResult MemberDelete(int memberId)
        {
            var member = _unitOfWork.GetRepositoryInstance<Member>().GetFirstOrDefault(memberId);
            return View(member);
        }
        [HttpPost]
        public ActionResult MemberDelete(Member member)
        {
            member.IsDelete = true;
            _unitOfWork.GetRepositoryInstance<Member>().Update(member);

            return RedirectToAction("Members");
        }

        private string HashPassword(string password)
        {
            HashAlgorithm sha256 = new SHA256CryptoServiceProvider();
            byte[] byteArray = Encoding.UTF8.GetBytes(password);
            MemoryStream stream = new MemoryStream(byteArray);
            
            return Convert.ToBase64String(sha256.ComputeHash(stream));
        }



        public ActionResult Roles()
        {
            List<Role> allroles = _unitOfWork.GetRepositoryInstance<Role>().GetAllRecordsIQueryable().ToList();
            return View(allroles);
        }
        public ActionResult RoleEdit(int roleId)
        {
            var role = _unitOfWork.GetRepositoryInstance<Member>().GetFirstOrDefault(roleId);
            return View(role);
        }
        [HttpPost]
        public ActionResult RoleEdit(Role role)
        {
            _unitOfWork.GetRepositoryInstance<Role>().Update(role);
            return RedirectToAction("Roles");
        }

        public ActionResult AddRole()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddRole(Role role)
        {
            _unitOfWork.GetRepositoryInstance<Role>().Add(role);
            return RedirectToAction("Roles");
        }

        public ActionResult RoleDelete(int roleId)
        {
            var role = _unitOfWork.GetRepositoryInstance<Role>().GetFirstOrDefault(roleId);
            return View(role);
        }
        [HttpPost]
        public ActionResult RoleDelete(Role role)
        {

            _unitOfWork.GetRepositoryInstance<Role>().Remove(role);
            return RedirectToAction("Roles");
        }
    }
}