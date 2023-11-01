namespace ECE.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
