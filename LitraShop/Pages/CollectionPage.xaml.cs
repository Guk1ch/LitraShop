using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Core.DataBase;
using Core.Functions;

namespace LitraShop.Pages
{
	/// <summary>
	/// Interaction logic for CollectionPage.xaml
	/// </summary>
	public partial class CollectionPage : Page
	{
		public static ObservableCollection<Book_Collection> booksToFill { get; set; }
		public int IDCollection;
		public Collection updateCollection { get; set; }
		public CollectionPage(Collection collection)
		{
			InitializeComponent();
            updateCollection = collection;
            if (collection.Name == "Избранное" || collection.Name == "Прочитано")
            {
                ImgRedaction.Visibility = Visibility.Hidden;
                ImgDelete.Visibility = Visibility.Hidden;
            }
            booksToFill = CollectionFunction.GetBookInCollection(collection.ID);
            if (booksToFill.Count == 0)
            {
                TbIsEmpty.Visibility = Visibility.Visible;
            }
            else
            {
                TbIsEmpty.Visibility = Visibility.Hidden;
            }
            IDCollection = collection.ID;
            this.DataContext = this;
        }
        private void TbBackMouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new CollectionListPage());
        }

        private void TbBackMouseEnter(object sender, MouseEventArgs e)
        {
            TbBack.Foreground = new SolidColorBrush(Colors.White);
        }

        private void TbBackMouseLeave(object sender, MouseEventArgs e)
        {
            TbBack.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void ImgRedactionMouseDown(object sender, MouseButtonEventArgs e)
        {
            EditCollectionWindow editCollection = new EditCollectionWindow(updateCollection.ID);
            editCollection.ShowDialog();
        }

        private void LvBookSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Book selected = (LvBook.SelectedItem as Book_Collection).Book;
            NavigationService.Navigate(new BookPage(selected));
        }

        private void ImgDeleteMouseDown(object sender, MouseButtonEventArgs e)
        {
            DeleteWindow deleteWindow = new DeleteWindow();
            if (deleteWindow.ShowDialog() == true)
            {
                CollectionFunction.DeletedCollection(IDCollection);
                NavigationService.Navigate(new CollectionListPage());
            }
        }

        private void BtnDelClick(object sender, RoutedEventArgs e)
        {
            var senderButton = sender as Button;
            var book = senderButton.DataContext as Book_Collection;
            Collection delBookColl = BdConnection.connection.Collection.Where(a => a.ID == book.ID_Collection).FirstOrDefault();

            if (delBookColl.Name != "Прочитано")
            {
                CollectionFunction.DeletedFilmInCollection(IDCollection, Convert.ToInt32(book.ID_Book));
                UpdateFilm();
            }
            else
            {
                MessageBox.Show("Прочитано не доступно для редактирования");
            }
        }

        public void UpdateFilm()
        {
            if (updateCollection.Name == "Избранное" || updateCollection.Name == "Прочитано")
            {
                ImgRedaction.Visibility = Visibility.Hidden;
                ImgDelete.Visibility = Visibility.Hidden;
            }
            booksToFill = CollectionFunction.GetBookInCollection(updateCollection.ID);
            if (booksToFill.Count == 0)
            {
                TbIsEmpty.Visibility = Visibility.Visible;
            }
            else
            {
                TbIsEmpty.Visibility = Visibility.Hidden;
            }

            LvBook.ItemsSource = booksToFill;
        }
    }
}
