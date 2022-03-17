namespace BlazorControlCefa.Repositories.Visit
{
    using BlazorControlCefa.Application.Dtos.Visit;
    using BlazorControlCefa.Application.Wrappers;
    using BlazorControlCefa.Data.Entities;
    using BlazorControlCefa.Features;
    public interface IVisitRepository
    {
        Task<PagingResponse<Visit>> GetAllVisitAsync(VisitParametersDto visitParameters, MetaData metaData);
        Task<List<VisitPersonDetail>> GetAllPersonPerVisitDetailForAssingAsync(int visitId);
        //Task<List<VisitVehicleDetail>> GetAllVehiclePerVisitDetailForAssingAsync(int visitId);
        Task<Visit> GetVisitAsync(int id);
        Visit GetVisit(int id);
        Task<VisitPersonDetail> GetVisitPersonDetailAsync(int id);
        //Task<VisitVehicleDetail> GetVisitVehicleDetailAsync(int id);
        Task<bool> FindAnyActiveVisit(int id);
        Task<bool> FindAnyPersonInVisitDetail(int id, int personId);
        //Task<bool> FindAnyVehicleInVisitDetail(int id, int vehicleId);
        Task AddVisit(Visit visit);
        Task AddVisitDetail(VisitPersonDetail visitPersoneDetail);
        //Task AddVisitVehicleDetail(VisitVehicleDetail visitVehicleDetail);
        void DeleteVisit(Visit visit);
        void DeleteVisitPersonDetail(VisitPersonDetail visitPersonDetail);
        //void DeleteVisitVehicleDetail(VisitVehicleDetail visitVehicleDetail);
        void UpdateVisit(Visit visit);
        bool Save();
        Task<bool> SaveAsync();
    }
}
