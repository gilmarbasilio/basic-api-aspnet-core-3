
using System.Collections.Generic;
using System.Threading.Tasks;
using BasicApiAspnetCore3.Data;
using BasicApiAspnetCore3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BasicApiAspnetCore3.Controllers
{
  public class ProductController : ControllerBase
  {
    [HttpGet]
    [Route("")]
    public async Task<ActionResult<List<Product>>> Get([FromServices] DataContext context)
    {
      var products = await context.Products.Include(x => x.Category).ToListAsync();
      return products;
    }

    [HttpPost]
    [Route("")]
    public async Task<ActionResult<Category>> Post([FromServices] DataContext context, [FromBody]Category model)
    {
      if (ModelState.IsValid)
      {
        context.Categories.Add(model);
        await context.SaveChangesAsync();
        return model;
      }
      else
      {
        return BadRequest(ModelState);
      }
    }
  }
}