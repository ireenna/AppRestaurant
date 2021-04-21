using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppRestaurant.Models;
using AppRestaurant.ViewModel;

namespace AppRestaurant.Repositories
{
    public class OrderRepository
    {
        private RestaurantDBEntities objRestaurantDbEntities;
        public OrderRepository()
        {
            objRestaurantDbEntities = new RestaurantDBEntities();
        }

        public bool AddOrder(OrderViewModel objOrderViewModel)
        {
            Orders objOrder = new Orders();
            objOrder.FinalTotal = objOrderViewModel.FinalTotal;
            objOrder.CustomerId = objOrderViewModel.CustomerId;
            objOrder.OrderDate =DateTime.Now;
            objOrder.OrderNumber=String.Format("{0:ddmmmyyyyhhmmss}", DateTime.Now);
            objOrder.PaymentTypeId = objOrderViewModel.PaymentTypeId;
            int a = objRestaurantDbEntities.Orders.Count();
            objOrder.OrderId = a;
            int OrderId = objOrder.OrderId;
            objRestaurantDbEntities.Orders.Add(objOrder);
            objRestaurantDbEntities.SaveChanges();
            

            foreach (var item in objOrderViewModel.ListOfOrderDetailViewModel)
            {
                OrderDetails objOrderDetails = new OrderDetails();
                objOrderDetails.OrderId = OrderId;
                objOrderDetails.Discount = item.Discount;
                objOrderDetails.ItemId = item.ItemId;
                objOrderDetails.Total = item.Total;
                objOrderDetails.Price = item.Price;
                objOrderDetails.Quantity = item.Quantity;
                objRestaurantDbEntities.OrderDetails.Add(objOrderDetails);
                objRestaurantDbEntities.SaveChanges();

                Transactions objTransactions = new Transactions();
                objTransactions.ItemId = item.ItemId;
                objTransactions.Quantity = (-1)*item.Quantity;
                objTransactions.TransactionDate = DateTime.Now;
                objTransactions.TypeId = 2;
                objRestaurantDbEntities.Transactions.Add(objTransactions);
                objRestaurantDbEntities.SaveChanges();

            }
            return true;
        }
    }
}