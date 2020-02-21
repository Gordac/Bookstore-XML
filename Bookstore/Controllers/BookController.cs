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

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateNew(string title, string author, int price, string cover, string description)
        {
            string filepath = HttpContext.Server.MapPath("~/XML/Books.xml");
            //string viewPath = HttpContext.Server.MapPath("~/Views/Book/Index.cshtml");

            var doc = XDocument.Load(filepath);

            var newElement = new XElement("Book",
                new XElement("id", 5),
                new XElement("title", title),
                new XElement("author",author),
                new XElement("price",price),
                new XElement("cover", cover),
                new XElement("description", description));

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