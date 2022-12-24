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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Core.Functions;
using Core.DataBase;
using Prism.Commands;

namespace LitraShop.Pages
{
	/// <summary>
	/// Interaction logic for BookPage.xaml
	/// </summary>
	public partial class BookPage : Page
	{
		public Book bookToFill { get; set; }
		private void NavHomeView(object ID)
		{
			if (ID is string destinationurl)
				System.Diagnostics.Process.Start(bookToFill.BookLink);
		}
		public string ExternalURL { get => bookToFill.BookLink; }
		private readonly ICommand navHomeViewCommand;
		public ICommand NavHomeViewCommand
		{
			get { return navHomeViewCommand; }
		}
		public BookPage(Book book)
		{
			InitializeComponent();
			bookToFill = book;
			navHomeViewCommand = new DelegateCommand<object>(NavHomeView);
			TbDuration.Text = bookToFill.CountList.ToString();
			if (CollectionFunction.Viewed(AuthorisPage.user.ID, bookToFill.ID))
			{
				CbWatch.IsChecked = true;
			}
			this.DataContext = this;
		}
		private void TbBackMouseDown(object sender, MouseButtonEventArgs e)
		{
			NavigationService.GoBack();
		}

		private void TbBackMouseEnter(object sender, MouseEventArgs e)
		{
			TbBack.Foreground = new SolidColorBrush(Colors.White);
		}

		private void TbBackMouseLeave(object sender, MouseEventArgs e)
		{
			TbBack.Foreground = new SolidColorBrush(Colors.Black);
		}
		private void ImgPlusMouseDown(object sender, MouseButtonEventArgs e)
		{
			SelectionCollectionWindow selectionCollectionWindow = new SelectionCollectionWindow(bookToFill.ID);
			selectionCollectionWindow.ShowDialog();
		}

		private void CbWatchChecked(object sender, RoutedEventArgs e)
		{
			CollectionFunction.ReadedBook(AuthorisPage.user.ID, bookToFill.ID);
		}

		private void CbWatchUnchecked(object sender, RoutedEventArgs e)
		{
			CollectionFunction.UnviewedBook(AuthorisPage.user.ID, bookToFill.ID);
		}

	}
}
