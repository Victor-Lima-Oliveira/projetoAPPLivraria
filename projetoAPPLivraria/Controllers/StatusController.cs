using Microsoft.AspNetCore.Mvc;
using projetoAPPLivraria.Repository.Contract;

namespace projetoAPPLivraria.Controllers
{
    public class StatusController : Controller
    {

        private IStatusRepository _statusRepository;

        public StatusController(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }

        public IActionResult Index()
        {
            
            return View(_statusRepository.obterStatus());
        }
    }
}
