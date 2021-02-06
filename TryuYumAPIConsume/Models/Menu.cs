using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TryuYumAPIConsume.Models
{
    public class Menu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public bool IsFreeDelivery { get; set; }
        public DateTime DateOfLaunch { get; set; }
        public string Category { get; set; }


    }
}
