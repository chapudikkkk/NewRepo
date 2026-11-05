using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace srr1.Model
{
    public class ParkingZoneDto
    {
        public string ParkingZoneId { get; set; } = string.Empty;
        public decimal HourlyRate { get; set; } 
        public List<ParkingSessionDto> ParkingSessions{ get; set; } = new List<ParkingSessionDto>();

    }
}
