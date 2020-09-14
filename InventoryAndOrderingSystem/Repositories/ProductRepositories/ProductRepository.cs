using InventoryAndOrderingSystem.Models;
using InventoryAndOrderingSystem.Models.ProductModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryAndOrderingSystem.Repositories.ProductRepositories
{
    public class ProductRepository
    {
        private readonly IOSContext _context;
        public ProductRepository(IOSContext context)
        {
            _context = context;
        }

        public bool Create(Product product)
        {
            try
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Edit(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
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

        public IQueryable<Product> Product()
        {
            return _context.Products;
        }

        public Product FindByID(int productID)
        {
            return _context.Products
                           .Find(productID);
        }

        public void Delete(int productID)
        {
            _context.Products.Remove(FindByID(productID));
            _context.SaveChanges();
        }

        public void ChangeQuantity(ProductModel productModel)  //apply deduction/revertion of quantity
        {
            List<SqlParameter> param = new List<SqlParameter>
            {
               new SqlParameter("@ProductID", productModel.ProductID),
               new SqlParameter("@Quantity", productModel.Quantity)
            };

            _context.Database.ExecuteSqlRaw("[dbo].[ChangeQuantity] @ProductID, @Quantity", param.ToList());
        }
    }
}
