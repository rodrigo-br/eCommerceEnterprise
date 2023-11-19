using ECE.Core.Data;
using ECE.Order.Domain.Vouchers;
using Microsoft.EntityFrameworkCore;

namespace ECE.Order.Infra.Data.Repository
{
    public class VoucherRepository : IVoucherRepository
    {
        private readonly OrderContext _context;

        public VoucherRepository(OrderContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<Voucher> GetVoucherByCode(string code)
        {
            return await _context.Vouchers.FirstOrDefaultAsync(v => v.Code == code);
        }
    }
}
