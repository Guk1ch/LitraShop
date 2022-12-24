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

namespace LitraShop.Pages
{
	/// <summary>
	/// Interaction logic for NewCollectionWindow.xaml
	/// </summary>
	public partial class NewCollectionWindow : Window
	{
		public int ID;
		public NewCollectionWindow(int UserID)
		{
			InitializeComponent();
			ID = UserID;
		}
        private void BtnSaveClick(object sender, RoutedEventArgs e)
        {
            string collectionName = TbName.Text.Trim();
            if (collectionName != "")
            {
                if (CollectionFunction.NewCollection(collectionName, ID))
                {
                    MessageBox.Show("Коллекция успешно создана!");
                    this.DialogResult = true;
                }
                else
                {
                    MessageBox.Show("Такая коллекция уже существует. Придумайте новое название");
                }
            }
            else
            {
                MessageBox.Show("Введите название");
            }
        }
    }
}
