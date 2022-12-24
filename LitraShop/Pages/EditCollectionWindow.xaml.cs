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
	/// Interaction logic for EditCollectionWindow.xaml
	/// </summary>
	public partial class EditCollectionWindow : Window
	{
		public int ID;
		public EditCollectionWindow(int idColl)
		{
			InitializeComponent();
			ID = idColl;
			this.DataContext = this;
		}
		private void BtnSaveClick(object sender, RoutedEventArgs e)
		{
			string collectionName = TbName.Text;
			CollectionFunction.EditCollectionName(ID, collectionName);
			this.DialogResult = true;
		}
	}
}
