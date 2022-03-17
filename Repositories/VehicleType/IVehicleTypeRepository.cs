namespace BlazorControlCefa.Repositories.VehicleType
{   
    using BlazorControlCefa.Application.Dtos.VehicleType;
    using BlazorControlCefa.Application.Wrappers;
    using BlazorControlCefa.Data.Entities;
    using BlazorControlCefa.Features;
    public interface IVehicleTypeRepository
    {
        Task<PagingResponse<VehicleType>> GetAllVehicleTypeAsync(VehicleTypeParametersDto vehicleTypeParameters, MetaData metaData);
        Task<List<VehicleType>> GetAllVehicleTypeForSelectionAsync();
        Task<VehicleType> GetVehicleTypeAsync(int id);
        VehicleType GetVehicleType(int id);
        Task AddVehicleType(VehicleType vehicleType);
        void DeleteVehicleType(VehicleType vehicleType);
        void UpdateVehicleType(VehicleType vehicleType);
        bool Save();
        Task<bool> SaveAsync();
    }
}
