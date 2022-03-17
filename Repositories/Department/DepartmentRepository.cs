namespace BlazorControlCefa.Repositories.Department
{
    using BlazorControlCefa.Application.Dtos.Department;
    using BlazorControlCefa.Application.Wrappers;
    using BlazorControlCefa.Data;
    using BlazorControlCefa.Data.Entities;
    using BlazorControlCefa.Features;
    using Microsoft.EntityFrameworkCore;


    public class DepartmentRepository : IDepartmentRepository
    {
        private ApplicationDbContext _context;

        public DepartmentRepository(ApplicationDbContext context)
        {
            _context = context
                ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<PagingResponse<Department>> GetAllDepartmentAsync(
            DepartmentParametersDto departmentParameters,
            MetaData metaData)
        {
            var departments = _context.Departments
                .OrderBy(p => p.Id)
                as IQueryable<Department>;

            if (!string.IsNullOrWhiteSpace(departmentParameters.Filters))
            {
                var lowerCaseSearchTerm = departmentParameters.Filters.ToLower();
                departments = departments.Where(p =>
                    p.Name.ToLower().Contains(lowerCaseSearchTerm)
                    );
            }

            var collection = await PagedList<Department>
                .CreateAsync(departments, departmentParameters.PageNumber, departmentParameters.PageSize);

            var pagingResponse = new PagingResponse<Department>
            {
                Items = collection,
                MetaData = collection.MetaData
            };

            return pagingResponse;
        }

        public async Task<List<Department>> GetAllDepartmentForSelectionAsync()
        {
            var collection = await _context.Departments
                .OrderBy(p => p.Id)
                .ToListAsync();
            return collection;
        }

        public async Task AddDepartment(Department department)
        {
            if (department == null)
            {
                throw new ArgumentNullException(nameof(department));
            }
            await _context.Departments.AddAsync(department);
        }

        public void DeleteDepartment(Department department)
        {
            if (department == null)
            {
                throw new ArgumentNullException(nameof(department));
            }
            _context.Departments.Remove(department);
        }

        public async Task<Department> GetDepartmentAsync(int id)
        {
            return await _context.Departments
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public Department GetDepartment(int id)
        {
            return _context.Departments
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

        public void UpdateDepartment(Department department)
        {
            _context.Entry(department).State = EntityState.Modified;
        }
    }
}
