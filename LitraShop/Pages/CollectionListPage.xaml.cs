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
using System.Collections.ObjectModel;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Core.DataBase;
using Core.Functions;

namespace LitraShop.Pages
{
	/// <summary>
	/// Interaction logic for CollectionListPage.xaml
	/// </summary>
	public partial class CollectionListPage : Page
	{
		public static ObservableCollection<Collection> fullColl { get; set; }
		public static User profil { get; set; }
		public CollectionListPage()
		{
			InitializeComponent();
			profil = AuthorisPage.user;
			fullColl = CollectionFunction.GetCollection(AuthorisPage.user.ID);
			this.DataContext = this;
		}
        private void ImgPlusMouseDown(object sender, MouseButtonEventArgs e)
        {
            NewCollectionWindow newCollection = new NewCollectionWindow(AuthorisPage.user.ID);
            newCollection.ShowDialog();
            UpdateCollection();
        }

        private void LvCollSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var collId = (sender as ListView).SelectedItem as Collection;
            NavigationService.Navigate(new CollectionPage(collId));
        }

        public void UpdateCollection()
        {
            LvColl.ItemsSource = CollectionFunction.GetCollection(AuthorisPage.user.ID);
        }
        private void TextBlockMouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new ManePage(profil));
        }

        private void TextBlockMouseEnter(object sender, MouseEventArgs e)
        {
            TbBack.Foreground = new SolidColorBrush(Colors.White);
        }

        private void TbBackMouseLeave(object sender, MouseEventArgs e)
        {
            TbBack.Foreground = new SolidColorBrush(Colors.Black);
        }
    }
}
