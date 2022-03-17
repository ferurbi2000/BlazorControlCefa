namespace BlazorControlCefa.Application.Dtos.Shared
{
    public abstract class BasePaginationParameters
    {
        internal virtual int MaxPageSize { get; } = 50;
        internal virtual int DefaultPageSize { get; set; } = 10;
        internal virtual int PageNumber { get; set; } = 1;
        internal virtual int PageSize
        {
            get { return DefaultPageSize; }
            set { DefaultPageSize = value > MaxPageSize ? MaxPageSize : value; }
        }
    }
}
