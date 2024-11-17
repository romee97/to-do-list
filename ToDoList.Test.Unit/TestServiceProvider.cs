using Microsoft.Extensions.DependencyInjection;
using ToDoList.Web;

namespace ToDoList.Test.Unit
{
    public class TestServiceProvider
    {
        private readonly IServiceProvider serviceProvider;

        public TestServiceProvider(TestContext context)
        {
            var serviceCollection = new ServiceCollection();
            var connectionString = context.Properties["DbConnectionString"] as string
                ?? throw new ApplicationException("Database connection info not found");

            ServiceRegistrator.RegisterRequiredServices(serviceCollection, connectionString);

            serviceProvider = serviceCollection.BuildServiceProvider();
        }

        public T Get<T>() where T : class
            => serviceProvider.GetRequiredService<T>();
    }
}
