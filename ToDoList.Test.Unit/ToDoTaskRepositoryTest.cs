using ToDoList.Service.Entities.ToDoTask;

namespace ToDoList.Test.Unit
{
    [TestClass]
    public class ToDoTaskRepositoryTest : UnitTestBase<IToDoTaskRepository>
    {
        [TestMethod]
        public void Add()
        {
            var repository = ServiceUnderTest;
            var taskToAdd = new ToDoTask
            {
                Title = "new task",
                Description = "new description",
                TaskDate = new DateTime(2010, 11, 1),
                IsDone = true,
            };

            repository.Add(taskToAdd);
            CompleteUnitOfWork();

            var dbSet = GetDbSet<ToDoTask>();
            var taskInDatabase = dbSet.SingleOrDefault(t => t.Title == taskToAdd.Title 
                                                         && t.TaskDate == taskToAdd.TaskDate);
            Assert.IsNotNull(taskInDatabase);
            Assert.AreEqual(taskToAdd.Description, taskInDatabase.Description);
            Assert.AreEqual(taskToAdd.IsDone, taskInDatabase.IsDone);
            Assert.AreEqual(dbSet.Count(), taskInDatabase.Id);
        }

        [TestMethod]
        public void Update()
        {
            var repository = ServiceUnderTest;
            var taskUpdateData = new ToDoTask
            {
                Id = 2,
                Title = "updated task",
                Description = "updated description",
                TaskDate = new DateTime(2021, 3, 8),
                IsDone = true,
            };

            repository.Update(taskUpdateData);
            CompleteUnitOfWork();

            var taskInDatabase = GetDbSet<ToDoTask>().Single(t => t.Id == taskUpdateData.Id);
            Assert.AreEqual(taskUpdateData.Title, taskInDatabase.Title);
            Assert.AreEqual(taskUpdateData.Description, taskInDatabase.Description);
            Assert.AreEqual(taskUpdateData.TaskDate, taskInDatabase.TaskDate);
            Assert.AreEqual(taskUpdateData.IsDone, taskInDatabase.IsDone);
        }

        [TestMethod]
        public void Delete()
        {
            var repository = ServiceUnderTest;
            var idsToDelete = new[] { 1, 3 };

            repository.Delete(idsToDelete);
            CompleteUnitOfWork();

            var tasksInDatabase = GetDbSet<ToDoTask>().Where(t => idsToDelete.Contains(t.Id));
            Assert.IsFalse(tasksInDatabase.Any());
        }

        protected override IEnumerable<object> GetDataToSeed() => new object[]
        {
            new ToDoTask { Title = "task1", Description = "description1", TaskDate = new DateTime(2001, 2, 3), IsDone = false },
            new ToDoTask { Title = "task2", Description = "description2", TaskDate = new DateTime(2002, 3, 4), IsDone = false },
            new ToDoTask { Title = "task3", Description = "description3", TaskDate = new DateTime(2003, 4, 5), IsDone = false },
        };
    }
}
