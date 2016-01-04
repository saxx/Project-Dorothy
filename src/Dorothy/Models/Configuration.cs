using Microsoft.Extensions.Configuration;

namespace Dorothy.Models
{
    public class Configuration
    {
        public Configuration(IConfiguration config)
        {
            UserPassword = config["userPassword"];
            AdminPassword = config["adminPassword"];
            ConnectionString = config["connectionString"];

            if (string.IsNullOrWhiteSpace(UserPassword))
            {
                UserPassword = "userP4ss";
            }

            if (string.IsNullOrEmpty(AdminPassword))
            {
                AdminPassword = "adminP4ss";
            }

            if (string.IsNullOrEmpty(ConnectionString))
            {
                ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Dorothy;Integrated Security=True;MultipleActiveResultSets=True;";
            }
        }

        public string UserPassword { get; }
        public string AdminPassword { get; }

        public string ConnectionString { get; }
    }
}
