using Microsoft.Extensions.Configuration;

namespace Dorothy.Models
{
    public class Configuration
    {
        public Configuration(IConfiguration config)
        {
            NormalUserPassword = config["userPassword"];
            InsiderUserPassword = config["insiderPassword"];
            AdminPassword = config["adminPassword"];
            ConnectionString = config["connectionString"];

            if (string.IsNullOrWhiteSpace(NormalUserPassword))
            {
                NormalUserPassword = "userP4ss";
            }

            if (string.IsNullOrWhiteSpace(InsiderUserPassword))
            {
                InsiderUserPassword = "insiderP4ss";
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

        public string NormalUserPassword { get; }
        public string InsiderUserPassword { get; }
        public string AdminPassword { get; }

        public string ConnectionString { get; }
    }
}
