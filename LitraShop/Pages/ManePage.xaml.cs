using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Collections.ObjectModel;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Core.Functions;
using Core.DataBase;

namespace LitraShop.Pages
{
	/// <summary>
	/// Interaction logic for ManePage.xaml
	/// </summary>
	public partial class ManePage : Page
	{
		public static User profil { get; set; }
		public static ObservableCollection<Book> bookList { get; set; }
		public ManePage(User user)
		{
			InitializeComponent();
			profil = user;
			bookList = BookFunction.GetBook();
			this.DataContext = this;
		}
		private void BtnProfilClick(object sender, RoutedEventArgs e)
		{
			NavigationService.Navigate(new ProfilPage(profil));
		}
		private void LvBooksSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var isSelected = LvBooks.SelectedItem as Book;
			if (isSelected != null)
			{
				NavigationService.Navigate(new BookPage(isSelected));
			}
		}
		private void TbSearchSelectionChanged(object sender, RoutedEventArgs e)
		{
			bookList = BookFunction.GetBook();
			if (TbSearch.Text != "")
			{
				bookList = new ObservableCollection<Book>(BdConnection.connection.Book.Where(a => a.Name.Contains(TbSearch.Text)).ToList());
			}

			if (bookList.Count == 0)
			{
				TbIsEmpty.Visibility = Visibility.Visible;
			}
			else
			{
				TbIsEmpty.Visibility = Visibility.Hidden;
			}
			LvBooks.ItemsSource = bookList;
		}
	}
}
