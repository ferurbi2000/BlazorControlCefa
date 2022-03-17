namespace BlazorControlCefa.Repositories.VehicleBrand
{
    using BlazorControlCefa.Application.Dtos.VehicleBrand;
    using BlazorControlCefa.Application.Wrappers;
    using BlazorControlCefa.Data;
    using BlazorControlCefa.Data.Entities;
    using BlazorControlCefa.Features;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public class VehicleBrandRepository : IVehicleBrandRepository
    {
        private ApplicationDbContext _context;

        public VehicleBrandRepository(ApplicationDbContext context)
        {
            _context = context
                ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<PagingResponse<VehicleBrand>> GetAllVehicleBrandAsync(VehicleBrandParametersDto vehicleBrandParameters, MetaData metaData)
        {
            var vehicleBrands = _context.VehicleBrands
                .OrderBy(v => v.Id)
                as IQueryable<VehicleBrand>;

            if (!string.IsNullOrWhiteSpace(vehicleBrandParameters.Filters))
            {
                var lowerCaseSearchTerm = vehicleBrandParameters.Filters.ToLower();
                vehicleBrands = vehicleBrands.Where(p =>
                    p.Name.ToLower().Contains(lowerCaseSearchTerm)
                    );
            }

            var collection = await PagedList<VehicleBrand>
                .CreateAsync(vehicleBrands, vehicleBrandParameters.PageNumber, vehicleBrandParameters.PageSize);

            var pagingResponse = new PagingResponse<VehicleBrand>
            {
                Items = collection,
                MetaData = collection.MetaData
            };

            return pagingResponse;
        }

        public async Task<List<VehicleBrand>> GetAllVehicleBrandForSelectionAsync()
        {
            var collection = await _context.VehicleBrands
                .OrderBy(v => v.Name)
                .ToListAsync();
            return collection;
        }

        public async Task AddVehicleBrand(VehicleBrand vehicleBrand)
        {
            if (vehicleBrand == null)
            {
                throw new ArgumentNullException(nameof(vehicleBrand));
            }
            await _context.VehicleBrands.AddAsync(vehicleBrand);
        }

        public void DeleteVehicleBrand(VehicleBrand vehicleBrand)
        {
            if (vehicleBrand == null)
            {
                throw new ArgumentNullException(nameof(vehicleBrand));
            }
            _context.VehicleBrands.Remove(vehicleBrand);
        }

        public async Task<VehicleBrand> GetVehicleBrandAsync(int id)
        {
            return await _context.VehicleBrands
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public VehicleBrand GetVehicleBrand(int id)
        {
            return _context.VehicleBrands
               .FirstOrDefault(p => p.Id == id);
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void UpdateVehicleBrand(VehicleBrand vehicleBrand)
        {
            _context.Entry(vehicleBrand).State = EntityState.Modified;
        }
    }
}
