using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TruYum.Models;

namespace WebAPI.Controllers
{   
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private IMenuRepository MenuRepository;

        private IWebHostEnvironment webHostEnvironment;

        public MenuController(IMenuRepository repo, IWebHostEnvironment environment)
        {
            MenuRepository = repo;
            webHostEnvironment = environment;
        }

        [HttpGet]
        public IEnumerable<Menu> GetMenus()
        {
            return MenuRepository.GetAllMenus().ToList();
        }
        [HttpGet("{id}")]
        public Menu GetMenuItemById(int id)
        {
            return MenuRepository.GetMenuItemById(id);
        }



        [HttpPost]
        public Menu Create([FromBody] Menu Menu)
        {
            return MenuRepository.AddMenu(Menu);
        }


        [HttpPut]
        public Menu Update([FromForm] Menu Menu)
        {
            return MenuRepository.UpdateMenu(Menu);
        }


        [HttpDelete("{id}")]
        public void Delete(int id) => MenuRepository.DeleteMenu(id);
    }
}
