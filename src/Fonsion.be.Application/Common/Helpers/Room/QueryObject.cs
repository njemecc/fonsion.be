namespace Fonsion.be.Application.Common.Helpers;

public class QueryObject
{
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public int Page { get; set; } = 0;
    public int PageSize { get; set; } = 5;
}
