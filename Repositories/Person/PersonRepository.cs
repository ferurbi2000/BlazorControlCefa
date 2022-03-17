namespace BlazorControlCefa.Repositories.Person
{
    using BlazorControlCefa.Application.Dtos.Person;
    using BlazorControlCefa.Application.Wrappers;
    using BlazorControlCefa.Data;
    using BlazorControlCefa.Data.Entities;
    using BlazorControlCefa.Data.ScopeFactory;
    using BlazorControlCefa.Features;
    using Microsoft.EntityFrameworkCore;

    public class PersonRepository : IPersonRepository
    {
        private ApplicationDbContext _context;
        // private IServiceScopeFactory<ApplicationDbContext> _dbContext;

        public PersonRepository(ApplicationDbContext context)
        {
           _context = context
               ?? throw new ArgumentNullException(nameof(context));
        }

        // public PersonRepository(IServiceScopeFactory<ApplicationDbContext> dbContext)
        // {
        //     _dbContext = dbContext
        //         ?? throw new ArgumentNullException(nameof(dbContext));
        // }

        public async Task<PagingResponse<Person>> GetAllPersonAsync(
            PersonParametersDto personParameters,
            MetaData metaData)
        {
            // using var scope = _dbContext.CreateScope();
            // var _context = scope.GetRequiredService();

            var persons = _context.People
                .Include(p => p.PersonTypes)
                .Include(p => p.Departments)
                .Include(p => p.Positions)
                .OrderBy(p => p.Id)
                as IQueryable<Person>;


            if (!string.IsNullOrWhiteSpace(personParameters.Filters))
            {
                var lowerCaseSearchTerm = personParameters.Filters.ToLower();
                persons = persons.Where(p =>
                    p.FirstName.ToLower().Contains(lowerCaseSearchTerm) ||
                    p.LastName.ToLower().Contains(lowerCaseSearchTerm) ||
                    p.Cedula.ToLower().Contains(lowerCaseSearchTerm)
                    );
            }

            var collection = await PagedList<Person>
                .CreateAsync(persons, personParameters.PageNumber, personParameters.PageSize);

            var pagingResponse = new PagingResponse<Person>
            {
                Items = collection,
                MetaData = collection.MetaData
            };

            return pagingResponse;
        }

        public async Task<List<Person>> GetAllPersonForSelectionAsync()
        {
            // using var scope = _dbContext.CreateScope();
            // var _context = scope.GetRequiredService();

            var collection = await _context.People
                .Include(p => p.PersonTypes)
                .OrderBy(p => p.FirstName)
                .ToListAsync();
            return collection;
        }

        public async Task<List<Person>> GetAllEmployeeForSelectionAsync()
        {
            // using var scope = _dbContext.CreateScope();
            // var _context = scope.GetRequiredService();

            var collection = await _context.People
                .Include(p => p.PersonTypes)
                .OrderBy(p => p.FirstName)
                .Where(p => p.PersonTypeId == 1)
                .ToListAsync();
            return collection;
        }

        public async Task AddPerson(Person person)
        {
            // using var scope = _dbContext.CreateScope();
            // var _context = scope.GetRequiredService();

            if (person == null)
            {
                throw new ArgumentNullException(nameof(person));
            }
            await _context.People.AddAsync(person);
        }

        public void DeletePerson(Person person)
        {
            // using var scope = _dbContext.CreateScope();
            // var _context = scope.GetRequiredService();

            if (person == null)
            {
                throw new ArgumentNullException(nameof(person));
            }
            _context.People.Remove(person);
        }

        public async Task<Person> GetPersonAsync(int id)
        {
            // using var scope = _dbContext.CreateScope();
            // var _context = scope.GetRequiredService();

            return await _context.People
                .Include(p => p.PersonTypes)
                .Include(p => p.Departments)
                .Include(p => p.Positions)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public Person GetPerson(int id)
        {
            // using var scope = _dbContext.CreateScope();
            // var _context = scope.GetRequiredService();

            return _context.People
                .Include(p => p.PersonTypes)
                .Include(p => p.Departments)
                .Include(p => p.Positions)
                .FirstOrDefault(p => p.Id == id);
        }

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

        public void UpdatePerson(Person person)
        {
            // using var scope = _dbContext.CreateScope();
            // var _context = scope.GetRequiredService();

            _context.Entry(person).State = EntityState.Modified;
        }
    }
}
