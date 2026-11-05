using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace srr1.Entities
{
    public class PaymentEntity
    {
        public int Id { get; set; }
        public string PaymentId { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }

        public int ParkingSessionId { get; set; }
        public ParkingSessionEntity ParkingSession { get; set; } = null!;
    }
}
