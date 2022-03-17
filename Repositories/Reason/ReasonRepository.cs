namespace BlazorControlCefa.Repositories.Reason
{
    using BlazorControlCefa.Application.Dtos.Department;
    using BlazorControlCefa.Application.Dtos.Reason;
    using BlazorControlCefa.Application.Wrappers;
    using BlazorControlCefa.Data;
    using BlazorControlCefa.Data.Entities;
    using BlazorControlCefa.Features;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ReasonRepository : IReasonRepository
    {
        private ApplicationDbContext _context;

        public ReasonRepository(ApplicationDbContext context)
        {
            _context = context
                ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<PagingResponse<Reason>> GetAllReasonAsync(
            ReasonParametersDto reasonParameters,
            MetaData metaData)
        {
            var reasons = _context.Reasons
                .OrderBy(p => p.Id)
                as IQueryable<Reason>;

            if (!string.IsNullOrWhiteSpace(reasonParameters.Filters))
            {
                var lowerCaseSearchTerm = reasonParameters.Filters.ToLower();
                reasons = reasons.Where(p =>
                    p.Name.ToLower().Contains(lowerCaseSearchTerm)
                    );
            }

            var collection = await PagedList<Reason>
                .CreateAsync(reasons, reasonParameters.PageNumber, reasonParameters.PageSize);

            var pagingResponse = new PagingResponse<Reason>
            {
                Items = collection,
                MetaData = collection.MetaData
            };

            return pagingResponse;
        }

        public async Task<List<Reason>> GetAllReasonForSelectionAsync()
        {
            var collection = await _context.Reasons
                .OrderBy(p => p.Id)
                .ToListAsync();
            return collection;
        }

        public async Task AddReason(Reason reason)
        {
            if (reason == null)
            {
                throw new ArgumentNullException(nameof(reason));
            }
            await _context.Reasons.AddAsync(reason);
        }

        public void DeleteReason(Reason reason)
        {
            if (reason == null)
            {
                throw new ArgumentNullException(nameof(reason));
            }
            _context.Reasons.Remove(reason);
        }

        public async Task<Reason> GetReasonAsync(int id)
        {
            return await _context.Reasons
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public Reason GetReason(int id)
        {
            return _context.Reasons
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

        public void UpdateReason(Reason reason)
        {
            _context.Entry(reason).State = EntityState.Modified;
        }
    }
}
