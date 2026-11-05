using srr1.Date;
using srr1.Entities;
using srr1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace srr1.Services
{
    public class DbInitializer
    {
        private readonly AppDbContext _context;
        private readonly IFileLoader _jsonReader;

        public DbInitializer(AppDbContext context, IFileLoader jsonReader)
        {
            _context = context;
            _jsonReader = jsonReader;
        }

        public async Task InitializeAsync(string jsonFilePath, CancellationToken cancellationToken = default)
        {
            await _context.Database.EnsureCreatedAsync(cancellationToken);

            if (await _context.ParkingZones.AnyAsync(cancellationToken))
                return;

            await foreach (var zoneDto in _jsonReader.LoadStreamAsync<ParkingZoneDto>(jsonFilePath, cancellationToken))
            {
                var zoneEntity = new ParkingZoneEntity
                {
                    ParkingZoneId = zoneDto.ParkingZoneId,
                    HourlyRate = zoneDto.HourlyRate
                };

                foreach (var sessionDto in zoneDto.ParkingSessions)
                {
                    var sessionEntity = new ParkingSessionEntity
                    {
                        SessionId = sessionDto.SessionId,
                        CarPlate = sessionDto.CarPlate,
                        ParkingZone = zoneEntity
                    };
                    foreach (var penaltyDto in sessionDto.Penalties)
                    {
                        var penaltyEntity = new PenaltyEntity
                        {
                            PenaltyId = penaltyDto.PenaltyId,
                            Amount = penaltyDto.Amount,
                            Reason = penaltyDto.Reason,
                            Timestamp = penaltyDto.Timestamp,
                            IsPaid = penaltyDto.IsPaid,
                            ParkingSession = sessionEntity
                        };

                        sessionEntity.Penalties.Add(penaltyEntity);
                    }
                    foreach (var paymentDto in sessionDto.Payments)
                    {
                        var paymentEntity = new PaymentEntity
                        {
                            PaymentId = paymentDto.PaymentId,
                            Amount = paymentDto.Amount,
                            PaymentMethod = paymentDto.PaymentMethod,
                            Timestamp = paymentDto.Timestamp,
                            ParkingSession = sessionEntity
                        };

                        sessionEntity.Payments.Add(paymentEntity);
                    }

                    zoneEntity.ParkingSessions.Add(sessionEntity);
                }

                _context.ParkingZones.Add(zoneEntity);
            }

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
