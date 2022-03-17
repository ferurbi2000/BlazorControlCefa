namespace BlazorControlCefa.Repositories.PersonType
{
    using BlazorControlCefa.Application.Dtos.PersonType;
    using BlazorControlCefa.Application.Wrappers;
    using BlazorControlCefa.Data;
    using BlazorControlCefa.Data.Entities;
    using BlazorControlCefa.Data.ScopeFactory;
    using BlazorControlCefa.Features;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    public class PersonTypeRepository : IPersonTypeRepository
    {
        //private ApplicationDbContext _context;
        private IServiceScopeFactory<ApplicationDbContext> _dbContext;

        //public PersonTypeRepository(ApplicationDbContext context)
        //{
        //    _context = context
        //        ?? throw new ArgumentNullException(nameof(context));
        //}

        public PersonTypeRepository(IServiceScopeFactory<ApplicationDbContext> dbContext)
        {
            _dbContext = dbContext
                ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<PagingResponse<PersonType>> GetAllPersonTypeAsync(
            PersonTypeParametersDto personTypeParameters,
            MetaData metaData)
        {
            using var scope = _dbContext.CreateScope();
            var _context = scope.GetRequiredService();

            var personTypes = _context.PersonType
                .OrderBy(p => p.Id)
                as IQueryable<PersonType>;

            if (!string.IsNullOrWhiteSpace(personTypeParameters.Filters))
            {
                var lowerCaseSearchTerm = personTypeParameters.Filters.ToLower();
                personTypes = personTypes.Where(p =>
                    p.Name.ToLower().Contains(lowerCaseSearchTerm)
                    );
            }

            var collection = await PagedList<PersonType>
                .CreateAsync(personTypes, personTypeParameters.PageNumber, personTypeParameters.PageSize);

            var pagingResponse = new PagingResponse<PersonType>
            {
                Items = collection,
                MetaData = collection.MetaData
            };

            return pagingResponse;
        }

        public async Task<List<PersonType>> GetAllPersonTypeForSelectionAsync()
        {
            using var scope = _dbContext.CreateScope();
            var _context = scope.GetRequiredService();

            var collection = await _context.PersonType
                .OrderBy(p => p.Id)
                .ToListAsync();
            return collection;
        }

        public async Task AddPersonType(PersonType personType)
        {
            using var scope = _dbContext.CreateScope();
            var _context = scope.GetRequiredService();

            if (personType == null)
            {
                throw new ArgumentNullException(nameof(personType));
            }
            await _context.PersonType.AddAsync(personType);
        }

        public void DeletePersonType(PersonType personType)
        {
            using var scope = _dbContext.CreateScope();
            var _context = scope.GetRequiredService();

            if (personType == null)
            {
                throw new ArgumentNullException(nameof(personType));
            }
            _context.PersonType.Remove(personType);
        }

        public async Task<PersonType> GetPersonTypeAsync(int id)
        {
            using var scope = _dbContext.CreateScope();
            var _context = scope.GetRequiredService();

            return await _context.PersonType
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public PersonType GetPersonType(int id)
        {
            using var scope = _dbContext.CreateScope();
            var _context = scope.GetRequiredService();

            return _context.PersonType
                .FirstOrDefault(p => p.Id == id);
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

        public void UpdatePersonType(PersonType personType)
        {
            using var scope = _dbContext.CreateScope();
            var _context = scope.GetRequiredService();

            _context.Entry(personType).State = EntityState.Modified;
        }
    }
}
