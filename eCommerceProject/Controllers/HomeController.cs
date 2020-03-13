using eCommerceProject.DAL;
using eCommerceProject.Models.Home;
using eCommerceProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCommerceProject.Controllers
{
    public class HomeController : Controller
    {
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();
        public ActionResult Index(string search, int? page)
        {
            HomeIndexViewModel model = new HomeIndexViewModel();
            return View(model.CreateModel(search,4, page));
        }

        public ActionResult Products(string search, int? page)
        {
            HomeIndexViewModel model = new HomeIndexViewModel();
            return View(model.CreateModel(search, 4, page));
        }
        public ActionResult AddToCart(int productId)
        {
            if (Session["cart"] == null)
            {
                List<Item> cart = new List<Item>();
                var product = _unitOfWork.GetRepositoryInstance<Product>().GetFirstOrDefault(productId);
                cart.Add(new Item()
                {
                    Product = product,
                    Quantity = 1
                });
                Session["cart"] = cart;
            }
            else
            {
                List<Item> cart = (List<Item>)Session["cart"];
                var product = _unitOfWork.GetRepositoryInstance<Product>().GetFirstOrDefault(productId);
                foreach (var item in cart)
                {
                    if(item.Product.ProductId == productId)
                    {
                        int prevQty = item.Quantity;
                        cart.Remove(item);
                        cart.Add(new Item()
                        {
                            Product = product,
                            Quantity = prevQty + 1
                        });
                        break;
                    }
                    else
                    {
                        cart.Add(new Item()
                        {
                            Product = product,
                            Quantity = 1
                        });
                    }
                }
  
                Session["cart"] = cart;
            }

            return Redirect(".");
        }

        public ActionResult DecreaseQty(int productId)
        {
                List<Item> cart = (List<Item>)Session["cart"];
                var product = _unitOfWork.GetRepositoryInstance<Product>().GetFirstOrDefault(productId);
                foreach (var item in cart)
                {
                    if (item.Product.ProductId == productId)
                    {
                        int prevQty = item.Quantity;
                        if (prevQty == 1)
                        {
                            cart.Remove(item);
                        break;
                        }
                        else
                        {
                        cart.Remove(item);
                        cart.Add(new Item()
                        {
                            Product = product,
                            Quantity = prevQty - 1
                        });
                        break;
                    }
                    
                    }
                }

                Session["cart"] = cart;
            return Redirect("CheckoutDetails");
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult RemoveFormCart(int productId)
        {
            List<Item> cart = (List<Item>)Session["cart"];

            foreach (var item in cart)
            {
                if(item.Product.ProductId == productId)
                {
                    cart.Remove(item);
                    break;
                }
            }
            Session["cart"] = cart;
            return Redirect("Index");
        }


        public ActionResult Checkout()
        {

            return View();
        }
        public ActionResult CheckoutDetails()
        {

            return View();
        }
    }
}