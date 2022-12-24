using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Core.DataBase;

namespace Core.Functions
{
	public class CollectionFunction
	{
		public static ObservableCollection<Collection> collections { get; set; }
		public static ObservableCollection<Book_Collection> books { get; set; }
		public static Collection collectionForDelete { get; set; }
		public static Book_Collection Book_CollectionForDelete { get; set; }
		public static ObservableCollection<Collection> GetCollection(int idUser)
		{
			return collections = new ObservableCollection<Collection>((BdConnection.connection.Collection.Where(userCollectiom => userCollectiom.ID_User == idUser && userCollectiom.IsDelete != true)).ToList());
		}
		public static ObservableCollection<Book_Collection> GetBookInCollection(int idColl)
		{
			return books = new ObservableCollection<Book_Collection>((BdConnection.connection.Book_Collection.Where(a => a.ID_Collection == idColl)).ToList());
		}
		public static bool NewCollection(string nameCollection, int userID)
		{
			Collection newCollection = new Collection();
			if (UniqueCollection(userID, nameCollection))
			{
				newCollection.ID_User = userID;
				newCollection.Name = nameCollection;
				BdConnection.connection.Collection.Add(newCollection);
				BdConnection.connection.SaveChanges();
				return true;
			}
			else
			{
				return false;
			}
		}
		public static void EditCollectionName(int idColl, string newName)
		{
			Collection editcollection = BdConnection.connection.Collection.Where(userCollectiom => userCollectiom.ID == idColl).FirstOrDefault();
			editcollection.Name = newName;
			BdConnection.connection.SaveChanges();
		}
		public static bool UniqueCollection(int idUser, string name)
		{
			bool uniq = true;
			var unique = GetCollection(idUser);
			foreach (var i in unique)
			{
				if (i.Name == name)
				{
					uniq = false;
				}
			}
			return uniq;
		}
		public static void ReadedBook(int idUser, int idBook)
		{
			var BookColl = new Book_Collection();
			var allColl = CollectionFunction.GetCollection(idUser);
			int viewedCollection = 0;
			foreach (var i in allColl)
			{
				if (i.Name == "Прочитано")
				{
					viewedCollection = i.ID;
				}
			}
			BookColl.ID_Collection = viewedCollection;
			BookColl.ID_Book = idBook;
			Book_Collection UnigueBookCollection = BdConnection.connection.Book_Collection.Where(a => a.ID_Collection == BookColl.ID_Collection && a.ID_Book == BookColl.ID_Book).FirstOrDefault();
			if (UnigueBookCollection == null)
			{
				BdConnection.connection.Book_Collection.Add(BookColl);
				BdConnection.connection.SaveChanges();
			}
		}
		public static void UnviewedBook(int idUser, int idBook)
		{
			var BookColl = new Book_Collection();
			var allColl = CollectionFunction.GetCollection(idUser);
			int viewedCollection = 0;
			foreach (var i in allColl)
			{
				if (i.Name == "Прочитано")
				{
					viewedCollection = i.ID;
				}
			}
			BookColl.ID_Collection = viewedCollection;
			BookColl.ID_Book = idBook;
			Book_Collection DeletedFilmCollection = BdConnection.connection.Book_Collection.Where(a => a.ID_Collection == BookColl.ID_Collection && a.ID_Book == BookColl.ID_Book).FirstOrDefault();
			BdConnection.connection.Book_Collection.Remove(DeletedFilmCollection);
			BdConnection.connection.SaveChanges();
		}
		public static bool Viewed(int idUser, int idBook)
		{
			var BookColl = new Book_Collection();
			var allColl = CollectionFunction.GetCollection(idUser);
			int viewedCollection = 0;
			foreach (var i in allColl)
			{
				if (i.Name == "Прочитано")
				{
					viewedCollection = i.ID;
				}
			}
			BookColl.ID_Collection = viewedCollection;
			BookColl.ID_Book = idBook;
			Book_Collection DeletedFilmCollection = BdConnection.connection.Book_Collection.Where(a => a.ID_Collection == BookColl.ID_Collection && a.ID_Book == BookColl.ID_Book).FirstOrDefault();
			if (DeletedFilmCollection != null)
			{
				return true;
			}
			else
			{
				return false;
			}

		}
		public static void DeletedCollection(int IDCollection)
		{
			collectionForDelete = BdConnection.connection.Collection.Where(userCollectiom => userCollectiom.ID == IDCollection).FirstOrDefault();
			collectionForDelete.IsDelete = true;
			BdConnection.connection.SaveChanges();
		}
		public static void DeletedFilmInCollection(int IDColl, int IDBook)
		{
			Book_CollectionForDelete = BdConnection.connection.Book_Collection.Where(a => a.ID_Collection == IDColl && a.ID_Book == IDBook).FirstOrDefault();
			BdConnection.connection.Book_Collection.Remove(Book_CollectionForDelete);
			BdConnection.connection.SaveChanges();
		}
	}
}
