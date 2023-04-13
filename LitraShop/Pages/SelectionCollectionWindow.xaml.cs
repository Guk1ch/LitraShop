using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Core.Functions;
using Core.DataBase;
using System.Collections.ObjectModel;

namespace LitraShop.Pages
{
	/// <summary>
	/// Interaction logic for SelectionCollectionWindow.xaml
	/// </summary>
	public partial class SelectionCollectionWindow : Window
	{
		public int BookId;
		public SelectionCollectionWindow(int BookID)
		{
			InitializeComponent();
			BookId = BookID;
			ObservableCollection<Collection> collection = CollectionFunction.GetCollection(AuthorisPage.user.ID);
			ObservableCollection<Collection> trueCollection = new ObservableCollection<Collection>();
			foreach (var i in collection)
			{
				if (i.Name != "Прочитано")
				{
					trueCollection.Add(i);
				}
			}
			CbCollection.ItemsSource = trueCollection;
			CbCollection.DisplayMemberPath = "Name";
		}
		private void BtnAddClick(object sender, RoutedEventArgs e)
		{
			if (CbCollection.SelectedIndex >= 0)
			{
				var BookColl = new Comics_Collection();
				var select = CbCollection.SelectedItem as Collection;
				BookColl.ID_Collection = select.ID;
				BookColl.ID_Comics = BookId;
				var isColl = BdConnection.connection.Comics_Collection.Where(a => a.ID_Collection == select.ID && a.ID_Comics == BookId).Count();
				if (isColl == 0)
				{
					BdConnection.connection.Comics_Collection.Add(BookColl);
					BdConnection.connection.SaveChanges();
					this.DialogResult = true;
				}
				else
				{
					MessageBox.Show("Книга уже добавлена в коллекцию", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				}
			}
		}
		private void ImgPlusMouseDown(object sender, MouseButtonEventArgs e)
		{
			NewCollectionWindow newCollection = new NewCollectionWindow(AuthorisPage.user.ID);
			newCollection.ShowDialog();
			UpdateCollection();
		}
		public void UpdateCollection()
		{
			ObservableCollection<Collection> collection = CollectionFunction.GetCollection(AuthorisPage.user.ID);
			ObservableCollection<Collection> trueCollection = new ObservableCollection<Collection>();
			foreach (var i in collection)
			{
				if (i.Name != "Прочитано")
				{
					trueCollection.Add(i);
				}
			}
			CbCollection.ItemsSource = trueCollection;
			CbCollection.DisplayMemberPath = "Name";
		}
	}

}
