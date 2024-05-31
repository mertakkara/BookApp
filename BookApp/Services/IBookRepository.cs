using BookApp.Model;

namespace BookApp.Interface
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetAll();
        Book GetById(int id);
        void Add(Book book);
        void Update(Book book);
        bool Delete(int id);
    }
}
