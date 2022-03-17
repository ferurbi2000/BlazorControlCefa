namespace BlazorControlCefa.Repositories.Department
{
    using BlazorControlCefa.Application.Dtos.Department;
    using BlazorControlCefa.Application.Wrappers;
    using BlazorControlCefa.Data.Entities;
    using BlazorControlCefa.Features;

    public interface IDepartmentRepository
    {
        Task<PagingResponse<Department>> GetAllDepartmentAsync(DepartmentParametersDto departmentParameters, MetaData metaData);
        Task<List<Department>> GetAllDepartmentForSelectionAsync();
        Task<Department> GetDepartmentAsync(int id);
        Department GetDepartment(int id);
        Task AddDepartment(Department department);
        void DeleteDepartment(Department department);
        void UpdateDepartment(Department department);
        bool Save();
        Task<bool> SaveAsync();
    }
}
