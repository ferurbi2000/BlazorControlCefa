namespace BlazorControlCefa.Repositories.Position
{
    using BlazorControlCefa.Application.Dtos.Position;
    using BlazorControlCefa.Application.Wrappers;
    using BlazorControlCefa.Data.Entities;
    using BlazorControlCefa.Features;

    public interface IPositionRepository
    {
        Task<PagingResponse<Position>> GetAllPositionAsync(PositionParametersDto positionParameters, MetaData metaData);
        Task<List<Position>> GetAllPositionForSelectionAsync();
        Task<Position> GetPositionAsync(int id);
        Position GetPosition(int id);
        Task AddPosition(Position position);
        void DeletePosition(Position position);
        void UpdatePosition(Position position);
        bool Save();
        Task<bool> SaveAsync();
    }
}
