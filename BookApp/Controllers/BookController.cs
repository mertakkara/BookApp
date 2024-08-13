using BookApp.Data;
using BookApp.Exceptions;
using BookApp.Interface;
using BookApp.Model;
using BookApp.RabbitMQ;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookApp.Controllers
{
    [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IRabbitMQBook _rabbitMQProducer;
        private readonly ILogger<BookController> _logger;
        private readonly IBookRepository _bookRepository;
        private readonly IUnitOfWork _unitOfWork;
        public BookController(ILogger<BookController> logger, IBookRepository bookRepository, IRabbitMQBook rabbitMQProducer, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _bookRepository = bookRepository;
            _rabbitMQProducer = rabbitMQProducer;
            _unitOfWork = unitOfWork;
        }
        [HttpGet("books")]
        //[Authorize]
        public async Task<IActionResult> GetBooks(int lastItemId = 0, int pageSize = 10)
        {
            var items = await _unitOfWork.Books.GetBooks();
            var item =  items.Where(i => i.BookID > lastItemId)
            .OrderBy(i => i.BookID).Take(pageSize)
           .ToList();
            return Ok(item);
        }

        [HttpGet("book/{id}")]
        [Authorize]
        public async Task<IActionResult> GetProduct(int id)
        {
            ApiResponse<Book> response = new ApiResponse<Book>();

            try
            {
                var product = await _unitOfWork.Books.GetById(id);
                if (product == null)
                {
                    response.Success = false;
                    response.Message = "Book not found.";
                    return NotFound(response);  
                }

                response.Success = true;
                response.Message = "Book retrieved successfully.";
                response.Data = product;
                return Ok(response);         
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error occurred.";
                response.Errors.Add(ex.Message);
                return StatusCode(500, response); 
            }
        }

        [HttpPost("addbook")]
        //[Authorize]
        public async Task<IActionResult> AddProduct([FromBody] Book book)
        {
            ApiResponse<string> response = new ApiResponse<string>();

            try
            {
                if (book.BookStock < 0)
                {
                    throw new InvalidBookStockException(book.BookStock);
                }
                var result = await _unitOfWork.Books.Add(book);
                _unitOfWork.Complete();
                _rabbitMQProducer.SendBookMessage("added");

                response.Success = result;
                response.Message = "success";
                response.Data = "Book created successfully!!";
                return Ok(response);         
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Data = "Book creation failed!!";
                return StatusCode(500, response); 
            }
        }

        [HttpPut("updatebook")]
        [Authorize]
        public async Task<IActionResult> UpdateProduct([FromBody] Book book)
        {
            ApiResponse<string> response = new ApiResponse<string>();
            var result = await _unitOfWork.Books.Update(book);
            if (result)
            {
                response.Success = true;
                response.Message = "Book updated successfully.";
                return Ok(response);
            }
            response.Success = false;
            response.Message = "Book not found.";
            return NotFound(response);
        }

        [HttpDelete("deletebook/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            ApiResponse<string> response = new ApiResponse<string>();
            var product = await _unitOfWork.Books.Delete(id);
            if (product)
            {
                response.Success = true;
                response.Message = "Book deleted successfully.";
                return Ok(response);
            }
            response.Success = false;
            response.Message = "Book not found.";
            return NotFound(response);
        }
    }
}
