using E_CartShopping.Models;
using E_CartShopping.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_CartShopping.Controllers
{
    public class ShoppingController : Controller
    {
        private ECartDBEntities objECartDBEntities;
        private List<ShoppingCartViewModel> listOfShoppingCartViewModels;
        public ShoppingController()
        {
            objECartDBEntities = new ECartDBEntities();
            listOfShoppingCartViewModels = new List<ShoppingCartViewModel>();
        }
        // GET: Shopping
        public ActionResult Index()
        {
            IEnumerable<ShoppingViewModel> ListOfShoppingViewModel = (from objItem in objECartDBEntities.Items
                                                                      join objCategory in objECartDBEntities.Categories
                                                                      on objItem.CategoryId equals objCategory.CategoryId
                                                                      select new ShoppingViewModel()
                                                                      {
                                                                          ImagePath = objItem.ImagePath,
                                                                          ItemName = objItem.ItemName,
                                                                          Description = objItem.Description,
                                                                          ItemPrice = objItem.ItemPrice,
                                                                          ItemId = objItem.ItemId,
                                                                          Category = objCategory.CategoryName

                                                                      }).ToList();

            return View(ListOfShoppingViewModel);
        }
        [HttpPost]
        public JsonResult Index(string ItemId)
        {
            ShoppingCartViewModel objShoppingCartViewModel = new ShoppingCartViewModel();

            Item objItem = objECartDBEntities.Items.Single(model => model.ItemId.ToString() == ItemId);

            if(Session["CartCounter"] != null)
            {
                listOfShoppingCartViewModels = Session["CartItem"] as List<ShoppingCartViewModel>;
            }

            if(listOfShoppingCartViewModels.Any(model => model.ItemId == ItemId))
            {
                objShoppingCartViewModel = listOfShoppingCartViewModels.Single(model => model.ItemId == ItemId);
                objShoppingCartViewModel.Quantity = objShoppingCartViewModel.Quantity + 1;
                objShoppingCartViewModel.Total = objShoppingCartViewModel.Quantity * objShoppingCartViewModel.UnitPrice;
            }
            else
            {
                objShoppingCartViewModel.ItemId = ItemId;
                objShoppingCartViewModel.ImagePath = objItem.ImagePath;
                objShoppingCartViewModel.ItemName = objItem.ItemName;
                objShoppingCartViewModel.Quantity = 1;
                objShoppingCartViewModel.UnitPrice = objItem.ItemPrice;
                objShoppingCartViewModel.Total = objItem.ItemPrice;

                listOfShoppingCartViewModels.Add(objShoppingCartViewModel);
            }

            Session["CartCounter"] = listOfShoppingCartViewModels.Count;
            Session["CartItem"] = listOfShoppingCartViewModels;

            return Json(new { success = true, counter = listOfShoppingCartViewModels.Count}, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ShoppingCart()
        {
            listOfShoppingCartViewModels = Session["CartItem"] as List<ShoppingCartViewModel>;

            return View(listOfShoppingCartViewModels);
        }
        [HttpPost]
        public ActionResult AddOrder()
        {
            int OrderId = 0;
            listOfShoppingCartViewModels = Session["CartItem"] as List<ShoppingCartViewModel>;

            Order objOrder = new Order()
            {
                OrderDate = DateTime.Now,
                OrderNumber = String.Format("{0:ddMMyyyyHHmmss}", DateTime.Now)
            };

            objECartDBEntities.Orders.Add(objOrder);
            objECartDBEntities.SaveChanges();

            OrderId = objOrder.OrderId;

            foreach (var item in listOfShoppingCartViewModels)
            {
                OrderDetail objOrderDetail = new OrderDetail();
                objOrderDetail.Total = item.Total;
                objOrderDetail.ItemId = item.ItemId;
                objOrderDetail.OrderId = OrderId;
                objOrderDetail.Quantity = item.Quantity;
                objOrderDetail.UnitPrice = item.UnitPrice;

                objECartDBEntities.OrderDetails.Add(objOrderDetail);
                objECartDBEntities.SaveChanges();

            }

            Session["CartCounter"] = null;
            Session["CartItem"] = null;

            return RedirectToAction("Index");
        }
    }
}