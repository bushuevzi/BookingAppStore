using BookingAppStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookingAppStore.Controllers
{
    public class HomeController : Controller
    {
        //чтобы работать с базой данных (вытаскивать от туда данные нужно создать переменную контекста базы)
        BookContext db = new BookContext();

        public ActionResult Index()
        {
            //берем коллекцию всех книг
            var books = db.Books;
            //передаем ее в представление
            ViewBag.Books = books;
            return View();
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
    }
}