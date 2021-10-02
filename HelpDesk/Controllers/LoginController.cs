using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace HelpDesk.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Enter(string usuario, string password)
        {
            try
            {
                using (NpgsqlConnection db = new NpgsqlConnection("HOST=127.0.0.1;Port=5432; User Id=postgres;Password=1nfabc123;Database = dbmmc;TIMEOUT=15;POOLING=True;MINPOOLSIZE=1;MAXPOOLSIZE=20;COMMANDTIMEOUT=20"))
                {
                    db.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select * from usuarios WHERE usuario = :USUARIO AND password = :PASS", db))
                    {
                        cmd.Parameters.AddWithValue(":USUARIO", usuario);
                        cmd.Parameters.AddWithValue(":PASS", password);
                        NpgsqlDataReader lector = cmd.ExecuteReader();
                        if (lector.Read())
                        {
                            //aqui van los datos de sesion 
                            return Content("1");
                        }
                        else
                        {
                            return Content("Usuario o Contrseña invorrectos ");
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                return Content("Ocurrio un error inesperado: " + ex.Message);
            }
        }
    }
}
