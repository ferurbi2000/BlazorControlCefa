namespace BlazorControlCefa.Repositories.VisitType
{
    using BlazorControlCefa.Application.Dtos.VisitType;
    using BlazorControlCefa.Application.Wrappers;
    using BlazorControlCefa.Data;
    using BlazorControlCefa.Data.Entities;
    using BlazorControlCefa.Features;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public class VisitTypeRepository : IVisitTypeRepository
    {
        private ApplicationDbContext _context;

        public VisitTypeRepository(ApplicationDbContext context)
        {
            _context = context
                ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<PagingResponse<VisitType>> GetAllVisitTypeAsync(
            VisitTypeParametersDto visitTypeParameters,
            MetaData metaData)
        {
            var visitTypes = _context.VisitTypes
                .OrderBy(p => p.Id)
                as IQueryable<VisitType>;

            if (!string.IsNullOrWhiteSpace(visitTypeParameters.Filters))
            {
                var lowerCaseSearchTerm = visitTypeParameters.Filters.ToLower();
                visitTypes = visitTypes.Where(p =>
                    p.Name.ToLower().Contains(lowerCaseSearchTerm)
                    );
            }

            var collection = await PagedList<VisitType>
                .CreateAsync(visitTypes, visitTypeParameters.PageNumber, visitTypeParameters.PageSize);

            var pagingResponse = new PagingResponse<VisitType>
            {
                Items = collection,
                MetaData = collection.MetaData
            };

            return pagingResponse;
        }

        public async Task<List<VisitType>> GetAllVisitTypeForSelectionAsync()
        {
            var collection = await _context.VisitTypes
               .OrderBy(p => p.Id)
               .ToListAsync();
            return collection;
        }

        public async Task AddVisitType(VisitType visitType)
        {
            if (visitType == null)
            {
                throw new ArgumentNullException(nameof(visitType));
            }
            await _context.VisitTypes.AddAsync(visitType);
        }

        public void DeleteVisitType(VisitType visitType)
        {
            if (visitType == null)
            {
                throw new ArgumentNullException(nameof(visitType));
            }
            _context.VisitTypes.Remove(visitType);
        }

        public async Task<VisitType> GetVisitTypeAsync(int id)
        {
            return await _context.VisitTypes
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public VisitType GetVisitType(int id)
        {
            return _context.VisitTypes
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

        public void UpdateVisitType(VisitType visitType)
        {
            _context.Entry(visitType).State = EntityState.Modified;
        }
    }
}
