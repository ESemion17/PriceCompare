using System.Runtime.InteropServices;

namespace PriceCompareLogic
{
    public class UserFactory
    {
        private string _name;

        public static void CreateOrUpdateAdmin(string name = "Admin", string password = "123456")
        {
            DbAccessor.CreateOrUpdateAdmin(new User()
            {
                Master = true,
                UserName = name,
                Password = password
            });
        }

        public bool NewUser(string name)
        {
            return DbAccessor.InsertUser(name);
        }
    }
}