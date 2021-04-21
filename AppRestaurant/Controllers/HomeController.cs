using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppRestaurant.Models;
using AppRestaurant.Repositories;
using AppRestaurant.ViewModel;

namespace AppRestaurant.Controllers
{
    public class HomeController : Controller
    {
        private RestaurantDBEntities objRestaurantDbEntities;
        public HomeController()
        {
            objRestaurantDbEntities = new RestaurantDBEntities();
        }
        // GET: Home
        public ActionResult Index()
        {
            CustomerRepository objCustomerRepository = new CustomerRepository();
            ItemRepository objItemRepository = new ItemRepository();
            PaymentTypeRepository objPaymentTypeRepository = new PaymentTypeRepository();
            
            var objMultipleModels = new Tuple<IEnumerable<SelectListItem>, IEnumerable<SelectListItem>, IEnumerable<SelectListItem>>
                (objCustomerRepository.GetAllCustomers(), objItemRepository.GetAllItems(), objPaymentTypeRepository.GetAllPaymentType());
            return View(objMultipleModels);
        }
        [HttpGet]
        public JsonResult getItemPrice(int itemId)
        {
            decimal price = objRestaurantDbEntities.Items.Single(model => model.ItemId == itemId).ItemPrice;
            return Json(price, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult Index(OrderViewModel objOrderViewModel)
        {
            OrderRepository objOrderRepository = new OrderRepository();
            objOrderRepository.AddOrder(objOrderViewModel);
            return Json("Order was successfully placed.", JsonRequestBehavior.AllowGet);
        }
    }
}