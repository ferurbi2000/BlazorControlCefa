using BlazorControlCefa.Application.Dtos.Shared;

namespace BlazorControlCefa.Application.Dtos.Visit
{
    public class VisitParametersDto : BasePaginationParameters
    {
        public string Filters { get; set; }
        public string SortOrder { get; set; }
    }
}
