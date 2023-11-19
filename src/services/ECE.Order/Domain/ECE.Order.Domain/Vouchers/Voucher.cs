using ECE.Core.DomainObjects;

namespace ECE.Order.Domain.Vouchers
{
    public class Voucher : Entity, IAggregateRoot
    {
        public string Code { get; private set; }
        public decimal? Percentage { get; private set; }
        public decimal? DiscountValue { get; private set; }
        public int Amount { get; private set; }
        public DiscountType DiscountType { get; private set; }
        public DateTime CreationDate { get; private set; }
        public DateTime? UsedDate { get; private set; }
        public DateTime ExpirationDate { get; private set; }
        public bool Active { get; private set; }
        public bool Used { get; private set; }
    }
}
