using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using projetoAPPLivraria.Models;
using projetoAPPLivraria.Repository.Contract;

namespace projetoAPPLivraria.Controllers
{
    public class AutorController : Controller
    {
        private IAutorRepository _autorRepository;

        public AutorController(IAutorRepository autoRepository)
        {
            _autorRepository = autoRepository;
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
            var listaStatus = _autorRepository.obterStatus();
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
            _autorRepository.excluir(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
