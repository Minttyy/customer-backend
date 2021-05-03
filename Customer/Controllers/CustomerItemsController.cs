using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Customer.Models;

namespace Customer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerItemsController : ControllerBase
    {
        private readonly CustomerContext _context;

        public CustomerItemsController(CustomerContext context)
        {
            _context = context;
        }

        // GET: api/CustomerItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerItem>>> GetCustomerItems()
        {
            return await _context.CustomerItems.ToListAsync();
        }

        // GET: api/CustomerItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerItem>> GetCustomerItem(long id)
        {
            var customerItem = await _context.CustomerItems.FindAsync(id);

            if (customerItem == null)
            {
                return NotFound();
            }

            return customerItem;
        }

        // PUT: api/CustomerItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomerItem(long id, CustomerItem customerItem)
        {
            if (id != customerItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(customerItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CustomerItems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<CustomerItem>> PostCustomerItem(CustomerItem customerItem)
        {
            _context.CustomerItems.Add(customerItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCustomerItem), new { id = customerItem.Id }, customerItem);
        }
        // DELETE: api/CustomerItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CustomerItem>> DeleteCustomerItem(long id)
        {
            var customerItem = await _context.CustomerItems.FindAsync(id);
            if (customerItem == null)
            {
                return NotFound();
            }

            _context.CustomerItems.Remove(customerItem);
            await _context.SaveChangesAsync();

            return customerItem;
        }

        private bool CustomerItemExists(long id)
        {
            return _context.CustomerItems.Any(e => e.Id == id);
        }
    }
}
