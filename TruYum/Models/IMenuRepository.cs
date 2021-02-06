using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TruYum.Models
{
    public interface IMenuRepository
    {
        IEnumerable<Menu> GetAllMenus();
        Menu AddMenu(Menu menu);
        Menu UpdateMenu(Menu menu);
        Menu GetMenuItemById(int id);
        void DeleteMenu(int id);

    }
}
