namespace BlazorControlCefa.Repositories.Visit
{
    using BlazorControlCefa.Data.Entities;
    public interface IVehicleVisitDetailRepository
    {
        Task<List<VisitVehicleDetail>> GetAllVehiclePerVisitDetailForAssingAsync(int visitId);
        Task AddVisitVehicleDetail(VisitVehicleDetail visitVehicleDetail);
        void DeleteVisitVehicleDetail(VisitVehicleDetail visitVehicleDetail);
        Task<VisitVehicleDetail> GetVisitVehicleDetailAsync(int id);
        Task<bool> FindAnyVehicleInVisitDetail(int id, int vehicleId);
        bool Save();
        Task<bool> SaveAsync();
    }
}
