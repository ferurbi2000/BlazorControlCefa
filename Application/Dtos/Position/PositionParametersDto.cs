using BlazorControlCefa.Application.Dtos.Shared;

namespace BlazorControlCefa.Application.Dtos.Position
{
    public class PositionParametersDto : BasePaginationParameters
    {
        public string Filters { get; set; }
        public string SortOrder { get; set; }
    }
}
