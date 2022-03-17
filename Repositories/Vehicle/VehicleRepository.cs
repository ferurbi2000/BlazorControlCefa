namespace BlazorControlCefa.Repositories.Vehicle
{
    using BlazorControlCefa.Application.Dtos.Vehicle;
    using BlazorControlCefa.Application.Wrappers;
    using BlazorControlCefa.Data;
    using BlazorControlCefa.Data.Entities;
    using BlazorControlCefa.Data.ScopeFactory;
    using BlazorControlCefa.Features;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class VehicleRepository : IVehicleRepository
    {
        private ApplicationDbContext _context;
        //private IServiceScopeFactory<ApplicationDbContext> _dbContext;

        public VehicleRepository(ApplicationDbContext context)
        {
            _context = context
                ?? throw new ArgumentNullException(nameof(context));
        }

        //public VehicleRepository(IServiceScopeFactory<ApplicationDbContext> dbContext)
        //{
        //    _dbContext = dbContext
        //        ?? throw new ArgumentNullException(nameof(dbContext));

        //    //using var scope = _dbContext.CreateScope();
        //    //_context = scope.GetRequiredService();
        //}

        public async Task<PagingResponse<Vehicle>> GetAllVehicleAsync(
            VehicleParametersDto vehicleParameters,
            MetaData metaData)
        {
            //using var scope = _dbContext.CreateScope();
            //var _context = scope.GetRequiredService();

            var vehicles = _context.Vehicles
                .OrderBy(p => p.Id)
                .Include(p => p.VehicleTypes)
                .Include(v => v.VehicleBrands)
                as IQueryable<Vehicle>;

            if (!string.IsNullOrWhiteSpace(vehicleParameters.Filters))
            {
                var lowerCaseSearchTerm = vehicleParameters.Filters.ToLower();
                vehicles = vehicles.Where(p =>
                    p.Plate.ToLower().Contains(lowerCaseSearchTerm) |
                    p.VehicleTypes.Name.ToLower().Contains(lowerCaseSearchTerm) |
                    p.VehicleBrands.Name.ToLower().Contains(lowerCaseSearchTerm) |
                    p.Color.ToLower().Contains(lowerCaseSearchTerm) |
                    p.VehicleCode.ToLower().Contains(lowerCaseSearchTerm) |
                    p.CompanyName.ToLower().Contains(lowerCaseSearchTerm)
                    );
            }

            var collection = await PagedList<Vehicle>
                .CreateAsync(vehicles, vehicleParameters.PageNumber, vehicleParameters.PageSize);

            var pagingResponse = new PagingResponse<Vehicle>
            {
                Items = collection,
                MetaData = collection.MetaData
            };

            return pagingResponse;
        }

        public async Task<PagingResponse<VehicleDetail>> GetAllVehiclePerPersonDetailAsync(
            VehicleParametersDto vehicleParameters,
            MetaData metaData,
            int PersonId)
        {
            //using var scope = _dbContext.CreateScope();
            //var _context = scope.GetRequiredService();


            var vehicles = _context.VehicleDetails
                .OrderBy(p => p.Id)
                .Include(p => p.Vehicles).ThenInclude(v => v.VehicleTypes)
                .Include(p => p.Vehicles).ThenInclude(v => v.VehicleBrands)
                .Include(v => v.Persons)
                as IQueryable<VehicleDetail>;

            //if (!string.IsNullOrWhiteSpace(vehicleParameters.Filters))
            //{
            //var lowerCaseSearchTerm = vehicleParameters.Filters.ToLower();
            vehicles = vehicles.Where(p =>
                p.PersonId == PersonId
                );
            //}

            var collection = await PagedList<VehicleDetail>
                .CreateAsync(vehicles, vehicleParameters.PageNumber, vehicleParameters.PageSize);

            var pagingResponse = new PagingResponse<VehicleDetail>
            {
                Items = collection,
                MetaData = collection.MetaData
            };

            return pagingResponse;
        }

        public async Task<List<VehicleDetail>> GetAllVehiclePerPersonDetailForAssingAsync(int personId)
        {
            //using var scope = _dbContext.CreateScope();
            //var _context = scope.GetRequiredService();

            var collection = await _context.VehicleDetails
                .OrderBy(p => p.Id)
                .Include(p => p.Vehicles).ThenInclude(v => v.VehicleTypes)
                .Include(p => p.Vehicles).ThenInclude(v => v.VehicleBrands)
                .Include(v => v.Persons)
                .Where(v => v.PersonId == personId && v.IsDeleted == false)
                .ToListAsync();

            return collection;
        }

        public async Task<List<Vehicle>> GetAllVehicleForSelectionAsync()
        {
            //using var scope = _dbContext.CreateScope();
            //var _context = scope.GetRequiredService();

            var collection = await _context.Vehicles
                .Include(p => p.VehicleTypes)
                .Include(p => p.VehicleBrands)
                .OrderBy(p => p.Id)
                .ToListAsync();
            return collection;
        }

        public async Task AddVehicle(Vehicle vehicle)
        {
            //using var scope = _dbContext.CreateScope();
            //var _context = scope.GetRequiredService();

            if (vehicle == null)
            {
                throw new ArgumentNullException(nameof(vehicle));
            }
            await _context.Vehicles.AddAsync(vehicle);
        }

        public async Task AddVehicleDetail(VehicleDetail vehicleDetail)
        {
            //using var scope = _dbContext.CreateScope();
            //var _context = scope.GetRequiredService();

            if (vehicleDetail == null)
            {
                throw new ArgumentNullException(nameof(vehicleDetail));
            }
            await _context.VehicleDetails.AddAsync(vehicleDetail);
        }

        public void DeleteVehicle(Vehicle vehicle)
        {
            //using var scope = _dbContext.CreateScope();
            //var _context = scope.GetRequiredService();

            if (vehicle == null)
            {
                throw new ArgumentNullException(nameof(vehicle));
            }
            _context.Vehicles.Remove(vehicle);
        }

        public void DeleteVehicleDetail(VehicleDetail vehicleDetail)
        {
            //using var scope = _dbContext.CreateScope();
            //var _context = scope.GetRequiredService();

            if (vehicleDetail == null)
            {
                throw new ArgumentNullException(nameof(vehicleDetail));
            }
            _context.VehicleDetails.Remove(vehicleDetail);
        }

        public async Task<Vehicle> GetVehicleAsync(int id)
        {
            //using var scope = _dbContext.CreateScope();
            //var _context = scope.GetRequiredService();

            return await _context.Vehicles
                .Include(p => p.VehicleTypes)
                .Include(v => v.VehicleBrands)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<VehicleDetail> GetVehicleDetailAsync(int id)
        {
            //using var scope = _dbContext.CreateScope();
            //var _context = scope.GetRequiredService();

            return await _context.VehicleDetails
                .Include(p => p.Vehicles).ThenInclude(v => v.VehicleTypes)
                .Include(p => p.Vehicles).ThenInclude(v => v.VehicleBrands)
                .Include(v => v.Persons)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public Vehicle GetVehicle(int id)
        {
            //using var scope = _dbContext.CreateScope();
            //var _context = scope.GetRequiredService();

            return _context.Vehicles
                .Include(p => p.VehicleTypes)
                .Include(v => v.VehicleBrands)
               .FirstOrDefault(p => p.Id == id);
        }

        public VehicleDetail GetVehicleDetail(int id)
        {
            //using var scope = _dbContext.CreateScope();
            //var _context = scope.GetRequiredService();

            return _context.VehicleDetails
                .Include(p => p.Vehicles).ThenInclude(v => v.VehicleTypes)
                .Include(p => p.Vehicles).ThenInclude(v => v.VehicleBrands)
                .Include(v => v.Persons)
               .FirstOrDefault(p => p.Id == id);
        }

        public bool Save()
        {
            //using var scope = _dbContext.CreateScope();
            //var _context = scope.GetRequiredService();

            return _context.SaveChanges() > 0;
        }

        public async Task<bool> SaveAsync()
        {
            //using var scope = _dbContext.CreateScope();
            //var _context = scope.GetRequiredService();

            return await _context.SaveChangesAsync() > 0;
        }

        public void UpdateVehicle(Vehicle vehicle)
        {
            //using var scope = _dbContext.CreateScope();
            //var _context = scope.GetRequiredService();

            _context.Entry(vehicle).State = EntityState.Modified;
        }

        public void UpdateVehicleDetail(VehicleDetail vehicleDetail)
        {
            //using var scope = _dbContext.CreateScope();
            //var _context = scope.GetRequiredService();

            _context.Entry(vehicleDetail).State = EntityState.Modified;
        }
    }
}
