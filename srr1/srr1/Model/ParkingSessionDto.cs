using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace srr1.Model
{
    public class ParkingSessionDto
    {
        public string SessionId { get; set; } = string.Empty;
        public string CarPlate { get; set; } = string.Empty;

        public List<PenaltyDto> Penalties { get; set; } = new List<PenaltyDto>();
        public List<PaymentDto> Payments { get; set; }=new List<PaymentDto>();
    }
}
