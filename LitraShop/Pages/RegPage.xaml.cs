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
using System.IO;
using Core.Functions;
using Core.DataBase;

namespace LitraShop.Pages
{
	/// <summary>
	/// Interaction logic for RegPage.xaml
	/// </summary>
	public partial class RegPage : Page
	{
		public byte[] UserPhoto;
		public RegPage()
		{
			InitializeComponent();
		}
        private void TextBlockMouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new AuthorisPage());
        }

        private void TextBlockMouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            tb_back.Foreground = new SolidColorBrush(Colors.White);
        }

        private void TbBackMouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            tb_back.Foreground = new SolidColorBrush(Colors.White);
        }

        private void BtnNewPhotoClick(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFile = new Microsoft.Win32.OpenFileDialog()
            {
                Filter = "*.jpg|*.jpg|*.png|*.png"
            };
            if (openFile.ShowDialog().GetValueOrDefault())
            {
                UserPhoto = File.ReadAllBytes(openFile.FileName);
                img_photo.Source = new BitmapImage(new Uri(openFile.FileName));
            }
        }

        private void BtnSaveClick(object sender, RoutedEventArgs e)
        {
            string nickName = tb_nick.Text.Trim();
            string login = tb_login.Text.Trim();
            string pass = tb_pass.Text.Trim();
            if (RegistrationFunction.UniqueLogin(login))
            {
                if (nickName != "" && login != "" && pass != "")
                {
                    RegistrationFunction.RegistrationUser(nickName, login, pass, UserPhoto);

                    System.Windows.MessageBox.Show("Аккаунт успешно создан!");
                    NavigationService.Navigate(new AuthorisPage());
                }
                else
                {
                    System.Windows.MessageBox.Show("Заполните все поля!");
                }
            }
            else
            {
                tb_login.Text = "";
                System.Windows.MessageBox.Show("Придумайте другой логин");
            }
        }
    }
}
