using ECE.Core.Data;

namespace ECE.Order.Domain.Vouchers
{
    public interface IVoucherRepository : IRepository<Voucher>
    {
        Task<Voucher> GetVoucherByCode(string code);
    }
}
