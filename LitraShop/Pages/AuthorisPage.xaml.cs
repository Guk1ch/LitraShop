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

namespace LitraShop.Pages
{
	/// <summary>
	/// Interaction logic for AuthorisPage.xaml
	/// </summary>
	public partial class AuthorisPage : Page
	{
		public static User user;
		public static int PassCount = 0;
		public AuthorisPage()
		{
			InitializeComponent();
			TbLogin.Text = Properties.Settings.Default.Login.ToString();
		}
		private void BtnRegistClick(object sender, RoutedEventArgs e)
		{
			NavigationService.Navigate(new RegPage());
		}
        private void BtnAuthorizClick(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.Password < DateTime.Now)
            {
                string login = TbLogin.Text.Trim();
                string password = TbPassword.Password.Trim();
                user = Authorization.AuthorizationUser(login, password);
                if (user != null)
                {
                    if (CbSaveLogin.IsChecked.GetValueOrDefault())
                    {
                        Properties.Settings.Default.Login = TbLogin.Text.Trim();
                        Properties.Settings.Default.Save();
                    }
                    else
                    {
                        Properties.Settings.Default.Login = null;
                        Properties.Settings.Default.Save();
                    }

                    NavigationService.Navigate(new ManePage(user));
                }
                else
                {
                    PassCount++;
                    MessageBox.Show("Неверный логин или пароль", "error", MessageBoxButton.OK, MessageBoxImage.Error);
                    if (PassCount == 3)
                    {
                        PassCount = 0;
                        Properties.Settings.Default.Password = DateTime.Now.AddMinutes(1);
                        Properties.Settings.Default.Save();
                    }
                }
            }
            else
            {
                MessageBox.Show("Вы ввели неверный пароль 3 раза, возможность входа заблокирована на: \n" + (Properties.Settings.Default.Password - DateTime.Now).Seconds + " сек.");
            }
        }

        private void TextBlockMouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new RegPage());
        }

        private void TextBlockMouseEnter(object sender, MouseEventArgs e)
        {
            TbRegist.Foreground = new SolidColorBrush(Colors.Red);
        }

        private void TextBlockMouseLeave(object sender, MouseEventArgs e)
        {
            TbRegist.Foreground = new SolidColorBrush(Colors.White);
        }
    }
}
