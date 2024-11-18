using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDoList.DAL.Base;
using ToDoList.Service.Entities.ToDoTask;

namespace ToDoList.Web.Pages
{
    public class ModifyTaskModel : PageModel
    {
        private readonly IToDoTaskRepository toDoTaskRepository;
        private readonly IUnitOfWork unitOfWork;

        [BindProperty]
        public ToDoTask Task { get; set; }

        public ModifyTaskModel(IToDoTaskRepository toDoTaskRepository, IUnitOfWork unitOfWork)
        {
            this.toDoTaskRepository = toDoTaskRepository;
            this.unitOfWork = unitOfWork;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPostSave()
        {
            if (Task.Id != 0)
                toDoTaskRepository.Update(Task);
            else
                toDoTaskRepository.Add(Task);

            unitOfWork.Complete();

            return RedirectToPage("Index");
        }

        public void OnPostAddOrUpdate(ToDoTask task)
        {
            Task = task;
            if (Task.TaskDate == default)
                Task.TaskDate = DateTime.Today;
        }

        public IActionResult OnPostDelete(ToDoTask task)
        {
            toDoTaskRepository.Delete([task.Id]);

            unitOfWork.Complete();

            return RedirectToPage("Index");
        }
    }
}
