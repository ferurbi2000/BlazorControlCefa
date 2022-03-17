namespace BlazorControlCefa.Repositories.Reason
{
    using BlazorControlCefa.Application.Dtos.Reason;
    using BlazorControlCefa.Application.Wrappers;
    using BlazorControlCefa.Data.Entities;
    using BlazorControlCefa.Features;
    public interface IReasonRepository
    {
        Task<PagingResponse<Reason>> GetAllReasonAsync(ReasonParametersDto reasonParameters, MetaData metaData);
        Task<List<Reason>> GetAllReasonForSelectionAsync();
        Task<Reason> GetReasonAsync(int id);
        Reason GetReason(int id);
        Task AddReason(Reason reason);
        void DeleteReason(Reason reason);
        void UpdateReason(Reason reason);
        bool Save();
        Task<bool> SaveAsync();
    }
}
