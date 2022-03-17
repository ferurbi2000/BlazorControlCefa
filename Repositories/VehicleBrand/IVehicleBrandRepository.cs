namespace BlazorControlCefa.Repositories.VehicleBrand
{
    using BlazorControlCefa.Application.Dtos.VehicleBrand;
    using BlazorControlCefa.Application.Wrappers;
    using BlazorControlCefa.Data.Entities;
    using BlazorControlCefa.Features;
    public interface IVehicleBrandRepository
    {
        Task<PagingResponse<VehicleBrand>> GetAllVehicleBrandAsync(VehicleBrandParametersDto vehicleBrandParameters, MetaData metaData);
        Task<List<VehicleBrand>> GetAllVehicleBrandForSelectionAsync();
        Task<VehicleBrand> GetVehicleBrandAsync(int id);
        VehicleBrand GetVehicleBrand(int id);
        Task AddVehicleBrand(VehicleBrand vehicleBrand);
        void DeleteVehicleBrand(VehicleBrand vehicleBrand);
        void UpdateVehicleBrand(VehicleBrand vehicleBrand);
        bool Save();
        Task<bool> SaveAsync();
    }
}
