using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Crmf;
using projetoAPPLivraria.Models;
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
            
            return View(_statusRepository.obterTodosStatus());
        }
        public IActionResult cadStatus()
        {
            return View();
        }
        [HttpPost]
        public IActionResult cadStatus(Status status)
        {
            _statusRepository.cadastrar(status);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult atualizarStatus(int id)
        { 
            return View(_statusRepository.obterStatus(id));
        }
        [HttpPost]
        public IActionResult atualizarStatus(Status status)
        {
            _statusRepository.atualizar(status);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult deleteAutor(int id)
        {
            String retorno = _statusRepository.excluir(id);
            if (retorno != null)
                TempData["mensagemErro"] = retorno;

            return RedirectToAction(nameof(Index));
        }

    }
}
