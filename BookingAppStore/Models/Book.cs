using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookingAppStore.Models
{
    //модель книги
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
    }
}