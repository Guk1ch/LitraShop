using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Core.DataBase;

namespace Core.Functions
{
	public class BookFunction
	{
        public static ObservableCollection<Book> books { get; set; }
        public static ObservableCollection<Book> GetBook()
        {
            return new ObservableCollection<Book>(BdConnection.connection.Book.ToList());
        }
        public static Book GetBookInfo(int id)
        {
            Book selectedBook = BdConnection.connection.Book.Where(bookId => bookId.ID == id).FirstOrDefault();
            Book book = new Book()
            {
                ID = selectedBook.ID,
                Name = selectedBook.Name,
                Poster = null,
                Description = selectedBook.Description,
                Date_Issue = selectedBook.Date_Issue,
                CountList = selectedBook.CountList
            };
            return book;
        }
        public static ObservableCollection<Book> SearchBook(string name)
        {
            return books = new ObservableCollection<Book>(BdConnection.connection.Book.Where(a => a.Name.Contains(name)).ToList());
        }
    }
}
