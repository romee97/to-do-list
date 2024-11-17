using Microsoft.Extensions.DependencyInjection;
using ToDoList.Model.Entities.ToDoTask;

namespace ToDoList.Test.Unit
{
    [TestClass]
    public class ToDoTaskQueryServiceTest
    {
        private TestServiceProvider serviceProvider;

        public TestContext TestContext { get; set; }

        [TestInitialize]
        public async Task Initialize()
        {
            serviceProvider = new TestServiceProvider(TestContext);
            await TestDatabaseInitializer.ResetDatabase(serviceProvider, TestContext, GetDataToSeed());
        }

        [TestMethod]
        public async Task Get()
        {
            var queryService = serviceProvider.Get<IToDoTaskQueryService>();

            var tasks = await queryService.Get(new DateTime(2001, 12, 31), new DateTime(2003, 3, 3));
            
            var taskArray = tasks.OrderBy(t => t.Title)
                                 .ToArray();
            Assert.AreEqual(2, taskArray.Length);
            Assert.AreEqual("task2", taskArray[0].Title);
            Assert.AreEqual("task3", taskArray[1].Title);
        }

        private IEnumerable<object> GetDataToSeed() => new object[]
        {
            new ToDoTask { Title = "task1", Description = "description1", TaskDate = new DateTime(2001, 1, 1), IsDone = false },
            new ToDoTask { Title = "task2", Description = "description2", TaskDate = new DateTime(2002, 2, 2), IsDone = false },
            new ToDoTask { Title = "task3", Description = "description3", TaskDate = new DateTime(2003, 3, 3), IsDone = false },
            new ToDoTask { Title = "task4", Description = "description4", TaskDate = new DateTime(2004, 4, 4), IsDone = false },
        };
    }
}