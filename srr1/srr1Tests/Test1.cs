
using Microsoft.VisualStudio.TestTools.UnitTesting;
using srr1;
using srr1.Entities;
using srr1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace srr1Tests
{
    [TestClass]
    public class ParkingCatalogTests
    {
        private ParkingCatalog _catalog = null!;

        [TestInitialize]
        public void TestInitialize()
        {
            var zones = new List<ParkingZoneEntity>
            {
                new ParkingZoneEntity
                {
                    Id = 1,
                    ParkingZoneId = "ZONE-1",
                    HourlyRate = 35,
                    ParkingSessions = new List<ParkingSessionEntity>
                    {
                        new ParkingSessionEntity
                        {
                            Id = 1,
                            SessionId = "SESS-001",
                            CarPlate = "KA9988CB",
                            Payments = new List<PaymentEntity>
                            {
                                new PaymentEntity
                                {
                                    PaymentId = "PAY-001",
                                    Amount = 70,
                                    PaymentMethod = "ApplePay"
                                },
                                new PaymentEntity
                                {
                                    PaymentId = "PAY-002",
                                    Amount = 30,
                                    PaymentMethod = "GooglePay"
                                }
                            },
                            Penalties = new List<PenaltyEntity>
                            {
                                new PenaltyEntity
                                {
                                    PenaltyId = "PEN-001",
                                    Amount = 500,
                                    IsPaid = false
                                }
                            }
                        },
                        new ParkingSessionEntity
                        {
                            Id = 2,
                            SessionId = "SESS-002",
                            CarPlate = "KA1234AB",
                            Payments = new List<PaymentEntity>(),
                            Penalties = new List<PenaltyEntity>
                            {
                                new PenaltyEntity
                                {
                                    PenaltyId = "PEN-002",
                                    Amount = 300,
                                    IsPaid = true
                                }
                            }
                        }
                    }
                },
                new ParkingZoneEntity
                {
                    Id = 2,
                    ParkingZoneId = "ZONE-2",
                    HourlyRate = 40,
                    ParkingSessions = new List<ParkingSessionEntity>
                    {
                        new ParkingSessionEntity
                        {
                            Id = 3,
                            SessionId = "SESS-003",
                            CarPlate = "AA4567BC",
                            Payments = new List<PaymentEntity>
                            {
                                new PaymentEntity
                                {
                                    PaymentId = "PAY-003",
                                    Amount = 200,
                                    PaymentMethod = "Card"
                                }
                            },
                            Penalties = new List<PenaltyEntity>()
                        },
                        new ParkingSessionEntity
                        {
                            Id = 4,
                            SessionId = "SESS-004",
                            CarPlate = "KA7654KI",
                            Payments = new List<PaymentEntity>
                            {
                                new PaymentEntity
                                {
                                    PaymentId = "PAY-004",
                                    Amount = 0,
                                    PaymentMethod = "Cash"
                                }
                            },
                            Penalties = new List<PenaltyEntity>()
                        }
                    }
                }
            };

            IParkingRepository repository = new TestParkingRepository(zones);
            _catalog = new ParkingCatalog(repository);
        }

        [TestMethod]
        public async Task GetPaymentStatisticsAsync_ReturnsCorrectStatistics()
        {
            var result = await _catalog.GetPaymentStatisticsAsync();

            Assert.AreEqual(4, result.Count);

            var applePay = result.First(x =>
                (string)x.GetType().GetProperty("PaymentMethod")!.GetValue(x)! == "ApplePay");

            Assert.AreEqual(1, applePay.GetType().GetProperty("PaymentCount")!.GetValue(applePay));
            Assert.AreEqual(70m, applePay.GetType().GetProperty("TotalAmount")!.GetValue(applePay));
        }

        [TestMethod]
        public async Task GetMaxZone()
        {
            var result = await _catalog.MaxZone();
            Assert.IsTrue(result.HasValue);
            Assert.AreEqual("ZONE-2", result.Value.ParkingZoneId);
            Assert.AreEqual(200m, result.Value.TotalRevenue);
        }

        [TestMethod]
        public async Task GetZoneStatisticsAsync_ReturnsCorrectStatistics()
        {
            var result = await _catalog.GetZoneStatisticsAsync();

            Assert.AreEqual(2, result.Count);

            var zone = result.First(x =>
                (string)x.GetType().GetProperty("ParkingZoneId")!.GetValue(x)! == "ZONE-1");

            Assert.AreEqual(2, zone.GetType().GetProperty("SessionCount")!.GetValue(zone));
            Assert.AreEqual(2, zone.GetType().GetProperty("PaymentCount")!.GetValue(zone));
            Assert.AreEqual(100m, zone.GetType().GetProperty("TotalRevenue")!.GetValue(zone));
        }

        [TestMethod]
        public async Task GetAnomaliesAsync_FindsAllAnomalies()
        {
            var result = await _catalog.GetAnomaliesAsync();

            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        public async Task GetPenaltyStatisticsAsync_ReturnsCorrectStatistics()
        {
            var result = await _catalog.GetPenaltyStatisticsAsync();

            Assert.AreEqual(2, result.Count);

            var unpaid = result.First(x =>
                (string)x.GetType().GetProperty("IsPaid")!.GetValue(x)! == "Не оплачено");

            Assert.AreEqual(1, unpaid.GetType().GetProperty("PenaltyCount")!.GetValue(unpaid));
            Assert.AreEqual(500m, unpaid.GetType().GetProperty("TotalAmount")!.GetValue(unpaid));
        }
    }

    public class TestParkingRepository : IParkingRepository
    {
        private readonly List<ParkingZoneEntity> _zones;

        public TestParkingRepository(List<ParkingZoneEntity> zones)
        {
            _zones = zones;
        }

        public Task<List<ParkingZoneEntity>> GetAllAsync(
            CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_zones);
        }

        public Task<ParkingZoneEntity?> GetByIdAsync(
            string parkingZoneId,
            CancellationToken cancellationToken = default)
        {
            return Task.FromResult(
                _zones.FirstOrDefault(z => z.ParkingZoneId == parkingZoneId));
        }

        public Task AddAsync(
            ParkingZoneEntity zone,
            CancellationToken cancellationToken = default)
        {
            _zones.Add(zone);
            return Task.CompletedTask;
        }

        public Task<bool> DeleteAsync(
            string parkingZoneId,
            CancellationToken cancellationToken = default)
        {
            var zone = _zones.FirstOrDefault(z => z.ParkingZoneId == parkingZoneId);

            if (zone == null)
                return Task.FromResult(false);

            _zones.Remove(zone);
            return Task.FromResult(true);
        }

        public Task<List<ParkingSessionEntity>> FindAsync(
            Expression<Func<ParkingSessionEntity, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            var sessions = _zones
                .SelectMany(z => z.ParkingSessions)
                .AsQueryable()
                .Where(predicate)
                .ToList();

            return Task.FromResult(sessions);
        }
    }
}
