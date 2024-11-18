using Microsoft.Extensions.DependencyInjection;
using ToDoList.Service.Entities.ToDoTask;

namespace ToDoList.Test.Unit
{
    [TestClass]
    public class ToDoTaskQueryServiceTest : UnitTestBase<IToDoTaskQueryService>
    {
        [TestMethod]
        public void Get()
        {
            var queryService = ServiceUnderTest;
            var dateFrom = new DateTime(2001, 12, 31);
            var dateTo = new DateTime(2003, 3, 3);

            var tasks = queryService.Get(dateFrom, dateTo);
            
            var taskArray = tasks.OrderBy(t => t.Title)
                                 .ToArray();
            Assert.AreEqual(2, taskArray.Length);
            Assert.AreEqual("task2", taskArray[0].Title);
            Assert.AreEqual("task3", taskArray[1].Title);
        }

        [TestMethod]
        public void TryGet()
        {
            var queryService = ServiceUnderTest;
            var title = "task2";
            var date = new DateTime(2002, 2, 2);

            var record = queryService.TryGet(title, date);

            Assert.IsNotNull(record);
            Assert.AreEqual("description2", record.Description);
        }

        protected override IEnumerable<object> GetDataToSeed() => new object[]
        {
            new ToDoTask { Title = "task1", Description = "description1", TaskDate = new DateTime(2001, 1, 1), IsDone = false },
            new ToDoTask { Title = "task2", Description = "description2", TaskDate = new DateTime(2002, 2, 2), IsDone = false },
            new ToDoTask { Title = "task2", Description = "task2 for another date", TaskDate = new DateTime(2012, 12, 12), IsDone = false },
            new ToDoTask { Title = "task3", Description = "description3", TaskDate = new DateTime(2003, 3, 3), IsDone = false },
            new ToDoTask { Title = "task4", Description = "description4", TaskDate = new DateTime(2004, 4, 4), IsDone = false },
        };
    }
}