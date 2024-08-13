namespace BookApp.Exceptions
{
    public class InvalidBookStockException : Exception
    {
        public int BookStock { get; private set; }

        public InvalidBookStockException() { }

        public InvalidBookStockException(int bookStock)
            : base($"Book Stock cannot be negative. You tried to assign: {bookStock}")
        {
            BookStock = bookStock;
        }
    }
}
