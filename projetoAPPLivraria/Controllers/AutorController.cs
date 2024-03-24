using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using projetoAPPLivraria.Models;
using projetoAPPLivraria.Repository;
using projetoAPPLivraria.Repository.Contract;

namespace projetoAPPLivraria.Controllers
{
    public class AutorController : Controller
    {
        private IAutorRepository _autorRepository;
        private IStatusRepository _statusRepository;

        public AutorController(IAutorRepository autoRepository, IStatusRepository statusRepository)
        {
            _autorRepository = autoRepository;
            _statusRepository = statusRepository;
        }

        public IActionResult Index()
        {
            return View(_autorRepository.obterTodosOsAutores());
        }

        public IActionResult CadAutor() {
            return View();
        }
        [HttpPost]

        public IActionResult CadAutor(Autor autor)
        {
                _autorRepository.cadastrar(autor);
                return View();
        }
        public IActionResult editAutor(int id)
        {
            var listaStatus = _statusRepository.obterTodosStatus();
            ViewBag.listaStatus = new SelectList(listaStatus, "codStatus", "nomeStatus");

            return View(_autorRepository.obterAutor(id));
        }
        [HttpPost]
        public IActionResult editAutor(Autor autor)
        {
            _autorRepository.atualizar(autor);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult deleteAutor(int id)
        {

            String retorno = _autorRepository.excluir(id);
            if (retorno != null)
                TempData["mensagemErro"] = retorno;

            return RedirectToAction(nameof(Index));
        }
    }
}
