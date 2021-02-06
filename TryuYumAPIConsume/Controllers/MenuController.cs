using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TryuYumAPIConsume.Models;

namespace TryuYumAPIConsume.Controllers
{
    public class MenuController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<Menu> MenuList = new List<Menu>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:52230/api/Menu"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    MenuList = JsonConvert.DeserializeObject<List<Menu>>(apiResponse);
                }
            }
            return View(MenuList);
        }

        public ViewResult GetMenu() => View();
     
        [HttpGet]

        public ViewResult AddMenu() => View();

        [HttpPost]

        public async Task<IActionResult> AddMenu(Menu Menu)
        {
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(Menu), Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PostAsync("http://localhost:52230/api/Menu", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        Menu = JsonConvert.DeserializeObject<Menu>(apiResponse);
                    }
                }
                return View(Menu);
            }
            return View();
            //return View(Menu);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateMenu(int id)
        {
            Menu Menu = new Menu();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:52230/api/Menu/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Menu = JsonConvert.DeserializeObject<Menu>(apiResponse);
                }
            }
            return View(Menu);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateMenu(Menu Menu)
        {
            Menu receivedMenu = new Menu();
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    var content = new MultipartFormDataContent();
                    content.Add(new StringContent(Menu.Id.ToString()), "Id");
                    content.Add(new StringContent(Menu.Name), "Name");
                    content.Add(new StringContent(Menu.Category), "Category");
                    content.Add(new StringContent(Menu.DateOfLaunch.ToString()), "DateOfLaunch");
                    content.Add(new StringContent(Menu.IsActive.ToString()), "IsActive");
                    content.Add(new StringContent(Menu.IsFreeDelivery.ToString()), "IsFreeDelivery");
                    content.Add(new StringContent(Menu.Price.ToString()), "Price");


                    using (var response = await httpClient.PutAsync("http://localhost:52230/api/Menu", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        ViewBag.Result = "Success";
                        receivedMenu = JsonConvert.DeserializeObject<Menu>(apiResponse);
                    }
                }
            }
            return View(receivedMenu);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteMenu(int MenuId)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("http://localhost:52230/api/Menu/" + MenuId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            return RedirectToAction("Index");
        }
    }
}
