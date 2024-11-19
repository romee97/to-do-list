using System.Text.RegularExpressions;
using ToDoList.DAL.Base;

namespace ToDoList.Test.Unit
{
    internal class TestDatabaseInitializer
    {
        public static void ResetDatabase(TestServiceProvider serviceProvider, TestContext testContext, IEnumerable<object> records)
        {
            TryRemoveDbFile(testContext);

            
            var dbContext = serviceProvider.Get<AppDbContext>();
            dbContext.Database.EnsureCreated();

            var recordsByType = records.ToLookup(x => x.GetType());
            var seedMethod = typeof(TestDatabaseInitializer).GetMethod(nameof(Seed))!;

            foreach (var grouping in recordsByType)
                seedMethod.MakeGenericMethod(grouping.Key)
                          .Invoke(null, [dbContext, grouping]);

            dbContext.SaveChanges();
        }

        private static void TryRemoveDbFile(TestContext testContext)
        {
            var regex = new Regex("(?:Data source=)(.+)(?:;)");
            var connectionString = testContext.Properties["DbConnectionString"] as string
                ?? throw new ApplicationException("Database connection info not found.");

            if (!regex.IsMatch(connectionString))
                throw new ApplicationException("Could not parse database connection info.");

            var dbFilePath = regex.Match(connectionString).Groups[1].Value;

            if (File.Exists(dbFilePath))
                File.Delete(dbFilePath);
        }

        public static void Seed<T>(AppDbContext dbContext, IEnumerable<object> records) where T : class
        {
            dbContext.Set<T>()
                     .AddRange(records.OfType<T>());
        }
    }
}
