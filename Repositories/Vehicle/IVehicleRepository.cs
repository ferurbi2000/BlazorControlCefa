namespace BlazorControlCefa.Repositories.Vehicle
{
    using BlazorControlCefa.Application.Dtos.Vehicle;
    using BlazorControlCefa.Application.Wrappers;
    using BlazorControlCefa.Data.Entities;
    using BlazorControlCefa.Features;

    public interface IVehicleRepository
    {
        Task<PagingResponse<Vehicle>> GetAllVehicleAsync(VehicleParametersDto vehicleParameters, MetaData metaData);
        Task<PagingResponse<VehicleDetail>> GetAllVehiclePerPersonDetailAsync(VehicleParametersDto vehicleParameters, MetaData metaData, int PersonId);        
        Task<List<Vehicle>> GetAllVehicleForSelectionAsync();
        Task<List<VehicleDetail>> GetAllVehiclePerPersonDetailForAssingAsync(int personId);
        Task<Vehicle> GetVehicleAsync(int id);
        Task<VehicleDetail> GetVehicleDetailAsync(int id);
        Vehicle GetVehicle(int id);
        VehicleDetail GetVehicleDetail(int id);
        Task AddVehicle(Vehicle vehicle);
        Task AddVehicleDetail(VehicleDetail vehicleDetail);
        void DeleteVehicle(Vehicle vehicle);
        void DeleteVehicleDetail(VehicleDetail vehicleDetail);
        void UpdateVehicle(Vehicle vehicle);
        void UpdateVehicleDetail(VehicleDetail vehicleDetail);
        bool Save();
        Task<bool> SaveAsync();
    }
}
