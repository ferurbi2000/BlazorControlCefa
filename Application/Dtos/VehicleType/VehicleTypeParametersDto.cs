using BlazorControlCefa.Application.Dtos.Shared;

namespace BlazorControlCefa.Application.Dtos.VehicleType
{
    public class VehicleTypeParametersDto : BasePaginationParameters
    {
        public string Filters { get; set; }
        public string SortOrder { get; set; }
    }
}
