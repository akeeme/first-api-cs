using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace myAPI.Models;


[ApiController]
public class CustomerController : ControllerBase{
    private readonly myAPIDBContext _context;

    public CustomerController(myAPIDBContext context){
        _context = context;
    }

    // GET: api/Customer
    [HttpGet("/api/customer")]
    public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers(){
        return await _context.Customers.ToListAsync();
    }

    // GET: api/Customer/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Customer>> GetCustomer(int id){
        var customer = await _context.Customers.FindAsync(id);

        if (customer == null){
            return NotFound();
        }

        return customer;
    }

    // DELETE: /api/Customer/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(int id){
        var customer = await _context.Customers.Include(c => c.Email).FirstOrDefaultAsync(c => c.CustomerId == id);
        if (customer == null){
            return NotFound();
        }

        _context.Customers.Remove(customer);

        var email = await _context.Emails.FirstOrDefaultAsync(e => e.EmailId == customer.Email.EmailId);

        _context.Emails.Remove(email!);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // POST: /api/Customer
    [HttpPost("/api/customer")]
    public async Task<ActionResult<Customer>> PostCustomer(Customer customer){
        _context.Customers.Add(customer);

        await _context.SaveChangesAsync();

        return CreatedAtAction("GetCustomer", new { id = customer.CustomerId }, customer);
    }
}