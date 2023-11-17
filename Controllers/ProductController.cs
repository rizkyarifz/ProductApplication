using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProductApplication.Models;
using ProductApplication.Models.Product;
using ProductApplication.Models.Response;
using ProductApplication.Service;
using ProductApplication.Service.Product;
using System;
using System.Net;
using System.Threading.Tasks;

public class ProductController : Controller
{    
    private readonly IProductService _svc;

    public ProductController(IProductService svc)
    {        
        _svc = svc;
    }
    public async Task<IActionResult> Index(string Filter)
    {
        var products = await _svc.GetAllProducts();

        if (!string.IsNullOrEmpty(Filter))
        {            
            products = products.Where(p =>
                p.NamaBarang.Contains(Filter, System.StringComparison.OrdinalIgnoreCase)
            ).ToList();
        }        
        ViewBag.Filter = Filter;
        return View(products);
    }

    public async Task<IActionResult> DetailProduct(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var product = await _svc.GetProductById(id.Value);
        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }

    public IActionResult CreateProduct()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddProduct(ProductModel product)
    {
        ResponseModel response = new ResponseModel();

        if (ModelState.IsValid)
        {
            try
            {
                await _svc.AddProduct(product);
                TempData["SuccessMessage"] = "Product added successfully"; // Use TempData to pass a success message to the next request
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {                
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
            }
        }

        return View(product);
    }

    public async Task<IActionResult> UpdateProduct(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var product = await _svc.GetProductById((int)id);
        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> EditProduct(ProductModel product)
    {
        ResponseModel response = new ResponseModel();

        if (ModelState.IsValid)
        {
            try
            {
                await _svc.EditProduct(product);
                TempData["SuccessMessage"] = "Product Updated successfully"; // Use TempData to pass a success message to the next request
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
            }
        }

        return View(product);
    }

    public async Task<IActionResult> DeleteProduct(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var product = await _svc.GetProductById((int)id);
        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteProduct(int ID)
    {
        ResponseModel response = new ResponseModel();

        if (ModelState.IsValid)
        {
            try
            {
                await _svc.DeleteProduct(ID);
                TempData["SuccessMessage"] = "Product deleted successfully"; // Use TempData to pass a success message to the next request
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
            }
        }

        return RedirectToAction("Index");
    }

}
