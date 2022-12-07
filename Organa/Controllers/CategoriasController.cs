using Microsoft.AspNetCore.Mvc;
using Organa.Models;
using Organa.Servicios;

namespace Organa.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly IServicioCategorias categorias;
        public CategoriasController(IServicioCategorias servicioCategorias)
        {
            categorias= servicioCategorias;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var Categorias = await categorias.Obtener();
            return View(Categorias);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(CategoriaViewModel Categorias)
        {
            if (!ModelState.IsValid)
            {
                return View(Categorias);
            }

            var Existe = await categorias.ExisteCrear(Categorias.Nombre);

            if (Existe)
            {
                ModelState.AddModelError(nameof(Categorias.Nombre), $"La categoria {Categorias.Nombre} ya se encuentra registrada");
                return View(Categorias);
            }

            await categorias.Crear(Categorias);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Editar(int id)
        {

            var Categoria = await categorias.ObtenerPorId(id);

            if (Categoria is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            return View(Categoria);
        }

        [HttpPost]
        public async Task<ActionResult> Editar(CategoriaViewModel Categoria)
        {
            var Categorias = await categorias.ObtenerPorId(Categoria.Id);

            if (Categorias is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            var Existe = await categorias.Existe(Categoria.Id,Categoria.Nombre);

            if (Existe)
            {
                ModelState.AddModelError(nameof(Categoria.Nombre), $"La categoria {Categoria.Nombre} ya se encuentra registrada");
                return View(Categoria);
            }

            await categorias.Actualizar(Categoria);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Borrar(int id)
        {
            var Categoria = await categorias.ObtenerPorId(id);

            if (Categoria is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            return View(Categoria);
        }

        [HttpPost]
        public async Task<IActionResult> BorrarCategoria(int id)
        {
            var Categoria = await categorias.ObtenerPorId(id);

            if (Categoria is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            await categorias.Eliminar(id);
            return RedirectToAction("Index");
        }
    }
}
