using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using projetoAPPLivraria.Models;
using projetoAPPLivraria.Repository.Contract;

namespace projetoAPPLivraria.Controllers
{
    public class LivroController : Controller
    {

        private ILivroRepository _livroRepository;
        private IAutorRepository _autorRepository;


        public LivroController(ILivroRepository livroRepository, IAutorRepository autorRepository)
        {
            _livroRepository = livroRepository;
            _autorRepository = autorRepository;
        }

        public IActionResult Index()
        {
            return View(_livroRepository.obterTodosOsLivros());
        }


        [HttpGet]
        public IActionResult cadLivro() {
            var listaAutor = _autorRepository.obterTodosOsAutores();
            var objAutor = new Livro
            {
                listaAutor = (List<Autor>)listaAutor
            };
            ViewBag.listaAutores = new SelectList(listaAutor, "id", "nomeAutor");
            return View();
        }
        

        [HttpPost]
        public IActionResult cadLivro(Livro livro) {
            var listaAutor = _autorRepository.obterTodosOsAutores();
            ViewBag.listaAutores = new SelectList(listaAutor, "id", "nomeAutor");

            _livroRepository.cadastrar(livro);
            return RedirectToAction(nameof(Index));

        }
        [HttpGet]
        public IActionResult editarLivro(int id)
        {
            var listaAutor = _autorRepository.obterTodosOsAutores();
           

            ViewBag.ListaAutores = new SelectList(listaAutor, "id", "nomeAutor");
            return View(_livroRepository.obterLivro(id));

        }
        [HttpPost]
        public IActionResult editarLivro(Livro livro)
        {
            var listaAutor = _autorRepository.obterTodosOsAutores();
            ViewBag.ListaAutores = new SelectList(listaAutor, "id", "nomeAutor");
            _livroRepository.atualizar(livro);
            return RedirectToAction(nameof(Index));

        }

        public IActionResult delete(int id)
        {
            _livroRepository.excluir(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
