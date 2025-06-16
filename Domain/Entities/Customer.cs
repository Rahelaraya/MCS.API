using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string? Name { get; set; }    // Nullable
        public string? Email { get; set; }   // Nullable
        public string? Phone { get; set; }   // Nullable
        public string? Address { get; set; } // Nullable


    }
}
