// ProductsController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProductApplication.Models;
using ProductApplication.Models.Product;
using ProductApplication.Models.Response;
using ProductApplication.Service;
using System;
using System.Net;
using System.Threading.Tasks;

public class ProductController : Controller
{    
    private readonly ProductService _svc;

    public ProductController(ProductService svc)
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

    [HttpPost, ActionName("Add")]
    [ValidateAntiForgeryToken]
    public async Task<ResponseModel> AddProduct([FromBody] ProductModel product)
    {
        ResponseModel response = new ResponseModel();

        if (ModelState.IsValid)
        {
            try
            {
                await _svc.AddProduct(product);

                response.status_code = (int)HttpStatusCode.OK;
                response.message = "Product added successfully";
            }
            catch (Exception ex)
            {
                response.status_code = (int)HttpStatusCode.InternalServerError;
                response.message = $"An error occurred: {ex.Message}";
            }
        }
        else
        {
            response.status_code = (int)HttpStatusCode.BadRequest;
            response.message = "Invalid product data";
        }

        return response;
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


    [HttpPatch, ActionName("Update")]
    [ValidateAntiForgeryToken]
    public async Task<ResponseModel> EditProduct([FromBody] ProductModel product)
    {
        ResponseModel response = new ResponseModel();

        if (ModelState.IsValid)
        {
            try
            {
                await _svc.EditProduct(product);

                response.status_code = (int)HttpStatusCode.OK;
                response.message = "Product updated successfully";
            }
            catch (Exception ex)
            {
                response.status_code = (int)HttpStatusCode.InternalServerError;
                response.message = $"An error occurred: {ex.Message}";
            }
        }
        else
        {
            response.status_code = (int)HttpStatusCode.BadRequest;
            response.message = "Invalid product data";
        }

        return response;
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

    [HttpDelete, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<ResponseModel> DeleteProduct([FromBody] int ID)
    {
        ResponseModel response = new ResponseModel();

        if (ModelState.IsValid)
        {
            try
            {
                await _svc.DeleteProduct(ID);

                response.status_code = (int)HttpStatusCode.OK;
                response.message = "Product updated successfully";
            }
            catch (Exception ex)
            {
                response.status_code = (int)HttpStatusCode.InternalServerError;
                response.message = $"An error occurred: {ex.Message}";
            }
        }
        else
        {
            response.status_code = (int)HttpStatusCode.BadRequest;
            response.message = "Invalid product data";
        }

        return response;
    }

}
