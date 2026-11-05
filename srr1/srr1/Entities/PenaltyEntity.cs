using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace srr1.Entities
{
    public class PenaltyEntity
    {
        public int Id { get; set; }
        public string PenaltyId { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Reason { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
        public bool IsPaid { get; set; }
        public int ParkingSessionId { get; set; }
        public ParkingSessionEntity ParkingSession { get; set; } = null!;
    }
}
