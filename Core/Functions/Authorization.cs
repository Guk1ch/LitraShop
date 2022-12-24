using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Core.DataBase;

namespace Core.Functions
{
	public class Authorization
	{
		public static ObservableCollection<User> users { get; set; }
        public static User AuthorizationUser(string login, string password)
        {
            users = new ObservableCollection<User>(BdConnection.connection.User.ToList());
            var userExists = users.Where(user => user.Login == login && user.Password == password).FirstOrDefault();
            if (userExists != null)
            {
                return userExists;
            }
            else
            {
                return userExists;
            }
        }
        public static void EditUser(int id, byte[] photo, string nick)
        {
            var user = BdConnection.connection.User.Where(x => x.ID == id).FirstOrDefault();
            user.Nickname = nick;
            user.Photo = photo;
            BdConnection.connection.SaveChanges();
        }
      
    }
}
