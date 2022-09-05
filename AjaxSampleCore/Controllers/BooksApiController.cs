using Microsoft.AspNetCore.Mvc;

namespace AjaxSampleCore.Controllers
{
    using Microsoft.EntityFrameworkCore;
    using Models;
    using ViewModels;

    [Route("api/[controller]")]
    [ApiController]
    public class BooksApiController : ControllerBase
    {
        readonly BookContext context;

        public BooksApiController(BookContext context) => this.context = context;

        // GET: api/BooksApi/5
        [HttpGet("{searchText}")]
        public async Task<ActionResult<IEnumerable<BookViewModel>>> GetBooks(string searchText)
        {
            var books = await context.Books
                                     .Include(book => book.Publisher)
                                     .Include(book => book.Authors)
                                     .Where(book => book.Title.Contains(searchText))
                                     .OrderBy(book => book.Code)
                                     .ToArrayAsync();
            var bookViews = books.Select(book => book.ToViewModel()).ToArray();
            return bookViews;
        }
    }
}
