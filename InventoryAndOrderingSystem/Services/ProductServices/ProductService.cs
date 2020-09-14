using InventoryAndOrderingSystem.Models;
using InventoryAndOrderingSystem.Models.ProductModels;
using InventoryAndOrderingSystem.Repositories.ProductRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryAndOrderingSystem.Services.ProductServices
{
    public class ProductService
    {
        private readonly ProductRepository _productRepository;
        public ProductService(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public bool Create(ProductModel productModel)
        {
            productModel.DateCreated = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
            return _productRepository.Create(FillProductEntity(productModel));
        }

        public bool Edit(ProductModel productModel)
        {
            return _productRepository.Edit(FillProductEntity(productModel));
        }

        private Product FillProductEntity(ProductModel productModel)
        {
            Product product = new Product()
            {
                ProductID = productModel.ProductID,
                ProductName = productModel.ProductName,
                Quantity = productModel.Quantity,
                Price = productModel.Price,
                Description = productModel.Description,
                DateCreated = productModel.DateCreated,
                DateUpdated = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"))
            };
            return product;
        }

        public List<ProductModel> ViewListingProduct()
        {
            return _productRepository.Product()
                                     .Select(p => new ProductModel
                                     {
                                         ProductID = p.ProductID,
                                         ProductName = p.ProductName,
                                         Price = p.Price,
                                         Quantity = p.Quantity,
                                         DateCreated = p.DateCreated,
                                         DateUpdated = p.DateUpdated,
                                         Description = p.Description
                                     })
                                     .ToList();
        }

        public ProductModel ProductDetail(int productID)
        {
            Product product = _productRepository.FindByID(productID);

            ProductModel productModel = new ProductModel()
            {
                ProductID = product.ProductID,
                ProductName = product.ProductName,
                Price = product.Price,
                Quantity = product.Quantity,
                DateCreated = product.DateCreated,
                DateUpdated = product.DateUpdated,
                Description = product.Description
            };

            return productModel;
        }

        public void ChangeQuantity(int productID, int quantity)  //deduct/revert quantity
        {
            ProductModel productModel = new ProductModel()
            {
                ProductID = productID,
                Quantity = quantity
            };
            
            _productRepository.ChangeQuantity(productModel);
        }
    }
}
