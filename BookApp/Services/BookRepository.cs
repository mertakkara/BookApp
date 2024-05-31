using BookApp.Data;
using BookApp.Interface;
using BookApp.Model;

namespace BookApp.Services
{
    public class BookRepository : IBookRepository
    {
        private readonly DBContextClass _dbContext;
        public BookRepository(DBContextClass dbContext)
        {
            _dbContext = dbContext;
        }
        public void Add(Book book)
        {
            var result = _dbContext.Books.Add(book);
            _dbContext.SaveChanges();
        }

        public bool Delete(int id)
        {
            var result = _dbContext.Books.FirstOrDefault(x => x.BookID == id);
            if (result != null)
            {
                _dbContext.Books.Remove(result);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public IEnumerable<Book> GetAll()
        {
            return _dbContext.Books.ToList();
        }

        public Book GetById(int id)
        {
            return _dbContext.Books.FirstOrDefault(x => x.BookID == id);
        }

        public void Update(Book book)
        {
            var result = _dbContext.Books.Update(book);
            _dbContext.SaveChanges();
        }
    }
}
