namespace BlazorControlCefa.Repositories.VisitType
{
    using BlazorControlCefa.Application.Dtos.VisitType;
    using BlazorControlCefa.Application.Wrappers;
    using BlazorControlCefa.Data.Entities;
    using BlazorControlCefa.Features;
    public interface IVisitTypeRepository
    {
        Task<PagingResponse<VisitType>> GetAllVisitTypeAsync(VisitTypeParametersDto visitTypeParameters, MetaData metaData);
        Task<List<VisitType>> GetAllVisitTypeForSelectionAsync();
        Task<VisitType> GetVisitTypeAsync(int id);
        VisitType GetVisitType(int id);
        Task AddVisitType(VisitType visitType);
        void DeleteVisitType(VisitType visitType);
        void UpdateVisitType(VisitType visitType);
        bool Save();
        Task<bool> SaveAsync();
    }
}
