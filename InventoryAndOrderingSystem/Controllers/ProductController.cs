using System.Collections.Generic;
using System.Linq;
using InventoryAndOrderingSystem.Models.ProductModels;
using InventoryAndOrderingSystem.Repositories.ProductRepositories;
using InventoryAndOrderingSystem.Services.ProductServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryAndOrderingSystem.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService _productService;
        private readonly ProductRepository _productRepository;
        public ProductController(ProductService productService, ProductRepository productRepository)
        {
            _productService = productService;
            _productRepository = productRepository;
        }

        [Authorize(Roles = "Manager")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Customer")]
        public IActionResult Selection()
        {
            return View(nameof(Selection));
        }

        [Authorize(Roles = "Manager")]
        public IActionResult ListingProductTable()
        {
            List<ProductModel> productModels = _productService.ViewListingProduct()
                                                              .OrderByDescending(p => p.DateCreated)
                                                              .ToList();
            return PartialView("Partials/Tables/_Product", productModels);
        }

        public IActionResult ListingProduct()
        {
            return PartialView("Partials/Cards/_ProductList", _productService.ViewListingProduct());
        }
        
        [Authorize(Roles = "Manager")]
        public IActionResult CreateProductModal()
        {
            return PartialView("Partials/Modals/_AddProduct");
        }

        [Authorize(Roles = "Manager")]
        public IActionResult EditProductModal(int? productID)
        {
            if(productID == null)
            {
                return BadRequest();
            }

            return PartialView("Partials/Modals/_EditProduct", _productService.ProductDetail((int)productID));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public IActionResult Create([Bind]ProductModel productModel)
        {
            if(productModel == null)
            {
                return BadRequest();
            }

            if(ModelState.IsValid)
            {
                if (_productService.Create(productModel) == true)
                {
                    //created
                }
            }
            return NoContent();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public IActionResult Edit([Bind]ProductModel productModel)
        {
            if (productModel == null)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                if (_productService.Edit(productModel) == true)
                {
                    //updated
                }
            }
            return NoContent();
        }

        [Authorize(Roles = "Manager")]
        public IActionResult Delete(int? productID)
        {
            if (productID == null)
            {
                return BadRequest();
            }
            _productRepository.Delete((int)productID);
            return NoContent();
        }
    }
}