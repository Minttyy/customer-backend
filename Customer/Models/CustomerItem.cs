using Microsoft.AspNetCore.Http;

namespace Customer.Models
{
    public class CustomerItem
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
