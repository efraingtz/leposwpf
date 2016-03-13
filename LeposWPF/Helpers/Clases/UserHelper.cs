using LeposWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeposWPF.Helpers.Clases
{
    /// <summary>
    /// Helper class used for logic business methods related to users
    /// </summary>
    public class UserHelper
    {
        /// <summary>
        /// User logged in the system
        /// </summary>
        public static User loggedUser
        {
            get;
            set;
        }
        /// <summary>
        /// Validate user
        /// </summary>
        /// <param name="ID">ID of user</param>
        /// <param name="password">Password of user</param>
        /// <returns>Whether or not the user and password validates</returns>
        public static Boolean isUser(String ID, string password)
        {
            LeposWPFModel model = new LeposWPFModel();
            var searchUser = model.Users.Where(a=> a.ID == ID && a.Password == password).FirstOrDefault();
            loggedUser = searchUser;
            return loggedUser != null;
        }
        /// <summary>
        /// Save user's password
        /// </summary>
        public static void savePassword()
        {
            LeposWPFModel model = new LeposWPFModel();
            var searchUser = model.Users.Where(a => a.ID == loggedUser.ID).FirstOrDefault();
            searchUser.Password = loggedUser.Password;
            model.SaveChanges();
        }
        /// <summary>
        /// Obtain random password
        /// </summary>
        /// <param name="length">Length of password</param>
        /// <returns></returns>
        public static string generateRandomPassword(int length = 5)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
