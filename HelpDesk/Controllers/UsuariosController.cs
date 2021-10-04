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
    public class UsuariosController : Controller
    {
        public IActionResult Index()
        {
            List<Usuarios> list = new List<Usuarios>();
            Usuarios clie = null;
            DataTable dataTable = new DataTable();

            try
            {
                using (NpgsqlConnection db = new NpgsqlConnection("HOST=127.0.0.1;Port=5432; User Id=postgres;Password=1nfabc123;Database = dbmmc;TIMEOUT=15;POOLING=True;MINPOOLSIZE=1;MAXPOOLSIZE=20;COMMANDTIMEOUT=20"))
                {
                    db.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select * from usuarios", db))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        using (NpgsqlDataReader dr = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                        {
                            while (dr.Read())
                            {
                                clie = new Usuarios();
                                clie.id = (int)dr["id"];
                                clie.nombre = dr["nombre"].ToString();
                                clie.apellido = dr["apellido"].ToString();
                                clie.usuario = dr["usuario"].ToString();
                                clie.password = dr["password"].ToString();
                                clie.fecha_nacimiento = (DateTime)dr["fecha_nacimiento"];
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
        public ActionResult add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult add(Usuarios model)
        {//vamos a validar un dateanoteishon
            string result = string.Empty;
            if (!ModelState.IsValid)//si esto no es valido
            {
                return View(model);
            }
            try
                {
                using (NpgsqlConnection db = new NpgsqlConnection("HOST=127.0.0.1;Port=5432; User Id=postgres;Password=1nfabc123;Database = dbmmc;TIMEOUT=15;POOLING=True;MINPOOLSIZE=1;MAXPOOLSIZE=20;COMMANDTIMEOUT=20"))
                {
                    db.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand("SP_Add_Users4", db))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new NpgsqlParameter("p_nombre", NpgsqlTypes.NpgsqlDbType.Varchar)).Value = model.nombre;
                        //cmd.Parameters.Add(new NpgsqlParameter("p_apellido", NpgsqlTypes.NpgsqlDbType.Varchar)).Value = model.apellido;
                        //cmd.Parameters.Add(new NpgsqlParameter("p_usuario", NpgsqlTypes.NpgsqlDbType.Varchar)).Value = model.usuario;
                        //cmd.Parameters.Add(new NpgsqlParameter("p_pass", NpgsqlTypes.NpgsqlDbType.Varchar)).Value = model.password;
                        cmd.Parameters.Add(new NpgsqlParameter("p_fecha_nacimiento", NpgsqlTypes.NpgsqlDbType.Date)).Value = model.fecha_nacimiento;
                        cmd.ExecuteNonQuery();
                        //result = Convert.ToString(cmd.Parameters["P_RESULT"].Value);
                        //return View("Index");
                    }
                }
                //p_nombre, , , , 

            }
            catch (Exception ex)
                {
                    return Content("error" + ex.Message);
                }
           return Redirect(Url.Content("~/Usuarios/Index"));
        }

        public IActionResult Edit(int id)
        {
            List<Usuarios> list = new List<Usuarios>();
            try
            {
                using (NpgsqlConnection db = new NpgsqlConnection("HOST=127.0.0.1;Port=5432; User Id=postgres;Password=1nfabc123;Database = dbmmc;TIMEOUT=15;POOLING=True;MINPOOLSIZE=1;MAXPOOLSIZE=20;COMMANDTIMEOUT=20"))
                {
                    db.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand("SP_Update_users", db))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new NpgsqlParameter("P_user_id", NpgsqlTypes.NpgsqlDbType.Integer)).Value = id;
                        cmd.Parameters.Add(new NpgsqlParameter("P_est_id", NpgsqlTypes.NpgsqlDbType.Varchar)).Value = "hola_mundo";
                        cmd.ExecuteNonQuery();
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
