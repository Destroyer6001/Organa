using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Organa.Models;
using Organa.Servicios;
using System.Reflection;

namespace Organa.Controllers
{
    public class PlatosController : Controller
    {
        private readonly IMapper mapper;
        private readonly IServicioIngredientes ingredientes;
        private readonly IServicioPlato platos;
        private readonly IAlmacenadorDeArchivos archivos;
        private readonly string contenedor = "Files";
        public PlatosController(IServicioPlato servicioPlato, IServicioIngredientes servicioIngredientes, IAlmacenadorDeArchivos almacenadorDeArchivos, IMapper automapper)
        {
            mapper= automapper;
            ingredientes= servicioIngredientes;
            platos = servicioPlato;
            archivos = almacenadorDeArchivos;

        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var Platos = await platos.Obtener();
            return View(Platos);
        }

        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            var categoriacarnebuscar = 1;
            var categoriagranobuscar = 2;
            var categoriaarrozbuscar = 3;


            var modelo = new PlatoCreacionViewModel();
            modelo.CategoriaCarne = await ObtenerCategoriasCarne(categoriacarnebuscar);
            modelo.Categoriagrano = await ObtenerCategoriasGranos(categoriagranobuscar);
            modelo.CategoriaArroz = await ObtenerCategoriasArroz(categoriaarrozbuscar);
            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(PlatoCreacionViewModel plato)
        {
            var categoriacarnebuscar = 1;
            var categoriagranobuscar = 2;
            var categoriaarrozbuscar = 3;

            if (!ModelState.IsValid)
            {
                plato.CategoriaCarne = await ObtenerCategoriasCarne(categoriacarnebuscar);
                plato.Categoriagrano = await ObtenerCategoriasGranos(categoriagranobuscar);
                plato.CategoriaArroz = await ObtenerCategoriasArroz(categoriaarrozbuscar);

                return View(plato);
            }

            var Ingrediente1 = await ingredientes.ObtenerPorId(plato.CategoriaCarneId);
            if((plato.Cantidad * plato.CantidadCarne) > Ingrediente1.Cantidad)
            {
                ModelState.AddModelError(nameof(plato.CantidadCarne), $"No se cuenta en bodega con la cantidad de carne solicitada");
                plato.CategoriaCarne = await ObtenerCategoriasCarne(categoriacarnebuscar);
                plato.Categoriagrano = await ObtenerCategoriasGranos(categoriagranobuscar);
                plato.CategoriaArroz = await ObtenerCategoriasArroz(categoriaarrozbuscar);

                return View(plato);
            }

            var Ingrediente2 = await ingredientes.ObtenerPorId(plato.CategoriaGranoId);
            if ((plato.Cantidad * plato.CantidadGrano) > Ingrediente2.Cantidad)
            {
                ModelState.AddModelError(nameof(plato.CantidadGrano), $"No se cuenta en bodega con la cantidad de grano solicitada");
                plato.CategoriaCarne = await ObtenerCategoriasCarne(categoriacarnebuscar);
                plato.Categoriagrano = await ObtenerCategoriasGranos(categoriagranobuscar);
                plato.CategoriaArroz = await ObtenerCategoriasArroz(categoriaarrozbuscar);

                return View(plato);
            }

            var Ingrediente3 = await ingredientes.ObtenerPorId(plato.CategoriaArrozId);
            if ((plato.Cantidad * plato.CantidadArroz) > Ingrediente3.Cantidad)
            {
                ModelState.AddModelError(nameof(plato.CantidadArroz), $"No se cuenta en bodega con la cantidad de arroz solicitada");
                plato.CategoriaCarne = await ObtenerCategoriasCarne(categoriacarnebuscar);
                plato.Categoriagrano = await ObtenerCategoriasGranos(categoriagranobuscar);
                plato.CategoriaArroz = await ObtenerCategoriasArroz(categoriaarrozbuscar);

                return View(plato);
            }

            var Existe = await platos.ExisteCrear(plato.Nombre);
            if(Existe)
            {
                ModelState.AddModelError(nameof(plato.Nombre), $"el plato {plato.Nombre} ya se encuentra registrado en el sistema");
                plato.CategoriaCarne = await ObtenerCategoriasCarne(categoriacarnebuscar);
                plato.Categoriagrano = await ObtenerCategoriasGranos(categoriagranobuscar);
                plato.CategoriaArroz = await ObtenerCategoriasArroz(categoriaarrozbuscar);

                return View(plato);
            }


            if (plato.SeleccionarImagen != null)
            {

                plato.Imagen = await archivos.GuardarArchivo(contenedor, plato.SeleccionarImagen);

            }

            await platos.Crear(plato);
            return RedirectToAction("Index");
        }


        private async Task<IEnumerable<SelectListItem>> ObtenerCategoriasCarne(int CategoriaId)
        {
            var Ingredientes = await ingredientes.ObtenerPorCategoriaId(CategoriaId);
            return Ingredientes.Select(x => new SelectListItem(x.Nombre, x.Id.ToString()));
        }

        private async Task<IEnumerable<SelectListItem>> ObtenerCategoriasGranos(int CategoriaId)
        {
            var Ingredientes = await ingredientes.ObtenerPorCategoriaId(CategoriaId);
            return Ingredientes.Select(x => new SelectListItem(x.Nombre, x.Id.ToString()));
        }

        private async Task<IEnumerable<SelectListItem>> ObtenerCategoriasArroz(int CategoriaId)
        {
            var Ingredientes = await ingredientes.ObtenerPorCategoriaId(CategoriaId);
            return Ingredientes.Select(x => new SelectListItem(x.Nombre, x.Id.ToString()));
        }
    }
}
