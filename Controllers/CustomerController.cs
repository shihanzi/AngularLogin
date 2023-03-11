using AngularLogin.Context;
using AngularLogin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AngularLogin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private AppDbContext _authDbContext;

        public CustomerController(AppDbContext appDbContext)
        {
            _authDbContext = appDbContext;
        }
        [HttpPost("RegisterCustomer")]
        public async Task<IActionResult> RegisterCustomer([FromBody] Customer CustomerObj)
        {
            if (CustomerObj == null)
                return BadRequest();

            if (await CheckCustomerIdExistAsync(CustomerObj.CustomerId))
                return BadRequest(new { Message = "Customer ID Already Exist" });

            await _authDbContext.Customers.AddAsync(CustomerObj);
            await _authDbContext.SaveChangesAsync();
            return Ok(new { Message = "Customer Registered Successfully" });
        }
        private Task<bool> CheckCustomerIdExistAsync(int CustomerId)
            => _authDbContext.Lots.AnyAsync(x => x.CustomerId == CustomerId);

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<Location>> GetAllCustomers()
        {
            return Ok(await _authDbContext.Customers.ToListAsync());
        }

        [HttpPut("updateCustomers")]
        public async Task<IActionResult> UpdateCustomer([FromBody] Customer CustomerObj)
        {
            if (CustomerObj == null)
                return BadRequest();

            var customer = await _authDbContext.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.CustomerId == CustomerObj.CustomerId);
            if (customer == null)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "Customer Not Found"
                });
            }
            else
            {
                _authDbContext.Customers.Update(CustomerObj);
                await _authDbContext.SaveChangesAsync();
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Customer Updated Successfully"
                });
            }
        }
    }
}
