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
using Core.DataBase;

namespace LitraShop.Pages
{
	/// <summary>
	/// Interaction logic for ProfilPage.xaml
	/// </summary>
	public partial class ProfilPage : Page
	{
		public static User profil { get; set; }
		public ProfilPage(User user)
		{
			InitializeComponent();
			profil = user;
			this.DataContext = this;
		}
		private void BtnCollClick(object sender, RoutedEventArgs e)
		{
			NavigationService.Navigate(new CollectionListPage());
		}
		private void BtnEditClick(object sender, RoutedEventArgs e)
		{
			EditProfWindow editProf = new EditProfWindow(profil);
			editProf.ShowDialog();
		}
		private void BtnExitClick(object sender, RoutedEventArgs e)
		{
			NavigationService.Navigate(new AuthorisPage());
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
