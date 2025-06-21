
namespace PharmacySystem.ApplicationLayer.DTOs.RepresentatitvePharmaciesOrdersAndOrderDetails
{
    public class GetOrdersPharmaciesCountDto
    {
        public string PharmaciesName { get; set; }
        public string UserName { get; set; }
        public string PharmaciesAddress { get; set; }
        public string PharmaciesGovernate { get; set; }
        public int OrdersCount { get; set; }
    }
}
