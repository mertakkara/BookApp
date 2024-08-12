using BookApp.Model;
using Microsoft.AspNetCore.Mvc;

namespace BookApp.Interface
{
    public interface IBookRepository
    {
        Task<Book> GetById(int id);
        Task<List<Book>> GetBooks();
        Task<bool> Add(Book book);
        Task<bool> Update(Book book);
        Task<bool> Delete(int id);
    }
}
