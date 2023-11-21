using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Productos_y_Categorias.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Globalization;

using Microsoft.AspNetCore.Mvc.Filters;

namespace Productos_y_Categorias.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private MyContext _context;

    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        List<Producto> ListaProductos = _context.Productos.ToList();
        ViewBag.AllProductos = ListaProductos;
        return View("Index");
    }

    [HttpPost("CrearProducto")]
    public IActionResult CrearProducto(Producto NuevoProducto)
    {
        _context.Add(NuevoProducto);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    [HttpGet("producto/{prodId}")]
    public IActionResult SpecificProduct(int prodId)
    {
        Producto? VerProd = _context.Productos.FirstOrDefault(p => p.ProductoId == prodId);
        ViewBag.ThisProducto = VerProd;

        var prodWithCats = _context.Productos
            .Include(p => p.AssocCategoria)
            .ThenInclude(c => c.Categoria)
            .FirstOrDefault(p => p.ProductoId == prodId);

        ViewBag.ProductWithCategories = prodWithCats;

        List<Categoria> ListaCategorias = _context.Categorias.ToList();
        List<Categoria> AlgunasCategories = new();

        if (prodWithCats != null)
        {
            foreach (var c in prodWithCats.AssocCategoria)
            {
                AlgunasCategories.Add(c.Categoria);
            }
            List<Categoria> NotYetAssoc = ListaCategorias.Except(AlgunasCategories).ToList();
            ViewBag.NotYetAssoc = NotYetAssoc;
            return View("VerProducto");
        }
        return View("producto");
    }

    [HttpPost("AddCatToProd")]
    public IActionResult AddCatToProd(Asociacion NuevaAssociacion)
    {
        _context.Add(NuevaAssociacion);
        _context.SaveChanges();
        return Redirect("/producto/" + NuevaAssociacion.ProductoId);
    }

    [HttpGet("categorias")]
    public IActionResult Categories()
    {
        List<Categoria> ListaCategorias = _context.Categorias.ToList();
        ViewBag.AllCategorias = ListaCategorias;
        return View("Categorias");
    }

    [HttpPost("CrearCategoria")]
    public IActionResult CrearCategoria(Categoria NuevaCatgoria)
    {
        _context.Add(NuevaCatgoria);
        _context.SaveChanges();
        return Redirect("categorias");
    }

    [HttpGet("categorias/{catId}")]
    public IActionResult SpecificCategory(int catId)
    {
        Categoria? verCat = _context.Categorias.FirstOrDefault(p => p.CategoriaId == catId);
        ViewBag.ThisCategoria = verCat;

        var catWithProds = _context.Categorias
            .Include(p => p.AssocProducto)
            .ThenInclude(c => c.Producto)
            .FirstOrDefault(p => p.CategoriaId == catId);

        ViewBag.CategoryWithProducts = catWithProds;

        List<Producto> ListaProductos = _context.Productos.ToList();
        List<Producto> AlgunosProductos = new();

        if (catWithProds != null)
        {
            foreach (var p in catWithProds.AssocProducto)
            {
                AlgunosProductos.Add(p.Producto);
            }
            List<Producto> NotYetAssoc = ListaProductos.Except(AlgunosProductos).ToList();
            ViewBag.NotYetAssoc = NotYetAssoc;
            return View("VerCategoria");
        }
        return View("Categoria");
    }

    [HttpPost("AddProdToCat")]
    public IActionResult AddProdToCat(Asociacion NuevaAssoc)
    {
        _context.Add(NuevaAssoc);
        _context.SaveChanges();
        return Redirect("/categorias/" + NuevaAssoc.CategoriaId);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
