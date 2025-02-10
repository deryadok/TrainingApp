using Microsoft.AspNetCore.Mvc;

namespace TrainingApp.API.Controllers
{
    public class ExerciseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
