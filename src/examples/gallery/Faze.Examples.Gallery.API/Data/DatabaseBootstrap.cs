using Dapper;
using DbUp;
using Microsoft.Data.Sqlite;
using System.Linq;
using System.Reflection;

namespace Faze.Examples.Gallery.API.Data
{
    public class DatabaseBootstrap : IDatabaseBootstrap
    {
        private readonly DatabaseConfig databaseConfig;

        public DatabaseBootstrap(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
        }

        public void Setup()
        {
            var upgrader =
                DeployChanges.To
                    .SQLiteDatabase(databaseConfig.Name)
                    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                    .LogToConsole()
                    .Build();

            upgrader.PerformUpgrade();
        }
    }
}
