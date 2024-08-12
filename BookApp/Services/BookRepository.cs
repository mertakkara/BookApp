using BookApp.Data;
using BookApp.Interface;
using BookApp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookApp.Services
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public BookRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> Add(Book book)
        {
            var result = await _dbContext.Books.AddAsync(book);
            _dbContext.SaveChanges();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var result = await _dbContext.Books.FirstOrDefaultAsync(x => x.BookID == id);
            if (result != null)
            {
                result.DeletedAt = DateTime.Now;
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<List<Book>> GetBooks()
        {
            var result = await _dbContext.Books.ToListAsync();
	        return result;
        }

        public async Task<Book> GetById(int id)
        {
            var result = await _dbContext.Books.FirstOrDefaultAsync(x => x.BookID == id);
            if(result != null)
                return result;
            return null;
        }

        public async Task<bool> Update(Book book)
        {
            var user = await _dbContext.Books.FirstOrDefaultAsync(x=>x.BookID ==  book.BookID);
            if (user == null)
                return false;

            user.BookPrice = book.BookPrice;
            user.BookDescription = book.BookDescription;
            user.BookName = book.BookName;
            user.BookStock = book.BookStock;
            var result = _dbContext.Books.Update(user);
            await _dbContext.SaveChangesAsync();
            return true;    
        }
    }
}
