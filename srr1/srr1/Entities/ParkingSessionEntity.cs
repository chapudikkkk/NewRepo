using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace srr1.Entities
{
    public class ParkingSessionEntity
    {
        public int Id { get; set; }
        public string SessionId { get; set; } = string.Empty;
        public string CarPlate { get; set; } = string.Empty;

        public int ParkingZoneId { get; set; }
        public ParkingZoneEntity ParkingZone { get; set; } = null!;

        public ICollection<PaymentEntity> Payments { get; set; } = new List<PaymentEntity>();

        public decimal TotalAmount => Payments.Sum(p => p.Amount);
        public ICollection<PenaltyEntity> Penalties { get; set; } = new List<PenaltyEntity>();
    }
}
