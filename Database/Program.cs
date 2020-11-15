using DbUp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = "data source=(localdb)\\mssqllocaldb;initial catalog=SomeOnlineRPG;integrated security=True";

            EnsureDatabase.For.SqlDatabase(connectionString);

            var upgrader =
                   DeployChanges.To
                       .SqlDatabase(connectionString)
                       .WithScriptsEmbeddedInAssembly(Assembly.GetCallingAssembly())
                       .LogToConsole()
                       .Build();

            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.Error);
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Success!");
                Console.ResetColor();
            }
        }
    }
}
