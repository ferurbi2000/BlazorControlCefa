namespace BlazorControlCefa.Repositories.Visit
{
    using BlazorControlCefa.Data;
    using BlazorControlCefa.Data.Entities;
    using Microsoft.EntityFrameworkCore;
    using BlazorControlCefa.Data.ScopeFactory;

    public class VehicleVisitDetailRepository : IVehicleVisitDetailRepository
    {
        //private ApplicationDbContext _context;
        private IServiceScopeFactory<ApplicationDbContext> _dbContext;

        //public VehicleVisitDetailRepository(ApplicationDbContext context)
        //{
        //    _context = context
        //        ?? throw new ArgumentNullException(nameof(context));
        //}

        public VehicleVisitDetailRepository(IServiceScopeFactory<ApplicationDbContext> dbContext)
        {
            _dbContext = dbContext
                ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<List<VisitVehicleDetail>> GetAllVehiclePerVisitDetailForAssingAsync(int visitId)
        {
            using var scope = _dbContext.CreateScope();
            var _context = scope.GetRequiredService();

            var collection = await _context.VisitVehicleDetails
                .OrderBy(v => v.Id)
                .Include(v => v.Vehicles).ThenInclude(v => v.VehicleTypes)
                .Where(v => v.VisitId == visitId && v.IsDeleted == false)
                .ToListAsync();

            return collection;
        }

        public async Task AddVisitVehicleDetail(VisitVehicleDetail visitVehicleDetail)
        {
            using var scope = _dbContext.CreateScope();
            var _context = scope.GetRequiredService();

            if (visitVehicleDetail == null)
            {
                throw new ArgumentNullException(nameof(visitVehicleDetail));
            }
            await _context.VisitVehicleDetails.AddAsync(visitVehicleDetail);
        }

        public void DeleteVisitVehicleDetail(VisitVehicleDetail visitVehicleDetail)
        {
            using var scope = _dbContext.CreateScope();
            var _context = scope.GetRequiredService();

            if (visitVehicleDetail == null)
            {
                throw new ArgumentNullException(nameof(visitVehicleDetail));
            }
            _context.VisitVehicleDetails.Remove(visitVehicleDetail);
        }

        public async Task<VisitVehicleDetail> GetVisitVehicleDetailAsync(int id)
        {
            using var scope = _dbContext.CreateScope();
            var _context = scope.GetRequiredService();

            return await _context.VisitVehicleDetails
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<bool> FindAnyVehicleInVisitDetail(int id, int vehicleId)
        {
            using var scope = _dbContext.CreateScope();
            var _context = scope.GetRequiredService();

            return await _context.VisitVehicleDetails
                .Where(p => p.VisitId == id && p.VehicleId == vehicleId && p.IsDeleted == false)
                .AnyAsync();
        }

        public bool Save()
        {
            using var scope = _dbContext.CreateScope();
            var _context = scope.GetRequiredService();

            return _context.SaveChanges() > 0;
        }

        public async Task<bool> SaveAsync()
        {
            using var scope = _dbContext.CreateScope();
            var _context = scope.GetRequiredService();

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
