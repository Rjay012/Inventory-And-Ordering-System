using InventoryAndOrderingSystem.Models;
using InventoryAndOrderingSystem.Models.OrderModels;
using InventoryAndOrderingSystem.Repositories.OrderRepositories;
using InventoryAndOrderingSystem.Services.ProductServices;
using System.Collections.Generic;
using System.Linq;

namespace InventoryAndOrderingSystem.Services.OrderServices
{
    public class OrderService
    {
        private readonly OrderRepository _orderRepository;
        private readonly ProductService _productService;
        public OrderService(OrderRepository orderRepository, ProductService productService)
        {
            _orderRepository = orderRepository;
            _productService = productService;
        }

        public bool Create(OrderModel orderModel)
        {
            Order order = new Order()
            {
                ProductID = orderModel.ProductID,
                ShipingAddress = orderModel.ShippingAddress,
                UserID = orderModel.UserID,
                Status = "on process"
            };

            //bind child attrib
            order.OrderDetails.Add(
                new OrderDetail()
                {
                    Price = orderModel.Price * orderModel.Quantity,
                    Quantity = orderModel.Quantity
                }
            );

            if (_orderRepository.Create(order) == true)
            {
                int deduction = orderModel.TotalQuantity - orderModel.Quantity;
                _productService.ChangeQuantity(orderModel.ProductID, deduction);
                return true;
            }

            return false;
        }

        public List<OrderModel> ViewListingCustomerOrder(int customerID)
        {
            return _orderRepository.ListingOrder()
                                   .Where(o => o.Order.UserID == customerID)
                                   .Select(s => new OrderModel
                                   {
                                       OrderID = s.OrderID,
                                       ProductID = s.Order.Product.ProductID,
                                       ProductName = s.Order.Product.ProductName,
                                       Price = s.Price,
                                       Quantity = s.Quantity,
                                       DeliveryDate = s.Order.DeliveryDate,
                                       ShippingAddress = s.Order.ShipingAddress,
                                       Status = s.Order.Status
                                   })
                                   .ToList();
        }

        public bool ChangeStatus(int orderID, string status)
        {
            OrderModel orderModel = new OrderModel()
            {
                OrderID = orderID,
                Status = status
            };

            return _orderRepository.ChangeStatus(orderModel);
        }

        public List<OrderModel> ListingOrder(int columnIndx, string order)
        {
            List<OrderModel> orderModels = _orderRepository.ListingOrder()
                                                           .Select(s => new OrderModel
                                                           {
                                                               OrderID = s.OrderID,
                                                               ProductID = s.Order.Product.ProductID,
                                                               CustomerName = s.Order.User.Name,
                                                               ProductName = s.Order.Product.ProductName,
                                                               Price = s.Price,
                                                               Quantity = s.Quantity,
                                                               DeliveryDate = s.Order.DeliveryDate,
                                                               ShippingAddress = s.Order.ShipingAddress,
                                                               Status = s.Order.Status
                                                           })
                                                           .ToList();

            switch(columnIndx)
            {
                case 2:
                    orderModels = order == "asc" ? orderModels.OrderBy(o => o.CustomerName).ToList() : orderModels.OrderByDescending(o => o.CustomerName).ToList();
                    break;
                case 3:
                    orderModels = order == "asc" ? orderModels.OrderBy(o => o.ShippingAddress).ToList() : orderModels.OrderByDescending(o => o.ShippingAddress).ToList();
                    break;
                case 4:
                    orderModels = order == "asc" ? orderModels.OrderBy(o => o.DeliveryDate).ToList() : orderModels.OrderByDescending(o => o.DeliveryDate).ToList();
                    break;
                case 6:
                    orderModels = order == "asc" ? orderModels.OrderBy(o => o.Status).ToList() : orderModels.OrderByDescending(o => o.Status).ToList();
                    break;
            }
            return orderModels;
        }
    }
}
