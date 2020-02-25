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
        private List<Book> GetData()
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

        private int NewID()
        {
            List<Book> list = GetData();
            int id = 1;

            foreach(var obj in list)
            {
                if(obj.id >= id)
                {
                    id = obj.id;
                }
            }

            return id + 1;
        }

        public ActionResult Index()
        {
            return View(GetData());
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateNew(string title, string author, int price, string cover, string description)
        {
            string filepath = HttpContext.Server.MapPath("~/XML/Books.xml");

            var doc = XDocument.Load(filepath);

            var newElement = new XElement("Book",
                new XElement("id", NewID()),
                new XElement("title", title),
                new XElement("author",author),
                new XElement("price",price),
                new XElement("cover", cover),
                new XElement("description", description));

            doc.Element("Books").Add(newElement);
            doc.Save(filepath);

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            List<Book> list = GetData();
            List<Book> targetBook = new List<Book>();

            foreach (Book item in list)
            {
                if(item.id == id)
                {
                    targetBook.Add(new Book
                    {
                        id = id,
                        title = item.title,
                        author = item.author,
                        price = item.price,
                        cover = item.cover,
                        description = item.description
                    });
                    break;
                }
            }

            return View("Update", targetBook);
        }

        [HttpPost]
        public ActionResult Update(int id, string title, string author, int price, string cover, string description)
        {
            string filepath = HttpContext.Server.MapPath("~/XML/Books.xml");

            XDocument doc = XDocument.Load(filepath);

            var newBook = doc.Descendants("Book").Single(n => n.Element("id").Value == id.ToString());

            newBook.SetElementValue("title", title); 
            newBook.SetElementValue("author", author);
            newBook.SetElementValue("price", price);
            newBook.SetElementValue("cover", cover);
            newBook.SetElementValue("description", description);

            doc.Save(filepath);


            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {

            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath("~/XML/Books.xml"));

            foreach (XmlNode node in doc.SelectNodes("Books/Book"))
            {
                if (node.SelectSingleNode("id").InnerText == id.ToString())
                {
                    node.ParentNode.RemoveChild(node);
                    break;
                }
            }
            doc.Save(Server.MapPath("~/XML/Books.xml"));

            return RedirectToAction("Index");
        }
    }
}