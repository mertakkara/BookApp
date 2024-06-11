namespace BookApp.Model
{
    public class ApiResponse<T>
    {
        public bool Status { get; set; }
        public bool Success { get; set; }

        public string Message { get; set; }

        public T Data { get; set; }

        public List<string> Errors { get; set; }

        public ApiResponse()
        {
            Status = true;
        }
    }
}
