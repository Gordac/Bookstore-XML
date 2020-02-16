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

        public ActionResult Index() //https://www.aspsnippets.com/Articles/Read-and-display-XML-data-in-View-in-ASPNet-MVC-Razor.aspx
        {
            List<Book> bookList = new List<Book>();

            XmlDocument doc = new XmlDocument();
            //doc.Load(@"../XML/Books.xml"); 
            //doc.Load(@"C:\Users\John\source\repos\Bookstore\Bookstore\XML\Books.xml");
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

            Console.WriteLine(bookList.Count);

            return View(bookList);
        }
    }
}