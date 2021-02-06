using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace TruYum.Models
{
    public class MenuRepository : IMenuRepository
    {
        public IConfiguration Configuration { get; }
        public string connectionString;
        private readonly ILogger<MenuRepository> _logger;
        public MenuRepository(IConfiguration configuration, ILogger<MenuRepository> logger)
        {
            this.Configuration = configuration;
            connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            _logger = logger;
        }
        public Menu AddMenu(Menu menu)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("[dbo].[spInsertIntoMenu]", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    cmd.Parameters.AddWithValue("@Name", menu.Name);
                    cmd.Parameters.AddWithValue("@Category", menu.Category);
                    cmd.Parameters.AddWithValue("@DateOfLaunch", menu.DateOfLaunch);
                    cmd.Parameters.AddWithValue("@IsActive", menu.IsActive);
                    cmd.Parameters.AddWithValue("@IsFreeDelivery", menu.IsFreeDelivery);
                    cmd.Parameters.AddWithValue("@Price", menu.Price);


                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    //ex.Message.ToString();
                    _logger.LogError(ex, "Error at Addmenu() :(");
                    menu = null;
                }

            }
            return menu;
        }

        public void DeleteMenu(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("[dbo].[spDeleteMenu]", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error at Deletemenu() :(");

                }

            }
        }

        public IEnumerable<Menu> GetAllMenus()
        {
            List<Menu> menus = new List<Menu>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("[dbo].[spSelectMenu]", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    conn.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    while(dataReader.Read())
                    {
                        Menu menu = new Menu();
                        menu.Id = Convert.ToInt32(dataReader["Id"]);
                        menu.Name = dataReader["Name"].ToString();
                        menu.Category = dataReader["Category"].ToString();
                        menu.IsFreeDelivery = (bool)dataReader["IsFreeDelivery"];
                        menu.IsActive = (bool)dataReader["IsActive"];
                        menu.Price = (decimal)dataReader["Price"];
                        menu.DateOfLaunch = (DateTime)dataReader["DateOfLaunch"];



                        menus.Add(menu);
                    }
                    dataReader.Close();
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex, "Error at GetAllmenus():");
                    menus = null;
                }
            }
            return menus;
        }



        public Menu UpdateMenu(Menu menu)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("[dbo].[spUpdateMenu]", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    cmd.Parameters.AddWithValue("@Id", menu.Id);
                    cmd.Parameters.AddWithValue("@Name", menu.Name);
                    cmd.Parameters.AddWithValue("@Category", menu.Category);
                    cmd.Parameters.AddWithValue("@DateOfLaunch", menu.DateOfLaunch);
                    cmd.Parameters.AddWithValue("@IsActive", menu.IsActive);
                    cmd.Parameters.AddWithValue("@IsFreeDelivery", menu.IsFreeDelivery);
                    cmd.Parameters.AddWithValue("@Price", menu.Price);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    //ex.Message.ToString();
                    _logger.LogError(ex, "Error at Updatemenu() :(");
                    menu = null;
                }
            }

            return menu;
        }
        public Menu GetMenuItemById(int id)
        {
            Menu Menu = new Menu();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("[dbo].[spSelectMenuItemById]", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.AddWithValue("@Id", id);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {                      
                        Menu.Id = Convert.ToInt32(rdr["Id"]);
                        Menu.Name = rdr["Name"].ToString();
                        Menu.Category = rdr["Category"].ToString();
                        Menu.IsFreeDelivery = (bool)rdr["IsFreeDelivery"];
                        Menu.IsActive = (bool)rdr["IsActive"];
                        Menu.Price = (decimal)rdr["Price"];
                        Menu.DateOfLaunch = (DateTime)rdr["DateOfLaunch"];
                    }


                    rdr.Close();
                }
                catch (Exception ex)
                {
                    //ex.Message.ToString();
                    _logger.LogError(ex, "Error at GetMenuById() :(");
                    Menu = null;
                }
            }
            return Menu;
        }
    }
}
