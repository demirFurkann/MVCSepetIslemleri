using MVCSepetIslemleri_Tekrar.CustomTools;
using MVCSepetIslemleri_Tekrar.DesignPatterns.SingletonPattern;
using MVCSepetIslemleri_Tekrar.Models;
using MVCSepetIslemleri_Tekrar.Models.PageVMs;
using MVCSepetIslemleri_Tekrar.Models.PureVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCSepetIslemleri_Tekrar.Controllers
{
    public class ShoppingController : Controller
    {
        NorthwindEntities _db;
        public ShoppingController()
        {
            _db = DBTool.DBInstance;
        }
        
        public ActionResult ProductList()
        {
            List<ProductVM> products = _db.Products.Select(x => new ProductVM
            {
                ID = x.ProductID,
                ProductName = x.ProductName,
                UnitPrice = x.UnitPrice,
            }).ToList();

            ListProductPageVM pvm = new ListProductPageVM
            {
                Products = products,
            };
            return View(pvm);
        }
        public ActionResult AddToCart(int id)
		{
			CartItem ci = SepeteYolla(id);
			TempData["mesaj"] = $"{ci.ProductName} isimli ürün sepete eklenmiştir";

			return RedirectToAction("ProductList");
		}

		private CartItem SepeteYolla(int id)
		{
			Cart c = Session["scart"] == null ? new Cart() : Session["scart"] as Cart;

			Product eklenecekUrun = _db.Products.Find(id);

			CartItem ci = new CartItem();
			ci.ProductName = eklenecekUrun.ProductName;
			ci.UnitPrice = eklenecekUrun.UnitPrice;
			ci.ID = eklenecekUrun.ProductID;

			c.SepeteEkle(ci);
			Session["scart"] = c;
			return ci;
		}

		public ActionResult CartPage()
        {
            if (Session["scart"]!=null)
            {
                Cart C = Session["scart"] as Cart;
                CartPageVM cpvm = new CartPageVM
                {
                    Cart = C,
                };
                return View(cpvm);
            }

            ViewBag.SepetBos = "Sepetinizde Ürün Bulunmamaktadır";
            return View();
        }

        public ActionResult DeleteFromCart(int id)
        {
            if (Session["scart"]!=null)
            {
                Cart c = Session["scart"] as Cart;

                c.SepettenSil(id);
                if (c.Sepetim.Count == 0) Session.Remove("scart");
                return RedirectToAction("CartPage");
            }
            return RedirectToAction("ProductList");
        }
        public ActionResult IncreaseAmount(int id)
        {
            SepeteYolla(id);
            return RedirectToAction("CartPage");
        }
    }
}