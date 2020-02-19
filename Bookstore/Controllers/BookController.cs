using Bookstore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;

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
            string filepath = HttpContext.Server.MapPath("~/XML/Books.xml");
            string viewPath = HttpContext.Server.MapPath("~/Views/Book/Index.cshtml");

            var doc = XDocument.Load(filepath);

            var newElement = new XElement("Book",
                new XElement("id", 5),
                new XElement("title", "Test"),
                new XElement("author","test author"),
                new XElement("price",666),
                new XElement("cover", "https://upload.wikimedia.org/wikipedia/commons/thumb/6/64/Question_book-4.svg/1280px-Question_book-4.svg.png"),
                new XElement("description", "Lorem Ipsum"));

            doc.Element("Books").Add(newElement);
            doc.Save(filepath);

            return RedirectToAction("Index");
        }

        public ActionResult Update(int index)
        {

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int index)
        {

            return RedirectToAction("Index");
        }
    }
}