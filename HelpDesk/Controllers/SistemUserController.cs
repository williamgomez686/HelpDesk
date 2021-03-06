using HelpDesk.Models;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDesk.Controllers
{
    public class SistemUserController : Controller
    {
        public IActionResult Index()
        {
            List<SistemUser> list = new List<SistemUser>();
            SistemUser clie = null;
            DataTable dataTable = new DataTable();

            try
            {
                using (NpgsqlConnection db = new NpgsqlConnection("HOST=192.168.1.136;Port=5432; User Id=postgres;Password=1nfabc123;Database = postgres;TIMEOUT=15;POOLING=True;MINPOOLSIZE=1;MAXPOOLSIZE=20;COMMANDTIMEOUT=20"))
                {
                    db.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand(@"select * from usuarios", db))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        using (NpgsqlDataReader dr = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                        {
                            while (dr.Read())
                            {
                                clie = new SistemUser();
                                clie.Usuarioid = (int)dr["usu_id"];
                                clie.Empleadoid = (int)dr["emplab_id"];
                                clie.Nikname = dr["usu_nick"].ToString();
                                clie.Password = dr["usu_pass"].ToString();
                                clie.NivelUsuario = (int)dr["usu_niv"];
                                clie.estado = dr["usu_est"].ToString();
                                list.Add(clie);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return Content("error" + ex.Message);
            }
            return View(list);
        }
    }
}
