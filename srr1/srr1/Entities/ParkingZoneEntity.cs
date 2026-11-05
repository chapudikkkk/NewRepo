using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace srr1.Entities
{
    public class ParkingZoneEntity
    {
        public int Id { get; set; }
        public string ParkingZoneId { get; set; } = string.Empty;
        public decimal HourlyRate { get; set; }

        public ICollection<ParkingSessionEntity> ParkingSessions { get; set; } = new List<ParkingSessionEntity>();
    }
}
