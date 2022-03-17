using BlazorControlCefa.Application.Dtos.Shared;

namespace BlazorControlCefa.Application.Dtos.Vehicle
{
    public class VehicleParametersDto : BasePaginationParameters
    {
        public string Filters { get; set; }
        public string SortOrder { get; set; }
    }
}
