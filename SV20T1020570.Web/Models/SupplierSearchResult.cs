using SV20T1020570.DomainModels;

namespace SV20T1020570.Web.Models
{
    public class SupplierSearchResult:BasePaginationResult
    {
        public List<Supplier> Data { get; set; }

    }
}
