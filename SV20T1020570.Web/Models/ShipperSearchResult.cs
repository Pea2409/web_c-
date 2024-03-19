using SV20T1020570.DomainModels;

namespace SV20T1020570.Web.Models
{
    public class ShipperSearchResult : BasePaginationResult
    {
        public List<Shipper> Data { get; set; }
    }
}
