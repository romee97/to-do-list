using Microsoft.EntityFrameworkCore;
using ToDoList.DAL.Base;

namespace ToDoList.Test.Unit
{
    public abstract class UnitTestBase<T> where T : notnull
    {
        private TestServiceProvider serviceProvider;

        public TestContext TestContext { get; set; }
        protected T ServiceUnderTest { get; private set; }

        [TestInitialize]
        public void Initialize()
        {
            serviceProvider = new TestServiceProvider(TestContext);
            TestDatabaseInitializer.ResetDatabase(serviceProvider, TestContext, GetDataToSeed());
            ServiceUnderTest = GetService<T>();
        }

        [TestCleanup]
        public void Cleanup()
        {
            serviceProvider.Dispose();
        }

        protected virtual IEnumerable<object> GetDataToSeed() => [];

        protected S GetService<S>() where S : notnull
            => serviceProvider.Get<S>();

        protected void CompleteUnitOfWork()
            => serviceProvider.Get<IUnitOfWork>()
                              .Complete();

        protected DbSet<S> GetDbSet<S>() where S : class
            => serviceProvider.Get<AppDbContext>()
                              .Set<S>();
    }
}
