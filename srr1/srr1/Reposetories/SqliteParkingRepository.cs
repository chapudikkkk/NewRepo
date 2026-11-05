using Microsoft.EntityFrameworkCore;
using srr1.Entities;
using srr1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace srr1.Repositories
{
    public class SQLiteParkingRepository : IParkingRepository
    {
        private readonly AppDbContext _context;

        public SQLiteParkingRepository(AppDbContext context)
        {
            _context = context;
        }

        // Отримання всіх зон паркування
        public async Task<List<ParkingZoneEntity>> GetAllAsync(
            CancellationToken cancellationToken = default)
        {
            return await _context.ParkingZones
                .Include(z => z.ParkingSessions)
                    .ThenInclude(s => s.Payments)
                .Include(z => z.ParkingSessions)
                    .ThenInclude(s => s.Penalties)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        // Отримання зони за її ідентифікатором
        public async Task<ParkingZoneEntity?> GetByIdAsync(
            string parkingZoneId,
            CancellationToken cancellationToken = default)
        {
            return await _context.ParkingZones
                .Include(z => z.ParkingSessions)
                    .ThenInclude(s => s.Payments)
                .Include(z => z.ParkingSessions)
                    .ThenInclude(s => s.Penalties)
                .FirstOrDefaultAsync(
                    z => z.ParkingZoneId == parkingZoneId,
                    cancellationToken);
        }

        // Додавання нової зони паркування
        public async Task AddAsync(
            ParkingZoneEntity zone,
            CancellationToken cancellationToken = default)
        {
            await _context.ParkingZones.AddAsync(zone, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        // Видалення зони паркування
        public async Task<bool> DeleteAsync(
            string parkingZoneId,
            CancellationToken cancellationToken = default)
        {
            var zone = await _context.ParkingZones
                .FirstOrDefaultAsync(
                    z => z.ParkingZoneId == parkingZoneId,
                    cancellationToken);

            if (zone == null)
                return false;

            _context.ParkingZones.Remove(zone);

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }

        // Пошук паркувальних сесій за умовою
        public async Task<List<ParkingSessionEntity>> FindAsync(
            Expression<Func<ParkingSessionEntity, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            return await _context.ParkingSessions
                .Where(predicate)
                .Include(s => s.Payments)
                .Include(s => s.Penalties)
                .Include(s => s.ParkingZone)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
    }
}