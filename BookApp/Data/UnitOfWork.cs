using BookApp.Interface;
using BookApp.Services;
using System;

namespace BookApp.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DBContextClass _context;

        public UnitOfWork(DBContextClass context)
        {
            _context = context;
            Books = new BookRepository(_context);
        }

        public IBookRepository Books { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
