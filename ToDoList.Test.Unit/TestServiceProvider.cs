using Microsoft.Extensions.DependencyInjection;
using ToDoList.Web;

namespace ToDoList.Test.Unit
{
    public class TestServiceProvider : IDisposable
    {
        private IServiceScope serviceScope;

        public TestServiceProvider(TestContext context)
        {
            var serviceCollection = new ServiceCollection();
            var connectionString = context.Properties["DbConnectionString"] as string
                ?? throw new ApplicationException("Test database connection info not found");

            ServiceRegistrator.RegisterRequiredServices(serviceCollection, connectionString);

            serviceScope = serviceCollection.BuildServiceProvider()
                                            .CreateScope();
        }

        public void Dispose()
            => serviceScope.Dispose();

        public T Get<T>() where T : notnull 
            => serviceScope.ServiceProvider.GetRequiredService<T>();
    }
}
