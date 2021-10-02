using HelpDesk.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDesk.Controllers
{
    public class TiketController : Controller
    {
        // GET: TiketController
        public ActionResult Index()
        {
            List<Tiket> list = new List<Tiket>();
            Tiket oTiket = null;
            DataTable dataTable = new DataTable();

            try
            {
                using (NpgsqlConnection db = new NpgsqlConnection("HOST=192.168.1.136;Port=5432; User Id=postgres;Password=1nfabc123;Database = postgres;TIMEOUT=15;POOLING=True;MINPOOLSIZE=1;MAXPOOLSIZE=20;COMMANDTIMEOUT=20"))
                {
                    db.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select * from tickes", db))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        using (NpgsqlDataReader dr = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                        {
                            while (dr.Read())
                            {
                                oTiket = new Tiket();
                                oTiket.Tk_Id = (int)dr["Tk_Id"];
                                oTiket.Usu_Id = (int)dr["Usu_Id"];
                                oTiket.Ar_Pro_Id = (int)dr["Ar_Pro_Id"];
                                oTiket.Urg_Id = (int)dr["Urg_Id"];
                                oTiket.Est_Id = (int)dr["Est_Id"];
                                oTiket.Tk_Asu = dr["Tk_Asu"].ToString();
                                oTiket.Tk_Desc = dr["Tk_Desc"].ToString();
                                oTiket.Tk_FchAlt = (DateTime)dr["Tk_FchAlt"];
                                oTiket.Tk_TelCom = dr["Tk_TelCom"].ToString();
                                oTiket.Tk_Sol = dr["Tk_Sol"].ToString();
                                //oTiket.Tk_FchSol = (DateTime)dr["Tk_FchSol"];
                               // oTiket.Tk_FchMod = (DateTime)dr["Tk_FchMod"];
                                oTiket.Tk_UsuTec = dr["Tk_UsuTec"].ToString();
                                list.Add(oTiket);
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

        // GET: TiketController/Details/5
        [HttpGet]
        public ActionResult Details(int id)
        {
            List<Tiket> list = new List<Tiket>();
            Tiket oTiket = null;
            DataTable dataTable = new DataTable();

            try
            {
                using (NpgsqlConnection db = new NpgsqlConnection("HOST=192.168.1.136;Port=5432; User Id=postgres;Password=1nfabc123;Database = postgres;TIMEOUT=15;POOLING=True;MINPOOLSIZE=1;MAXPOOLSIZE=20;COMMANDTIMEOUT=20"))
                {
                    db.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select * from tickes where tk_id =" + id , db))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        using (NpgsqlDataReader dr = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                        {
                            while (dr.Read())
                            {
                                oTiket = new Tiket();
                                oTiket.Tk_Id = (int)dr["tk_id"];
                                oTiket.Usu_Id = (int)dr["usu_id"];
                                oTiket.Ar_Pro_Id = (int)dr["ar_pro_id"];
                                oTiket.Urg_Id = (int)dr["urg_id"];
                                oTiket.Est_Id = (int)dr["est_id"];
                                oTiket.Tk_Asu = dr["tk_asu"].ToString();
                                oTiket.Tk_Desc = dr["tk_desc"].ToString();
                                oTiket.Tk_FchAlt = (DateTime)dr["tk_fchalt"];
                                oTiket.Tk_TelCom = dr["tk_telcom"].ToString();
                                oTiket.Tk_Sol = dr["tk_sol"].ToString();
                                //oTiket.Tk_FchSol = (DateTime)dr["Tk_FchSol"];
                                // oTiket.Tk_FchMod = (DateTime)dr["Tk_FchMod"];
                                //oTiket.Tk_UsuTec = dr["Tk_UsuTec"].ToString();
                                list.Add(oTiket);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return Content("error" + ex.Message);
            }

            //var detail = from d in list
            //             where d.Tk_Id == id
            //             select d;
            return View(list.ToList());

        }

        // GET: TiketController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TiketController/Create
        [HttpPost]
       // [ValidateAntiForgeryToken]
        public ActionResult Create(Tiket model)
        {
            string result = string.Empty;
            if (!ModelState.IsValid)//si esto no es valido
            {
                return View(model);
            }
            try
            {
                using (NpgsqlConnection db = new NpgsqlConnection("HOST=192.168.1.136;Port=5432; User Id=postgres;Password=1nfabc123;Database = postgres;TIMEOUT=15;POOLING=True;MINPOOLSIZE=1;MAXPOOLSIZE=20;COMMANDTIMEOUT=20"))
                {
                    db.Open();
                    var cadena = @"INSERT INTO tickes
                                    (usu_id, ar_pro_id, urg_id, est_id, tk_asu, tk_desc, tk_fchalt, tk_telcom)
                                        VALUES(:Usu_Id, :Ar_Pro_Id, :Urg_Id, :Est_Id, :Tk_Asu, :Tk_Desc, :Tk_FchAlt, :Tk_TelCom);";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(cadena, db))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.AddWithValue(":Usu_Id", model.Usu_Id);
                        cmd.Parameters.AddWithValue(":Ar_Pro_Id", model.Ar_Pro_Id);
                        cmd.Parameters.AddWithValue(":Urg_Id", model.Urg_Id);
                        cmd.Parameters.AddWithValue(":Est_Id", 1);
                        cmd.Parameters.AddWithValue(":Tk_Asu", model.Tk_Asu);
                        cmd.Parameters.AddWithValue(":Tk_Desc", model.Tk_Desc);
                        cmd.Parameters.AddWithValue(":Tk_FchAlt", DateTime.Now);
                        cmd.Parameters.AddWithValue(":Tk_TelCom", model.Tk_TelCom);
                        cmd.ExecuteNonQuery();
                    }
                    //using (NpgsqlCommand cmd = new NpgsqlCommand("SP_Add_tiket", db))
                    //{
                    //    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    //    cmd.Parameters.Add(new NpgsqlParameter("p_Usu_Id", NpgsqlTypes.NpgsqlDbType.Integer)).Value = model.Usu_Id;                    
                    //    cmd.Parameters.Add(new NpgsqlParameter("p_Ar_Pro_Id", NpgsqlTypes.NpgsqlDbType.Integer)).Value = model.Ar_Pro_Id;
                    //    cmd.Parameters.Add(new NpgsqlParameter("p_Urg_Id", NpgsqlTypes.NpgsqlDbType.Integer)).Value = model.Urg_Id;
                    //    cmd.Parameters.Add(new NpgsqlParameter("p_Est_Id", NpgsqlTypes.NpgsqlDbType.Integer)).Value = model.Tk_Asu;
                    //    cmd.Parameters.Add(new NpgsqlParameter("p_Tk_Asu", NpgsqlTypes.NpgsqlDbType.Varchar)).Value =  model.Tk_Asu;
                    //    cmd.Parameters.Add(new NpgsqlParameter("p_Tk_Desc", NpgsqlTypes.NpgsqlDbType.Varchar)).Value = model.Tk_Desc;
                    //    cmd.Parameters.Add(new NpgsqlParameter("p_Tk_FchAlt", NpgsqlTypes.NpgsqlDbType.Date)).Value = model.Tk_FchAlt;
                    //    cmd.Parameters.Add(new NpgsqlParameter("p_Tk_TelCom", NpgsqlTypes.NpgsqlDbType.Varchar)).Value = model.Tk_TelCom;
                    //    cmd.ExecuteNonQuery();
                    //    //result = Convert.ToString(cmd.Parameters["P_RESULT"].Value);
                    //    //return View("Index");
                    //}
                } 

            }
            catch (Exception ex)
            {
                return Content("Error en el Prosedimiento Almacenado: " + ex.Message);
            }
            return Redirect(Url.Content("~/Tiket/Index"));
        }

        // GET: TiketController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TiketController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TiketController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TiketController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
