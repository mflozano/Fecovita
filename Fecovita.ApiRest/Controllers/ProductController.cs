using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Fecovita.ApiRest.Models;
using Fecovita.Core.Entities;
using Fecovita.Core.IRepository.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Fecovita.ApiRest.Controllers
{
    [Route("api/[controller]/catalogo")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        public IGenericRepository<Product> ProductRepository { get; }
        private readonly IMapper Mapper;

        public ProductController(IGenericRepository<Product> productRepository, IMapper mapper)
        {
            ProductRepository = productRepository;
            Mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IList<ProductModel>> Get()
        {
            var catalog = ProductRepository.Filter().Select(x=> Mapper.Map<ProductModel>(x)).ToList();
            return Ok(catalog);
        }

        [HttpGet("{id:int}")]
        public ActionResult Get(int id)
        {
            var model = Mapper.Map<ProductModel>(ProductRepository.GetById(id));
            return Ok(model);
        }

        [HttpGet("{category}")]
        public ActionResult<IList<ProductModel>> GetByCategoy(string category)
        {
            var products = ProductRepository.Filter(x=>x.Category == category).Select(x => Mapper.Map<ProductModel>(x)).ToList();
            return Ok(products);
        }

        [HttpPost]
        public ActionResult Post([FromBody] ProductModel model)
        {
            try
            {
                var product = new Product
                {
                    Name = model.Name,
                    Code = model.Code,
                    Price = model.Price,
                    Category = model.Category,
                    Description = model.Description,
                    Liters = model.Liters
                };

                ProductRepository.Create(product);
                ProductRepository.SaveChanges();
                return Ok(product.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] ProductModel model)
        {
            try
            {
                if (model.Id != id) return BadRequest();
                var product = ProductRepository.GetById(id);

                product.Name = model.Name;
                product.Code = model.Code;
                product.Price = model.Price;
                product.Category = model.Category;
                product.Description = model.Description;
                product.Liters = model.Liters;

                ProductRepository.Edit(product);
                ProductRepository.SaveChanges();
                return Ok(product.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var product = ProductRepository.GetById(id);
                if (product == null) return BadRequest();

                ProductRepository.Delete(product);
                ProductRepository.SaveChanges();
                return Ok(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
