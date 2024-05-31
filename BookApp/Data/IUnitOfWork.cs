using BookApp.Interface;

namespace BookApp.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IBookRepository Books { get; }
        int Complete();
    }
}
