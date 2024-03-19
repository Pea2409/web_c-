using SV20T1020570.DomainModels;

namespace SV20T1020570.Web.Models
{
    public class ProductSearchResult : BasePaginationResult
    {
        public List<Product> Data { get; set; }
    }

}
