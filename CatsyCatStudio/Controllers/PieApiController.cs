using CatsyCatStudio.Interface;
using CatsyCatStudioPieShop.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CatsyCatStudio.Controllers
{
    [Route("api/[controller]")] 
    [ApiController]
    public class PieApiController : Controller
    {
        private readonly IPieRepository _pieRepository;

        public PieApiController(IPieRepository PieRepository)
        {
            _pieRepository = PieRepository;
        }


        [HttpGet]
        [Route("All")]
        public IActionResult AllPies()
        {

            try
            {


                //using (StreamReader r = new StreamReader("wwwroot/client/api/products/products.json"))
                //{
                //    string json = r.ReadToEnd();
                //    var items = JsonConvert.DeserializeObject<List<Product>>(json);
                //    return Json(items);
                //}

                return Ok(_pieRepository.AllPies);
            }
            catch (Exception ex)
            {
                return Json("bad Request");
            }
        }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public IActionResult Get() {

            try
            {


                //using (StreamReader r = new StreamReader("wwwroot/client/api/products/products.json"))
                //{
                //    string json = r.ReadToEnd();
                //    var items = JsonConvert.DeserializeObject<List<Product>>(json);
                //    return Json(items);
                //}

                return Ok(_pieRepository.AllPies);
            }
            catch (Exception ex)
            {
                return Json("bad Request");
            }
        }


        [HttpGet("{pieId}")]
        public IActionResult GetPieById(string pieId) {

            int Id = Convert.ToInt32(pieId);

            var pie = _pieRepository.PieById(Id);

            return Ok(pie);
        }
    }
}
