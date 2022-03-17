using BlazorControlCefa.Application.Dtos.Shared;

namespace BlazorControlCefa.Application.Dtos.Reason
{
    public class ReasonParametersDto : BasePaginationParameters
    {
        public string Filters { get; set; }
        public string SortOrder { get; set; }
    }
}
