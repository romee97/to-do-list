namespace ToDoList.DAL.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext dbContext;

        public UnitOfWork(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Complete()
            => dbContext.SaveChanges();
    }
}
