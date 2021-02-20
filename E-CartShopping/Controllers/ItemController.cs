using E_CartShopping.Models;
using E_CartShopping.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_CartShopping.Controllers
{
    public class ItemController : Controller
    {
        private ECartDBEntities objECartDBEntities;

        public ItemController()
        {
            objECartDBEntities = new ECartDBEntities();
        }
        // GET: Item
        public ActionResult Index()
        {
            ItemViewModel objItemViewModel = new ItemViewModel();
            objItemViewModel.CategorySelectListItem = (from objCatetegory in objECartDBEntities.Categories
                                                       select new SelectListItem()
                                                       {
                                                           Text = objCatetegory.CategoryName,
                                                           Value = objCatetegory.CategoryId.ToString(),
                                                           Selected = true
                                                       }).ToList();

            return View(objItemViewModel);
        }
        [HttpPost]
        public ActionResult Index(ItemViewModel objItemViewModel)
        {
            string NewImage = Guid.NewGuid() + Path.GetExtension(objItemViewModel.ImagePath.FileName);
            objItemViewModel.ImagePath.SaveAs(Server.MapPath("~/Images/" + NewImage));

            Item objItem = new Item()
            {
                ImagePath = "~/Images/" + NewImage,
                CategoryId = objItemViewModel.CategoryId,
                Description = objItemViewModel.Description,
                ItemId = Guid.NewGuid(),
                ItemCode = objItemViewModel.ItemCode,
                ItemName = objItemViewModel.ItemName,
                ItemPrice = objItemViewModel.ItemPrice,               
            };

            objECartDBEntities.Items.Add(objItem);
            objECartDBEntities.SaveChanges();

            return Json(new { success= true, message="Item is added Successfully."}, JsonRequestBehavior.AllowGet);
        }
    }
}