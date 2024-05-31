using BookApp.Data;
using BookApp.Interface;
using BookApp.Model;
using BookApp.RabbitMQ;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookApp.Controllers
{
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

        [HttpGet("booklist")]
        [Authorize]
        public IActionResult GetProducts()
        {
            var books = _unitOfWork.Books.GetAll();
            return Ok(books);
        }

        [HttpGet("book/{id}")]
        [Authorize]
        public IActionResult GetProduct(int id)
        {
            var product = _unitOfWork.Books.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost("addbook")]
        [Authorize]
        public IActionResult AddProduct([FromBody] Book book)
        {
            _unitOfWork.Books.Add(book);
            _unitOfWork.Complete();
            _rabbitMQProducer.SendBookMessage("added");
            return Ok("added");
        }

        [HttpPut("updatebook")]
        [Authorize]
        public IActionResult UpdateProduct([FromBody] Book book)
        {
            _unitOfWork.Books.Update(book);
            _unitOfWork.Complete();
            return Ok("updated");
        }

        [HttpDelete("deletebook/{id}")]
        [Authorize]
        public IActionResult DeleteProduct(int id)
        {
            var result = _unitOfWork.Books.Delete(id);
            if (result)
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
