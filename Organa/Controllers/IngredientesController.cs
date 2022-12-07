using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Organa.Models;
using Organa.Servicios;

namespace Organa.Controllers
{
    public class IngredientesController : Controller
    {
        private readonly IServicioCategorias categorias;
        private readonly IServicioIngredientes ingredientes;
        private readonly IMapper mapper;
        public IngredientesController(IServicioIngredientes servicioIngredientes, IServicioCategorias servicioCategorias, IMapper automapper)
        {
            categorias= servicioCategorias;
            ingredientes= servicioIngredientes;
            mapper= automapper;
        }

        public async Task<IActionResult> Index()
        {
            var Ingredientes = await ingredientes.Obtener();
            return View(Ingredientes);
        }

        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            var modelo = new IngredienteCreacionViewModel();
            modelo.CategoriaViewModel = await ObtenerCategorias();
            return View(modelo);
        }


        [HttpPost]
        public async Task<IActionResult> Crear(IngredienteCreacionViewModel ingrediente)
        {
            if (!ModelState.IsValid)
            {
                ingrediente.CategoriaViewModel = await ObtenerCategorias();
                return View(ingrediente);
            }

            var Existe = await ingredientes.ExisteCrear(ingrediente.Nombre);

            if (Existe)
            {
                ModelState.AddModelError(nameof(ingrediente.Nombre), $"el nombre del ingrediente {ingrediente.Nombre} ya se encuentra registrado en el sistema");
                ingrediente.CategoriaViewModel = await ObtenerCategorias();
                return View(ingrediente);
            }

            await ingredientes.Crear(ingrediente);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var ingrediente = await ingredientes.ObtenerPorId(id);

            if (ingrediente is null)
            {
                return RedirectToAction("NoEncontrado", "Index");
            }

            var modelo = mapper.Map<IngredienteCreacionViewModel>(ingrediente);

            modelo.CategoriaViewModel = await ObtenerCategorias();
            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(IngredienteCreacionViewModel ingrediente)
        {
            var Ingredientes = await ingredientes.ObtenerPorId(ingrediente.Id);

            if (Ingredientes is null)
            {
                return RedirectToAction("NoEncontrado", "Index");
            }

            var Existe = await ingredientes.Existe(ingrediente.Nombre, ingrediente.Id);

            if (Existe)
            {
                ModelState.AddModelError(nameof(ingrediente.Nombre), $"el nombre del ingrediente {ingrediente.Nombre} Ya se encuentra registrado en el sistema");
                ingrediente.CategoriaViewModel = await ObtenerCategorias();
                return View(ingrediente);
            }

            await ingredientes.Actualizar(ingrediente);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Borrar(int id)
        {
            var ingrediente = await ingredientes.ObtenerPorId(id);

            if (ingrediente is null)
            {
                return RedirectToAction("NoEncontrado", "Index");
            }

            return View(ingrediente);
        }

        [HttpPost]
        public async Task<IActionResult> BorrarVehiculo(int id)
        {
            var ingrediente = await ingredientes.ObtenerPorId(id);

            if (ingrediente is null)
            {
                return RedirectToAction("NoEncontrado", "Index");
            }

            await ingredientes.Eliminar(id);
            return RedirectToAction("Index");
        }

        private async Task<IEnumerable<SelectListItem>> ObtenerCategorias()
        {
            var Categorias = await categorias.Obtener();
            return Categorias.Select(x => new SelectListItem(x.Nombre, x.Id.ToString()));

        }
    }
}
