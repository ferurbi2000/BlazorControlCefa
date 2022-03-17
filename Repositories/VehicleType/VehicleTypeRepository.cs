namespace BlazorControlCefa.Repositories.VehicleType
{
    using BlazorControlCefa.Application.Dtos.VehicleType;
    using BlazorControlCefa.Application.Wrappers;
    using BlazorControlCefa.Data;
    using BlazorControlCefa.Data.Entities;
    using BlazorControlCefa.Features;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class VehicleTypeRepository: IVehicleTypeRepository
    {
        private ApplicationDbContext _context;

        public VehicleTypeRepository(ApplicationDbContext context)
        {
            _context = context
                ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<PagingResponse<VehicleType>> GetAllVehicleTypeAsync(VehicleTypeParametersDto vehicleTypeParameters, MetaData metaData)
        {
            var vehicleTypes = _context.VehicleTypes
                .OrderBy(v => v.Id)
                as IQueryable<VehicleType>;

            if (!string.IsNullOrWhiteSpace(vehicleTypeParameters.Filters))
            {
                var lowerCaseSearchTerm = vehicleTypeParameters.Filters.ToLower();
                vehicleTypes = vehicleTypes.Where(p =>
                    p.Name.ToLower().Contains(lowerCaseSearchTerm)
                    );
            }

            var collection = await PagedList<VehicleType>
                .CreateAsync(vehicleTypes, vehicleTypeParameters.PageNumber, vehicleTypeParameters.PageSize);

            var pagingResponse = new PagingResponse<VehicleType>
            {
                Items = collection,
                MetaData = collection.MetaData
            };

            return pagingResponse;
        }

        public async Task<List<VehicleType>> GetAllVehicleTypeForSelectionAsync()
        {
            var collection = await _context.VehicleTypes
                .OrderBy(v => v.Name)
                .ToListAsync();
            return collection;
        }

        public async Task AddVehicleType(VehicleType vehicleType)
        {
            if (vehicleType == null)
            {
                throw new ArgumentNullException(nameof(vehicleType));
            }
            await _context.VehicleTypes.AddAsync(vehicleType);
        }

        public void DeleteVehicleType(VehicleType vehicleType)
        {
            if (vehicleType == null)
            {
                throw new ArgumentNullException(nameof(vehicleType));
            }
            _context.VehicleTypes.Remove(vehicleType);
        }

        public async Task<VehicleType> GetVehicleTypeAsync(int id)
        {
            return await _context.VehicleTypes
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public VehicleType GetVehicleType(int id)
        {
            return _context.VehicleTypes
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

        public void UpdateVehicleType(VehicleType vehicleType)
        {
            _context.Entry(vehicleType).State = EntityState.Modified;
        }
    }
}
