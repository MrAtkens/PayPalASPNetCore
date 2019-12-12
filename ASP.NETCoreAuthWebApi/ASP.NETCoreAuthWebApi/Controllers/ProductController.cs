using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP.NETCoreAuthWebApi.DataAcces;
using ASP.NETCoreAuthWebApi.DTOs;
using ASP.NETCoreAuthWebApi.Models;
using ASP.NETCoreAuthWebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASP.NETCoreAuthWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly CartContext cartContext;
        private readonly ProductServices productServices;

        public ProductController(ProductServices productServices, CartContext cartContext)
        {
            this.productServices = productServices;
            this.cartContext = cartContext;
        }

        [HttpGet]

        public async Task<ActionResult> GetProducts()
        {
            List<Product> products = await productServices.GetProducts();
            return Ok(new { products });
        }

        [HttpPost]
        public async Task<ActionResult> AddToCart([FromBody]ProductDTOs productDTOs)
        {
            //Аутентификаций

            if (!ModelState.IsValid)
            {
                return Conflict();
            }

            if (await productServices.ProductAddToCart(productDTOs))
            {
                Product product = new Product() { Name = productDTOs.Name, Description = productDTOs.Description, Coast = productDTOs.Coast };
                cartContext.Add(product);
                await cartContext.SaveChangesAsync();
                return Ok(new { answer = "Вы успешно добавили продукт который был у нас в магазине в свою корзину" });
            }

            return Ok(new { answer = "Вы указали не верный продукт мы не нашли его в магазине", error = true });
        }

        [HttpGet]
        public async Task<ActionResult> Payment()
        {
            try
            {
                List<Product> products = await cartContext.Cart.ToListAsync();
                string url = productServices.PaymenCreate(products);
                cartContext.Cart.RemoveRange(products);
                await cartContext.SaveChangesAsync();
                return Ok(new { answer = url });
            }
            catch (Exception ex)
            {
                return Ok(new { answer = "Вы не добавили товар в корзину" });
            }
        }

    }
}