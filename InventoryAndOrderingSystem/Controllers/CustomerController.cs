using InventoryAndOrderingSystem.Models.CustomerModels;
using InventoryAndOrderingSystem.Repositories.CustomerRepositories;
using InventoryAndOrderingSystem.Services.CustomerServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryAndOrderingSystem.Controllers
{
    public class CustomerController : Controller
    {
        private readonly CustomerService _customerService;
        private readonly CustomerRepository _customerRepository;

        public CustomerController(CustomerService customerService, CustomerRepository customerRepository)
        {
            _customerService = customerService;
            _customerRepository = customerRepository;
        }

        [Authorize(Roles = "Manager")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Manager")]
        public IActionResult ListingCustomer()
        {
            return PartialView("Partials/Tables/_CustomerList", _customerService.ListingCustomers());
        }

        [Authorize(Roles = "Manager")]
        public IActionResult AddCustomerModal(CustomerModel customerModel)
        {
            return PartialView("Partials/Modals/_AddCustomer", customerModel);
        }

        [Authorize(Roles = "Manager")]
        public IActionResult EditCustomerDetail(int? customerID)
        {
            if(customerID == null)
            {
                return NotFound();
            }

            return PartialView("Partials/Modals/_EditCustomer", _customerService.CustomerDetail((int)customerID));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public IActionResult Create([Bind]CustomerModel customerModel)
        {
            if(customerModel == null)
            {
                return BadRequest();
            }

            if(ModelState.IsValid)
            {
                if(_customerService.Create(customerModel) == true)
                {
                    return Json(new { response = "success" });
                }
            }

            return Json(new { response = "failed" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public IActionResult Edit([Bind]CustomerModel customerModel)
        {
            if (customerModel == null)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                if (_customerService.Edit(customerModel) == true)
                {
                    return Json(new { response = "success" });
                }
            }

            return Json(new { response = "failed" });
        }

        [Authorize(Roles = "Manager")]
        public IActionResult Delete(int? customerID)
        {
            if (customerID == null)
            {
                return BadRequest();
            }

            _customerRepository.Delete((int)customerID);

            return NoContent();
        }
    }
}