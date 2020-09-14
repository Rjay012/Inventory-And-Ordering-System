using InventoryAndOrderingSystem.Models;
using InventoryAndOrderingSystem.Models.CustomerModels;
using InventoryAndOrderingSystem.Repositories.CustomerRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryAndOrderingSystem.Services.CustomerServices
{
    public class CustomerService
    {
        private readonly CustomerRepository _customerRepository;
        public CustomerService(CustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public List<CustomerModel> ListingCustomers()
        {
            return _customerRepository.Customer()
                                      .Select(c => new CustomerModel
                                      {
                                          CustomerID = c.UserID,
                                          Username = c.Username,
                                          Name = c.Name,
                                          DateCreated = Convert.ToDateTime(c.DateCreated),
                                          DateUpdated = Convert.ToDateTime(c.DateUpdated)
                                      })
                                      .ToList();
        }

        public bool Create(CustomerModel customerModel)
        {
            customerModel.DateCreated = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
            return _customerRepository.Create(BindCustomerInfo(customerModel));
        }

        public bool Edit(CustomerModel customerModel)
        {
            customerModel.DateUpdated = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
            return _customerRepository.Edit(BindCustomerInfo(customerModel));
        }

        private User BindCustomerInfo(CustomerModel customerModel)
        {
            User user = new User()
            {
                UserID = customerModel.CustomerID,
                Username = customerModel.Username,
                Name = customerModel.Name,
                DateCreated = customerModel.DateCreated,
                DateUpdated = customerModel.DateUpdated,
                Password = customerModel.Password,
                RoleID = 2
            };

            return user;
        }

        public CustomerModel CustomerDetail(int customerID)
        {
            User user = _customerRepository.FindByID(customerID);

            CustomerModel customerModel = new CustomerModel()
            {
                CustomerID = user.UserID,
                Username = user.Username,
                Name = user.Name,
                DateCreated = Convert.ToDateTime(user.DateCreated)
            };

            return customerModel;
        }
    }
}
