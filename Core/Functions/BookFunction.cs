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
        public static ObservableCollection<Comics> books { get; set; }
        public static ObservableCollection<Comics> GetBook()
        {
            return new ObservableCollection<Comics>(BdConnection.connection.Comics.ToList());
        }
        public static Comics GetBookInfo(int id)
        {
            Comics selectedBook = BdConnection.connection.Comics.Where(bookId => bookId.ID == id).FirstOrDefault();
            Comics book = new Comics()
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
        public static ObservableCollection<Comics> SearchBook(string name)
        {
            return books = new ObservableCollection<Comics>(BdConnection.connection.Comics.Where(a => a.Name.Contains(name)).ToList());
        }
    }
}
