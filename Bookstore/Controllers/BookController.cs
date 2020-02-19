using Bookstore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace Bookstore.Controllers
{
    public class BookController : Controller
    {
        private List<Book> getData()
        {
            List<Book> bookList = new List<Book>();

            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath("~/XML/Books.xml"));

            foreach (XmlNode node in doc.SelectNodes("/Books/Book"))
            {
                bookList.Add(new Book
                {
                    id = int.Parse(node["id"].InnerText),
                    title = node["title"].InnerText,
                    author = node["author"].InnerText,
                    price = int.Parse(node["price"].InnerText),
                    cover = node["cover"].InnerText,
                    description = node["description"].InnerText
                });
            }

            return bookList;
        }

        public ActionResult Index()
        {
            return View(getData());
        }

        public ActionResult CreateNew()
        {
            XmlDocument doc = new XmlDocument();

            

            return View();
        }

        public ActionResult Update(int index)
        {

            return View();
        }

        public ActionResult Delete(int index)
        {

            return View();
        }
    }
}