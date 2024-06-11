using BookApp.Model;

namespace BookApp.Interface
{
    public interface IBookRepository
    {
        Task<Book> GetById(int id);
        Task<bool> Add(Book book);
        Task<bool> Update(Book book);
        Task<bool> Delete(int id);
    }
}
