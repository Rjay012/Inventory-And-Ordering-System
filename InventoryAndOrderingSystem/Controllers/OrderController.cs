using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using InventoryAndOrderingSystem.Models;
using InventoryAndOrderingSystem.Models.OrderModels;
using InventoryAndOrderingSystem.Repositories.OrderRepositories;
using InventoryAndOrderingSystem.Repositories.ProductRepositories;
using InventoryAndOrderingSystem.Services.OrderServices;
using InventoryAndOrderingSystem.Services.ProductServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryAndOrderingSystem.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderService _orderService;
        private readonly ProductService _productService;
        private readonly ProductRepository _productRepository;
        private readonly OrderRepository _orderRepository;

        public OrderController(ProductRepository productRepository, OrderRepository orderRepository, OrderService orderService, ProductService productService)
        {
            _orderService = orderService;
            _orderRepository = orderRepository;
            _productService = productService;
            _productRepository = productRepository;
        }

        [Authorize(Roles = "Manager, Customer")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Customer")]
        public IActionResult ListingCustomerOrder()
        {
            int customerID = Convert.ToInt32(GetClaim().ElementAt(0).Value);
            return PartialView("Partials/Modals/_CustomerOrder", _orderService.ViewListingCustomerOrder(customerID));
        }

        [Authorize(Roles = "Manager")]
        public IActionResult ListingOrder()
        {
            return PartialView("Partials/Tables/_OrderList", _orderService.ListingOrder());
        }

        [Authorize(Roles = "Manager")]
        public IActionResult OrderDeliveryDetail(OrderModel orderModel)
        {
            return PartialView("Partials/Modals/_DeliveryDetail", orderModel);
        }

        [Authorize(Roles = "Customer")]
        public IActionResult OrderDetailModal(int? productID)
        {
            if (productID == null)
            {
                return BadRequest();
            }

            Product product = _productRepository.FindByID((int)productID);

            if (product == null)
            {
                return BadRequest();
            }

            OrderModel productModel = new OrderModel()
            {
                ProductID = (int)productID,
                Price = product.Price,
                TotalQuantity = product.Quantity
            };

            //ViewBag.ProductQuantity = product.Quantity;

            return PartialView("Partials/Modals/_OrderDetails", productModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer")]
        public IActionResult Create([Bind]OrderModel orderModel)
        {
            if (orderModel == null)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                orderModel.UserID = Convert.ToInt32(GetClaim().ElementAt(0).Value);
                orderModel.DeliveryDate = null;
                if (_orderService.Create(orderModel) == true)
                {

                }
            }

            return NoContent();
        }

        [Authorize(Roles = "Manager, Customer")]
        public IActionResult CancelOrder([Bind("OrderID, ProductID, Quantity")]OrderModel orderModel)
        {
            if (orderModel == null)
            {
                return BadRequest();
            }

            if (_orderService.ChangeStatus(orderModel.OrderID, "cancelled") == true)
            {
                int quantity = _productRepository.FindByID(orderModel.ProductID)
                                                 .Quantity;
                _productService.ChangeQuantity(orderModel.ProductID, quantity + orderModel.Quantity);  //revert product quantity
                return Json(new { response = "success" });
            }

            return Json(new { response = "failed" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public IActionResult CreateDeliveryDate([Bind("OrderID, DeliveryDate")]OrderModel orderModel)
        {
            if (orderModel == null)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                if (_orderRepository.SetDeliveryDate(orderModel) == true)
                {
                    return Json(new { response = "success" });
                }
            }

            return Json(new { response = "failed" });
        }

        [Authorize(Roles = "Manager")]
        public IActionResult RejectCustomerOrder([Bind("OrderID, ProductID, Quantity")]OrderModel orderModel)
        {
            if (orderModel == null)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                if (_orderService.ChangeStatus(orderModel.OrderID, "rejected") == true)
                {
                    int quantity = _productRepository.FindByID(orderModel.ProductID)
                                                     .Quantity;
                    _productService.ChangeQuantity(orderModel.ProductID, quantity + orderModel.Quantity);  //revert product quantity
                }
            }
            return NoContent();
        }

        [Authorize(Roles = "Manager")]
        public IActionResult Delete(int? orderID)
        {
            if (orderID == null)
            {
                return BadRequest();
            }

            _orderRepository.Delete((int)orderID);

            return NoContent();
        }

        private IEnumerable<Claim> GetClaim()
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;

            return claims;
        }
    }
}