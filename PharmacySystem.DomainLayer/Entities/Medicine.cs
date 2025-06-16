
namespace PharmacySystem.DomainLayer.Entities
{
    public class Medicine : BaseEntity
    {
        public string Name {get; set;}
        public string ArabicName {get; set;}
        public string Description {get; set; }
        public decimal Price {get; set;}
        public string? MedicineUrl { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new HashSet<OrderDetail>();
        public virtual ICollection<WareHouseMedicien> WareHouseMedicines { get; set; } = new HashSet<WareHouseMedicien>();
    }
}

