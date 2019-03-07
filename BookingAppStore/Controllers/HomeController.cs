using BookingAppStore.Models;
using BookingAppStore.Util;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BookingAppStore.Controllers
{
    public class HomeController : Controller
    {
        //чтобы работать с базой данных (вытаскивать от туда данные нужно создать переменную контекста базы)
        BookContext db = new BookContext();

        //public ActionResult Index()
        //{
        //    //берем коллекцию всех книг
        //    var books = db.Books;
        //    //передаем ее в представление
        //    ViewBag.Books = books;
        //    HttpContext.Response.Cookies["token"].Value = "user-1234567890";
        //    return View();
        //}

        public ActionResult Index()
        {
            IEnumerable<Book> books = db.Books.ToList();
            ViewBag.Books = books;

            SelectList authors = new SelectList(db.Books, "Author", "Name");
            ViewBag.Authors = authors;
            return View();
        }

        [HttpPost]
        public string GetForm(string author)
        {
            return author;
        }

        //асинхронный метод так как используем подключение к Базе данных и это занимает время
        public async Task<ActionResult> BookList()
        {
            IEnumerable<Book> books = await db.Books.ToListAsync();
            ViewBag.Books = books;
            return View("Index");
        }

        public string GetToken()
        {
            string id = HttpContext.Request.Cookies["token"].Value;
            return id.ToString();
        }

        //public ActionResult GetBook()
        //{
        //    return View();
        //}

        public ActionResult GetBook(int id)
        {
            Book b = db.Books.Find(id);
            if (b == null)
                return HttpNotFound();
            return View(b);
        }


        public ActionResult CreateBook()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateBook(Book book)
        {
            db.Books.Add(book);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult EditBook(int? id)
        {
            if(id == null)
            {
                return HttpNotFound();
            }
            Book book = db.Books.Find(id);
            if (book != null)
            {
                return View(book);
            }
            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult EditBook(Book book)
        {
            db.Entry(book).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        //[HttpPost]
        //public string GetBook(string title, string author)
        //{
        //    return $"{title} {author}";
        //}

        public string GetContext()
        {
            string browser = HttpContext.Request.Browser.Browser;
            string userAgent = HttpContext.Request.UserAgent;
            string url = HttpContext.Request.RawUrl;
            string ip = HttpContext.Request.UserHostAddress;
            string referrer = HttpContext.Request.UrlReferrer == null ? "" : HttpContext.Request.UrlReferrer.AbsoluteUri;
            return $"<p>Browser: {browser}</p><p>User-Agent: {userAgent}</p><p>Url запроса: {url}</p>" +
                $"<p>Реферер: {referrer}</p><p>IP-адрес: {ip}</p>";
        }

        public FilePathResult GetFile()
        {
            //путь к файлу
            string filePath = Server.MapPath("~/Files/test.pdf");
            //тип файла -- content-type
            string fileType = "application/octet-stream";
            //Имя файла (необязательно)
            string fileName = "test.pdf";

            return File(filePath, fileType, fileName);
        }


        public FileResult GetBytes()
        {
            string filePath = Server.MapPath("~/Files/test.pdf");
            byte[] mas = System.IO.File.ReadAllBytes(filePath);
            string fileType = "application/octet-stream";
            string fileName = "test.pdf";
            return File(mas, fileType, fileName);
        }

        public ActionResult GetHtml()
        {
            return new HtmlResult("<h2>Привет мир</h2>");
        }

        public ActionResult GetImage()
        {
            string path = "../Content/Images/youtube.png";
            return new ImageResult(path);

        }

        public ActionResult Buy(int id)
        {
            ViewBag.BookId = id;
            return View();
        }


        [HttpPost]
        public string Buy(Purchase purchase)
        {
            purchase.Date = DateTime.Now;
            db.Purchases.Add(purchase);
            db.SaveChanges();
            return $"Спасибо, {purchase.Person} за покупку!";
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Book book)
        {
            db.Books.Add(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}