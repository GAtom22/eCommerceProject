using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eCommerceProject;

namespace eCommerceProject.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Payment
        public ActionResult PaymentWithPayPal()
        {
            APIContext apicontext = PayPalConfiguration.GetAPIContext();
            try
            {
                string PayerId = Request.Params["PayerID"];
                if (string.IsNullOrEmpty(PayerId))
                {
                    string baseUri = Request.Url.Scheme + "://" + Request.Url.Authority + "/Payment/PaymentWithPayPal?";
                    var Guid = Convert.ToString((new Random()).Next(100000000));
                    var createdPayment = this.CreatePayment(apicontext, baseUri + "guid=" + Guid);

                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectURL = null;

                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;

                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            paypalRedirectURL = lnk.href;
                        }
                    }
                    Session.Add(Guid, createdPayment.id);
                    return Redirect(paypalRedirectURL);
                }
                else
                {
                    var guid = Request.Params["guid"];
                    var executePayment = ExecutePayment(apicontext, PayerId, Session[guid] as string);

                    if (executePayment.state.ToLower()!="approved")
                    {
                        return View("FailureView");
                    }

                }
            }
            catch (Exception ex)
            {
                return View("FailureView");

            }
            return View("SuccessView");
        }

        private PayPal.Api.Payment payment;
        private Payment ExecutePayment(APIContext apicontext, string payerId, string PaymentId)
        {
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId

            };
            this.payment = new Payment() { id = PaymentId};
            return this.payment.Execute(apicontext, paymentExecution);
        }

        
        private Payment CreatePayment(APIContext apicontext, string redirectUrl)
        {
            var ItemList = new ItemList() { items = new List<Item>() };

            if (Session["cart"] != null)
            {
                List<Models.Home.Item> cart = (List<Models.Home.Item>)Session["cart"];
                foreach (var item in cart)
                {
                    ItemList.items.Add(new Item()
                    {
                        name = item.Product.ProductName.ToString(),
                        currency = "USD",
                        price = item.Product.Price.ToString(),
                        quantity = item.Quantity.ToString(),
                        sku = "sku"
                    });
                }
                //ItemList.items.Add(new Item()
                //{
                //    name = "Item Name comes here",
                //    currency = "USD",
                //    price = "1",
                //    quantity = "1",
                //    sku = "sku"
                //});

                var payer = new Payer() { payment_method = "paypal" };
                var redirUrl = new RedirectUrls()
                {
                    cancel_url = redirectUrl + "&Cancel=true",
                    return_url = redirectUrl
                };
                var details = new Details()
                {
                    tax = "1",
                    shipping = "1",
                    subtotal = Session["SesTotal"].ToString()
                };
                int total = Convert.ToInt32(details.tax) + Convert.ToInt32(details.shipping) + Convert.ToInt32(details.subtotal);
                var amount = new Amount()
                {
                    currency = "USD",
                    total = total.ToString(),
                    details = details
                };
                //var amount = new Amount()
                //{
                //    currency = "USD",
                //    total = "3", // Total must be equal to sum of tax, shipping and subtotal.  
                //    details = details
                //};
                var transactionList = new List<Transaction>();
                transactionList.Add(new Transaction()
                {
                    description = "Transaction Description",
                    invoice_number = "#100000",
                    amount = amount,
                    item_list = ItemList
                });

                this.payment = new Payment()
                {
                    intent = "sale",
                    payer = payer,
                    transactions = transactionList,
                    redirect_urls = redirUrl
                };
            }


            return this.payment.Create(apicontext);


        }
    }
}