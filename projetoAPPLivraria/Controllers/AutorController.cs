using Microsoft.AspNetCore.Mvc;
using projetoAPPLivraria.Models;
using projetoAPPLivraria.Repository.Contract;

namespace projetoAPPLivraria.Controllers
{
    public class AutorController : Controller
    {
        private IAutorRepository _autoRepository;

        public AutorController(IAutorRepository autoRepository)
        {
            _autoRepository = autoRepository;
        }

        public IActionResult Index()
        {
            return View(_autoRepository.obterTodosOsAutores());
        }

        public IActionResult CadAutor() {
            return View();
        }
        [HttpPost]

        public IActionResult CadAutor(Autor autor)
        {
          
                _autoRepository.cadastrar(autor);
                return View();
            

        }
    }
}
