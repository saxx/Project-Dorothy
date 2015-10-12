using Microsoft.Framework.Configuration;

namespace Dorothy.Models
{
    public class Configuration
    {
        public Configuration(IConfiguration config)
        {
            UserPassword = config["userPassword"];
            AdminPassword = config["adminPassword"];

            if (string.IsNullOrWhiteSpace(UserPassword))
            {
                UserPassword = "userP4ss";
            }

            if (string.IsNullOrEmpty(AdminPassword))
            {
                AdminPassword = "adminP4ss";
            }
        }

        public string UserPassword { get; private set; }
        public string AdminPassword { get; private set; }
    }
}
