using srr1.Entities;
using srr1.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace srr1
{
    public class ParkingCatalog
    {
        private readonly IParkingRepository _repository;

        public ParkingCatalog(IParkingRepository repository)
        {
            _repository = repository;
        }

        public Task<List<ParkingZoneEntity>> GetCatalogAsync()
        {
            return _repository.GetAllAsync();
        }

        public Task AddZoneAsync(ParkingZoneEntity zone)
        {
            return _repository.AddAsync(zone);
        }

        public Task<bool> RemoveZoneAsync(string zoneId)
        {
            return _repository.DeleteAsync(zoneId);
        }

        public async Task<List<ParkingSessionEntity>> FilterSessionsAsync(decimal minAmount)
        {
            var zones = await GetCatalogAsync();

            return zones
                .SelectMany(z => z.ParkingSessions)
                .Where(s => s.Payments.Sum(p => p.Amount) >= minAmount)
                .ToList();
        }

        public async Task<List<object>> GetPaymentStatisticsAsync()
        {
            var zones = await GetCatalogAsync();

            return zones
                .SelectMany(z => z.ParkingSessions)
                .SelectMany(s => s.Payments)
                .GroupBy(p => p.PaymentMethod)
                .Select(g => (object)new
                {
                    PaymentMethod = g.Key,
                    PaymentCount = g.Count(),
                    TotalAmount = g.Sum(p => p.Amount),
                    AverageAmount = g.Average(p => p.Amount)
                })
                .ToList();
        }

        public async Task<List<object>> GetZoneStatisticsAsync()
        {
            var zones = await GetCatalogAsync();

            return zones
                .Select(z => (object)new
                {
                    ParkingZoneId = z.ParkingZoneId,
                    SessionCount = z.ParkingSessions.Count,
                    PaymentCount = z.ParkingSessions.Sum(s => s.Payments.Count),
                    TotalRevenue = z.ParkingSessions
                        .SelectMany(s => s.Payments)
                        .Sum(p => p.Amount)
                })
                .ToList();
        }

        public async Task<List<object>> GetAnomaliesAsync()
        {
            var zones = await GetCatalogAsync();

            var sessions = zones.SelectMany(z => z.ParkingSessions);

            var unpaidSessions = sessions
                .Where(s => !s.Payments.Any())
                .Select(s => (object)new
                {
                    SessionId = s.SessionId,
                    CarPlate = s.CarPlate,
                    Problem = "Сесія без платежів",
                    Amount = 0m
                });

            var invalidPayments = sessions
                .SelectMany(s => s.Payments
                    .Where(p => p.Amount <= 0)
                    .Select(p => (object)new
                    {
                        SessionId = s.SessionId,
                        CarPlate = s.CarPlate,
                        Problem = "Некоректна сума платежу",
                        Amount = p.Amount
                    }));

            var unpaidPenalties = sessions
                .SelectMany(s => s.Penalties
                    .Where(p => !p.IsPaid)
                    .Select(p => (object)new
                    {
                        SessionId = s.SessionId,
                        CarPlate = s.CarPlate,
                        Problem = "Неоплачений штраф",
                        Amount = p.Amount
                    }));

            return unpaidSessions
                .Concat(invalidPayments)
                .Concat(unpaidPenalties)
                .ToList();
        }

        public async Task<List<object>> GetPenaltyStatisticsAsync()
        {
            var zones = await GetCatalogAsync();

            return zones
                .SelectMany(z => z.ParkingSessions)
                .SelectMany(s => s.Penalties)
                .GroupBy(p => p.IsPaid)
                .Select(g => (object)new
                {
                    IsPaid = g.Key ? "Оплачено" : "Не оплачено",
                    PenaltyCount = g.Count(),
                    TotalAmount = g.Sum(p => p.Amount)
                })
                .ToList();
        }
public async Task<(string ParkingZoneId, decimal TotalRevenue)?> MaxZone()
{
    var zones = await GetCatalogAsync();

    return zones
        .Select(z => new
        {
            ParkingZoneId = z.ParkingZoneId,
            TotalRevenue = z.ParkingSessions
                .SelectMany(s => s.Payments)
                .Sum(p => p.Amount)
        })
        .OrderByDescending(z => z.TotalRevenue)
        .Select(z => ((string ParkingZoneId, decimal TotalRevenue)?)
            (z.ParkingZoneId, z.TotalRevenue))
        .FirstOrDefault();
}
        
    }
}