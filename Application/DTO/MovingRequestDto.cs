using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class MovingRequestDto
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public string PickupAddress { get; set; }
        public string DeliveryAddress { get; set; }
        public DateTime MovingDate { get; set; }
        public string MovingType { get; set; } // e.g., Local, Long Distance
        public string Status { get; set; } // e.g., Pending, Confirmed, Completed, Cancelled
    }
}
