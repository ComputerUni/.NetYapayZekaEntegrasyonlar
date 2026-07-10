using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCoreAI.Project1_ApiDemo.Context;
using NetCoreAI.Project1_ApiDemo.Entities;

namespace NetCoreAI.Project1_ApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ApiContext _context;

        public CustomersController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult CustomerList()
        {
            var value = _context.Customers.ToList();
            return Ok(value);
        }

        [HttpPost]
        public IActionResult CreateCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return Ok("Müşteri Ekleme İşlemi Başarılı");
        }

        [HttpDelete]
        public IActionResult DeleteCustomer(int id)
        {
            var value = _context.Customers.Find(id);
            _context.Customers.Remove(value);
            _context.SaveChanges();
            return Ok("Müşteri Başarıyla Silindi");
        }

        [HttpGet("{id}")]
        public IActionResult GetByIdCustomer(int id)
        {
            var value = _context.Customers.Find(id);
            if(value == null)
            {
                return BadRequest("Bu ID'ye Ait Bir Müşteri Kaydı Bulunamadı");
            }
            return Ok("Müşteri Kaydı Başarıyla Getirildi");
        }

        [HttpPut]
        public IActionResult UpdateCustomer(Customer customer)
        {
            _context.Customers.Update(customer);
            _context.SaveChanges();
            return Ok("Müşteri Bilgileri Başarıyla Güncellendi");
        }
    }
}
