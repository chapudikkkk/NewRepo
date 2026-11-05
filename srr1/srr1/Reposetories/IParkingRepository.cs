using srr1.Entities;
using srr1.Specifications;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace srr1.Services
{

    public interface IParkingRepository
    {
        Task<List<ParkingZoneEntity>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<ParkingZoneEntity?> GetByIdAsync(string parkingZoneId, CancellationToken cancellationToken = default);

        Task AddAsync(ParkingZoneEntity zone, CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(string parkingZoneId, CancellationToken cancellationToken = default);
        Task<List<ParkingSessionEntity>> FindAsync(Expression<Func<ParkingSessionEntity, bool>> predicate,CancellationToken cancellationToken = default);

    }
}