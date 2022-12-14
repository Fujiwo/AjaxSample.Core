// 【変更】 Books のコントローラー

//#define INITIALIZING

using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AjaxSampleCore.Controllers
{
    using AjaxSampleCore.Models;
    using Microsoft.EntityFrameworkCore;
    using ViewModels;

    public class BooksController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        readonly BookContext context;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public BooksController(BookContext context) => this.context = context;

        public async Task<IActionResult> Index()
        {
#if INITIALIZING
            await DatabaseInitializer.Run(context);
#endif // INITIALIZING

            var books = await context.Books
                         .Include(book => book.Publisher)
                         .Include(book => book.Authors)
                         .OrderBy(book => book.Code)
                         .Take(3)
                         .ToArrayAsync();
            var bookViewModels = books.Select(book => book.ToViewModel());

            return View(bookViewModels);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Search(string searchText)
        {
            var books = await context.Books
                                     .Include(book => book.Publisher)
                                     .Include(book => book.Authors)
                                     .Where(book => book.Title.Contains(searchText))
                                     .OrderBy(book => book.Code)
                                     .ToArrayAsync();
            var bookViewModels = books.Select(book => book.ToViewModel());
            return PartialView("Part", bookViewModels);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}