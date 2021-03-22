namespace vcar.Extensions
{
    public interface IQueryObj
    {
        string sortBy { get; set; }
        bool sortAsc { get; set; }
        int Page { get; set; }
        int PageSize { get; set; }
    }
}