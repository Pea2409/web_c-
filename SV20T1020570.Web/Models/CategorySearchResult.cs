using SV20T1020570.DomainModels;

namespace SV20T1020570.Web.Models
{
    public class CategorySearchResult : BasePaginationResult
    {
        public List<Category> Data { get; set; }
    }
}
