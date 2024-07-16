using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BookApi.Data;
using BookApi.Models;
namespace BookApi.Controllers
{
    public class BookController : Controller
    {
        private readonly Repository _repository;

        public BookController(Repository repository)
        {
            _repository = repository;
        }

        public ActionResult Index()
        {
            var books = _repository.GetBooks();
            return View(books);
        }
        [HttpGet]
        public ActionResult GetBookById(int id)
        {
            var book = _repository.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book); 
        }

        [HttpGet]
        public ActionResult<IEnumerable<BookModel>> GetBooks()
        {
            return Ok(_repository.GetBooks());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookModel book)
        {
            if (ModelState.IsValid)
            {
                _repository.AddBook(book);
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var book = _repository.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BookModel book)
        {
            if (ModelState.IsValid)
            {
                _repository.UpdateBook(book);
                return RedirectToAction("Index");
            }
            return View(book);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var book = _repository.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _repository.DeleteBook(id);
            return RedirectToAction("Index");
        }
    }
}
