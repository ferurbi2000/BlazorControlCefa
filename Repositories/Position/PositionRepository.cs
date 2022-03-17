namespace BlazorControlCefa.Repositories.Position
{
    using BlazorControlCefa.Application.Dtos.Position;
    using BlazorControlCefa.Application.Wrappers;
    using BlazorControlCefa.Data;
    using BlazorControlCefa.Data.Entities;
    using BlazorControlCefa.Features;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class PositionRepository : IPositionRepository
    {
        private ApplicationDbContext _context;

        public PositionRepository(ApplicationDbContext context)
        {
            _context = context
                ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<PagingResponse<Position>> GetAllPositionAsync(
            PositionParametersDto positionParameters,
            MetaData metaData)
        {
            var positions = _context.Positions
                .OrderBy(p => p.Id)
                as IQueryable<Position>;

            if (!string.IsNullOrWhiteSpace(positionParameters.Filters))
            {
                var lowerCaseSearchTerm = positionParameters.Filters.ToLower();
                positions = positions.Where(p =>
                    p.Name.ToLower().Contains(lowerCaseSearchTerm)
                    );
            }

            var collection = await PagedList<Position>
                .CreateAsync(positions, positionParameters.PageNumber, positionParameters.PageSize);

            var pagingResponse = new PagingResponse<Position>
            {
                Items = collection,
                MetaData = collection.MetaData
            };

            return pagingResponse;
        }

        public async Task<List<Position>> GetAllPositionForSelectionAsync()
        {
            var collection = await _context.Positions
                .OrderBy(p => p.Id)
                .ToListAsync();
            return collection;
        }

        public async Task AddPosition(Position position)
        {
            if (position == null)
            {
                throw new ArgumentNullException(nameof(position));
            }
            await _context.Positions.AddAsync(position);
        }

        public void DeletePosition(Position position)
        {
            if (position == null)
            {
                throw new ArgumentNullException(nameof(position));
            }
            _context.Positions.Remove(position);
        }

        public async Task<Position> GetPositionAsync(int id)
        {
            return await _context.Positions
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public Position GetPosition(int id)
        {
            return _context.Positions
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

        public void UpdatePosition(Position position)
        {
            _context.Entry(position).State = EntityState.Modified;
        }
    }
}
