using InventoryAndOrderingSystem.Models;
using InventoryAndOrderingSystem.Models.OrderModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace InventoryAndOrderingSystem.Repositories.OrderRepositories
{
    public class OrderRepository
    {
        private readonly IOSContext _context;
        public OrderRepository(IOSContext context)
        {
            _context = context;
        }

        public bool Create(Order order)
        {
            _context.Add(order);
            try
            {
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IQueryable<OrderDetail> ListingOrder()
        {
            return _context.OrderDetails;
        }

        public bool ChangeStatus(OrderModel orderModel)
        {
            List<SqlParameter> param = new List<SqlParameter>
            {
               new SqlParameter("@OrderID", orderModel.OrderID),
               new SqlParameter("@Status", orderModel.Status)
            };

            try
            {
                _context.Database.ExecuteSqlRaw("[dbo].[ChangeOrderStatus] @OrderID, @Status", param.ToList());
                return true;
            }
            catch
            {
                return false;
            }
        }

        private Order FindByID(int orderID)
        {
            return _context.Orders
                           .Find(orderID);
        }

        public void Delete(int orderID)
        {
            _context.Orders.Remove(FindByID(orderID));
            _context.SaveChanges();
        }

        public bool SetDeliveryDate(OrderModel orderModel)
        {
            List<SqlParameter> param = new List<SqlParameter>
            {
               new SqlParameter("@OrderID", orderModel.OrderID),
               new SqlParameter("@DeliveryDate", orderModel.DeliveryDate)
            };

            try
            {
                _context.Database.ExecuteSqlRaw("[dbo].[SetDeliveryDate] @OrderID, @DeliveryDate", param.ToList());
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
