namespace BlazorControlCefa.Repositories.Visit
{
    using BlazorControlCefa.Application.Dtos.Visit;
    using BlazorControlCefa.Application.Wrappers;
    using BlazorControlCefa.Data;
    using BlazorControlCefa.Data.Entities;
    using BlazorControlCefa.Data.ScopeFactory;
    using BlazorControlCefa.Features;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    public class VisitRepository : IVisitRepository
    {
        private ApplicationDbContext _context;
        // private IServiceScopeFactory<ApplicationDbContext> _dbContext;

        public VisitRepository(ApplicationDbContext context)
        {
            _context = context
                ?? throw new ArgumentNullException(nameof(context));
        }

        // public VisitRepository(IServiceScopeFactory<ApplicationDbContext> dbContext)
        // {
        //    _dbContext = dbContext
        //        ?? throw new ArgumentNullException(nameof(dbContext));
        // }

        public async Task<PagingResponse<Visit>> GetAllVisitAsync(
            VisitParametersDto visitParameters,
            MetaData metaData)
        {
            // using var scope = _dbContext.CreateScope();
            // var _context = scope.GetRequiredService();

            //var persons = _context.People
            //    .Include(p=>p.Visits)

            var visits = _context.Visits
                .Include(p => p.Departments)
                .Include(p => p.PersonApprovers)
                .Include(p => p.PersonVisitTos).ThenInclude(p => p.PersonTypes)
                .Include(p => p.PersonVisitTos).ThenInclude(p => p.Departments)
                .Include(p => p.PersonVisitTos).ThenInclude(p => p.Positions)
                .Include(p => p.Reasons)
                .Include(p => p.VisitTypes)
                .Include(p => p.VisitDetails).ThenInclude(p => p.Persons)
                .Include(p => p.VisitDetails).ThenInclude(p => p.Persons).ThenInclude(p => p.PersonTypes)
                .Where(p => p.IsCheckIn == true)
                .OrderBy(p => p.Id)
                as IQueryable<Visit>;




            if (!string.IsNullOrWhiteSpace(visitParameters.Filters))
            {
                var lowerCaseSearchTerm = visitParameters.Filters.ToLower();
                visits = visits.Where(p => (
                    p.PersonVisitTos.FirstName.ToLower().Contains(lowerCaseSearchTerm) ||
                    p.PersonVisitTos.LastName.ToLower().Contains(lowerCaseSearchTerm) ||
                    p.Reasons.Name.ToLower().Contains(lowerCaseSearchTerm)) ||
                    (
                        p.VisitDetails.FirstOrDefault().Persons.FirstName.ToLower().Contains(lowerCaseSearchTerm) ||
                        p.VisitDetails.FirstOrDefault().Persons.LastName.ToLower().Contains(lowerCaseSearchTerm))
                    );
            }

            var collection = await PagedList<Visit>
                .CreateAsync(visits, visitParameters.PageNumber, visitParameters.PageSize);

            var pagingResponse = new PagingResponse<Visit>
            {
                Items = collection,
                MetaData = collection.MetaData
            };

            return pagingResponse;
        }

        public async Task<List<VisitPersonDetail>> GetAllPersonPerVisitDetailForAssingAsync(int visitId)
        {
            // using var scope = _dbContext.CreateScope();
            // var _context = scope.GetRequiredService();

            var collection = await _context.VisitPersonDetails
                .OrderBy(v => v.Id)
                .Include(v => v.Persons).ThenInclude(v => v.PersonTypes)
                .Where(v => v.VisitId == visitId && v.IsDeleted == false)
                .ToListAsync();

            return collection;
        }

        //public async Task<List<VisitVehicleDetail>> GetAllVehiclePerVisitDetailForAssingAsync(int visitId)
        //{
        //    var collection = await _context.VisitVehicleDetails
        //        .OrderBy(v => v.Id)
        //        .Include(v => v.Vehicles).ThenInclude(v => v.VehicleTypes)
        //        .Where(v => v.VisitId == visitId && v.IsDeleted == false)
        //        .ToListAsync();

        //    return collection;
        //}

        public async Task AddVisit(Visit visit)
        {
            // using var scope = _dbContext.CreateScope();
            // var _context = scope.GetRequiredService();

            if (visit == null)
            {
                throw new ArgumentNullException(nameof(visit));
            }
            await _context.Visits.AddAsync(visit);
        }

        public async Task AddVisitDetail(VisitPersonDetail visitPersoneDetail)
        {
            // using var scope = _dbContext.CreateScope();
            // var _context = scope.GetRequiredService();

            if (visitPersoneDetail == null)
            {
                throw new ArgumentNullException(nameof(visitPersoneDetail));
            }
            await _context.VisitPersonDetails.AddAsync(visitPersoneDetail);
        }

        //public async Task AddVisitVehicleDetail(VisitVehicleDetail visitVehicleDetail)
        //{
        //    if (visitVehicleDetail == null)
        //    {
        //        throw new ArgumentNullException(nameof(visitVehicleDetail));
        //    }
        //    await _context.VisitVehicleDetails.AddAsync(visitVehicleDetail);
        //}

        public void DeleteVisit(Visit visit)
        {
            // using var scope = _dbContext.CreateScope();
            // var _context = scope.GetRequiredService();

            if (visit == null)
            {
                throw new ArgumentNullException(nameof(visit));
            }
            _context.Visits.Remove(visit);
        }

        public void DeleteVisitPersonDetail(VisitPersonDetail visitPersonDetail)
        {
            // using var scope = _dbContext.CreateScope();
            // var _context = scope.GetRequiredService();

            if (visitPersonDetail == null)
            {
                throw new ArgumentNullException(nameof(visitPersonDetail));
            }
            _context.VisitPersonDetails.Remove(visitPersonDetail);
        }

        //public void DeleteVisitVehicleDetail(VisitVehicleDetail visitVehicleDetail)
        //{
        //    if (visitVehicleDetail == null)
        //    {
        //        throw new ArgumentNullException(nameof(visitVehicleDetail));
        //    }
        //    _context.VisitVehicleDetails.Remove(visitVehicleDetail);
        //}

        public async Task<Visit> GetVisitAsync(int id)
        {
            // using var scope = _dbContext.CreateScope();
            // var _context = scope.GetRequiredService();

            return await _context.Visits
                .Include(p => p.Departments)
                .Include(p => p.PersonApprovers)
                .Include(p => p.PersonVisitTos)
                .Include(p => p.Reasons)
                .Include(p => p.VisitTypes)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public Visit GetVisit(int id)
        {
            // using var scope = _dbContext.CreateScope();
            // var _context = scope.GetRequiredService();

            return _context.Visits
                .Include(p => p.Departments)
                .Include(p => p.PersonApprovers)
                .Include(p => p.PersonVisitTos)
                .Include(p => p.Reasons)
                .Include(p => p.VisitTypes)
                .FirstOrDefault(p => p.Id == id);
        }

        public async Task<VisitPersonDetail> GetVisitPersonDetailAsync(int id)
        {
            // using var scope = _dbContext.CreateScope();
            // var _context = scope.GetRequiredService();

            return await _context.VisitPersonDetails
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        //public async Task<VisitVehicleDetail> GetVisitVehicleDetailAsync(int id)
        //{
        //    return await _context.VisitVehicleDetails
        //        .FirstOrDefaultAsync(v => v.Id == id);
        //}

        public async Task<bool> FindAnyActiveVisit(int id)
        {
            // using var scope = _dbContext.CreateScope();
            // var _context = scope.GetRequiredService();

            return await _context.Visits
                .Include(p => p.VisitDetails).ThenInclude(p => p.Persons)
                .Where(p => (p.IsCheckIn == true) &&
                    p.VisitDetails.FirstOrDefault().Persons.Id == id
                    )
                .AnyAsync();
        }

        public async Task<bool> FindAnyPersonInVisitDetail(int id, int personId)
        {
            // using var scope = _dbContext.CreateScope();
            // var _context = scope.GetRequiredService();

            return await _context.VisitPersonDetails
                .Where(p => p.VisitId == id && p.PersonId == personId && p.IsDeleted == false)
                .AnyAsync();
        }

        //public async Task<bool> FindAnyVehicleInVisitDetail(int id, int vehicleId)
        //{
        //    return await _context.VisitVehicleDetails
        //        .Where(p => p.VisitId == id && p.VehicleId == vehicleId && p.IsDeleted == false)
        //        .AnyAsync();
        //}

        public bool Save()
        {
            // using var scope = _dbContext.CreateScope();
            // var _context = scope.GetRequiredService();

            return _context.SaveChanges() > 0;
        }

        public async Task<bool> SaveAsync()
        {
            // using var scope = _dbContext.CreateScope();
            // var _context = scope.GetRequiredService();

            return await _context.SaveChangesAsync() > 0;
        }

        public void UpdateVisit(Visit visit)
        {
            // using var scope = _dbContext.CreateScope();
            // var _context = scope.GetRequiredService();

            _context.Entry(visit).State = EntityState.Modified;
        }
    }
}
