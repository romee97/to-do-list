using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDoList.Service.Entities.ToDoTask;

namespace ToDoList.Web.Pages
{
    public class ViewTaskModel : PageModel
    {

        [BindProperty]
        public ToDoTask Task { get; set; }

        public void OnGet()
        {
        }

        public void OnPost(ToDoTask task)
            => Task = task;
    }
}
