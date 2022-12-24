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
using Core.DataBase;
using Core.Functions;
using System.IO;

namespace LitraShop.Pages
{
	/// <summary>
	/// Interaction logic for EditProfWindow.xaml
	/// </summary>
	public partial class EditProfWindow : Window
	{
		public static User profil { get; set; }
		public byte[] UserPhoto;
		public EditProfWindow(User user)
		{
			InitializeComponent();
			profil = user;
			TbName.Text = profil.Nickname;
			UserPhoto = profil.Photo;
			this.DataContext = this;

		}
		private void BtnSaveClick(object sender, RoutedEventArgs e)
		{
			string nickName = TbName.Text.Trim();
			if (nickName != "")
			{
				Authorization.EditUser(profil.ID, UserPhoto, nickName);
				System.Windows.MessageBox.Show("Профиль успешно изменен!");
				this.DialogResult = true;
			}
			else
			{
				System.Windows.MessageBox.Show("Заполните все поля!");
			}
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
				ImgPhoto.Source = new BitmapImage(new Uri(openFile.FileName));
			}
		}

	}
}
