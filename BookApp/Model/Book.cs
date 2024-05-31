namespace BookApp.Model
{
    public class Book
    {
        public int BookID { get; set; }
        public string BookName { get; set; } = string.Empty;
        public string BookDescription { get; set; } = string.Empty;
        public int BookPrice { get; set; }
        public int BookStock { get; set; }
    }
}
