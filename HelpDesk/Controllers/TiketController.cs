using HelpDesk.Models;
using HelpDesk.Models.VistaParcial;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using HelpDesk.Metodos;

namespace HelpDesk.Controllers
{
    public class TiketController : Controller 
    {
        // GET: TiketController
        public ActionResult Index()
        {
            List<ViewTiket> list = new List<ViewTiket>();
            ViewTiket oTiket = null;

            try
            {
                MetodosTiket listar = new MetodosTiket();
                oTiket = listar.Listar_Tickets(list, oTiket);
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
            List<ViewTiket> list = new List<ViewTiket>();
            ViewTiket oTiket = null;

            try
            {
                MetodosTiket Detalles = new MetodosTiket();
                oTiket = Detalles.Detalles_Tiket(id, list, oTiket);
                //oTiket = Detalles_Tiket(id, list, oTiket);
                ViewBag.NoTiket = oTiket.No_Tiket;
            }
            catch (Exception ex)
            {
                return Content("Error " + ex.Message);
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
                MetodosTiket Agregar = new MetodosTiket();
                Agregar.Agrear_Ticket(model);

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
        [HttpGet]
        public ActionResult Delete(int id)
        {
            List<ViewTiket> list = new List<ViewTiket>();
            try
            {
                Muesta_paraEliminar(id, list);
            }
            catch (Exception ex)
            {
                return Content("Error " + ex.Message);
            }
            //Eliminar_sql(oTiket.No_Tiket); ;
            return View(list.ToList());
        }

        private void Muesta_paraEliminar(int id, List<ViewTiket> list)
        {
            ViewTiket oTiket = null;
            using (NpgsqlConnection db = new NpgsqlConnection("HOST=192.168.1.136;Port=5432; User Id=postgres;Password=1nfabc123;Database = postgres;TIMEOUT=15;POOLING=True;MINPOOLSIZE=1;MAXPOOLSIZE=20;COMMANDTIMEOUT=20"))
            {
                string cadena = @"select tk_id as No_Tiket,
	                                        u.usu_nick as Usuario,
	                                        a.ar_desc as Area_informatica,
		                                    u2.urg_desc as Nivel_Urgencia,
		                                    e.est_desc  as estado,
		                                    t.tk_asu as Asunto, 
		                                    t.tk_desc as Descripcion, 
		                                    tk_fchalt as Fecha, 
		                                    t.tk_telcom as TelExt,
		                                    t.tk_sol as Solucion
	                                    from tickes t 
		                                    inner join usuarios u on t.usu_id = u.usu_id 
		                                    inner join urgencia u2 on t.urg_id = u2.urg_id
		                                    inner join area_problemas ap on t.ar_pro_id = ap.arpro_id 
	 	                                    inner join area a on ap.ar_id = a.ar_id
	 	                                    inner join estado e on t.est_id = e.est_id 
                                    where t.tk_id =" + id;
                db.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(cadena, db))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    using (NpgsqlDataReader dr = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                    {
                        while (dr.Read())
                        {
                            oTiket = new ViewTiket();
                            oTiket.No_Tiket = (int)dr["No_Tiket"];
                            oTiket.NombreUsuario = (dr)["Usuario"].ToString();
                            oTiket.AreaInf = dr["Area_informatica"].ToString();
                            oTiket.nivUrgencia = dr["Nivel_Urgencia"].ToString();
                            oTiket.Estado = dr["estado"].ToString();
                            oTiket.Asunto = dr["Asunto"].ToString();
                            oTiket.Descripcion = dr["Descripcion"].ToString();
                            oTiket.FechadeTiket = (DateTime)dr["Fecha"];
                            oTiket.TelExt = dr["TelExt"].ToString();
                            oTiket.solucion = dr["Solucion"].ToString();
                            ViewBag.NoTiket = oTiket.No_Tiket;
                            list.Add(oTiket);
                        }
                    }
                }
            }
        }

        // POST: TiketController/Delete/5
        [HttpGet]

        public ActionResult Delete_confirm(int id)
        {
            try
            {
                MetodosTiket Eliminar = new MetodosTiket();
                Eliminar.Eliminar_sql(id);
            }
            catch
            {
                return View("Index");
            }
            return Redirect(Url.Content("~/Tiket/Index"));
        }

        //private static void Metodo_Eliminar(int id)
        //{
        //    using (NpgsqlConnection db = new NpgsqlConnection("HOST=127.0.0.1;Port=5432; User Id=postgres;Password=1nfabc123;Database = dbmmc;TIMEOUT=15;POOLING=True;MINPOOLSIZE=1;MAXPOOLSIZE=20;COMMANDTIMEOUT=20"))
        //    {
        //        db.Open();
        //        using (NpgsqlCommand cmd = new NpgsqlCommand("SP_Update_stat2", db))
        //        {
        //            cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //            cmd.Parameters.Add(new NpgsqlParameter("P_tk_id", id ));
        //            cmd.Parameters.Add(new NpgsqlParameter("P_est_id", 4));
        //            cmd.ExecuteNonQuery();
        //        }
        //    }
        //}
    }
}
