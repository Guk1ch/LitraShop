using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Core.DataBase;

namespace Core.Functions
{
	public class RegistrationFunction
	{
        public static ObservableCollection<User> users { get; set; }
        public static void RegistrationUser(string nick, string login, string password, byte[] photo)
        {
            User newUser = new User();

            newUser.Nickname = nick;
            newUser.Login = login;
            newUser.Password = password;
            newUser.Photo = photo;

            BdConnection.connection.User.Add(newUser);
            BdConnection.connection.SaveChanges();
            NewUserCollection();

        }
        public static bool UniqueLogin(string login)
        {
            users = new ObservableCollection<User>(BdConnection.connection.User.ToList());
            bool LoginUnic = true;
            foreach (var i in users)
            {
                if (i.Login == login)
                {
                    LoginUnic = false;
                }
            }
            return LoginUnic;
        }
        public static void NewUserCollection()
        {
            users = new ObservableCollection<User>(BdConnection.connection.User.ToList());

            Collection favouritesСollection = new Collection();
            Collection viewedCollection = new Collection();
            favouritesСollection.ID_User = users.Last().ID;
            favouritesСollection.Name = "Избранное";
            viewedCollection.ID_User = users.Last().ID;
            viewedCollection.Name = "Прочитано";
            BdConnection.connection.Collection.Add(favouritesСollection);
            BdConnection.connection.Collection.Add(viewedCollection);
            BdConnection.connection.SaveChanges();
        }
    }
}
