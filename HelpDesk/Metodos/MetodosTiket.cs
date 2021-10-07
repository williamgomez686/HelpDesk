using HelpDesk.Models;
using HelpDesk.Models.VistaParcial;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDesk.Metodos
{
    public class MetodosTiket
    {
        public string CadenaConexion = "HOST=192.168.1.136;Port=5432; User Id=postgres;Password=1nfabc123;Database = postgres;TIMEOUT=15;POOLING=True;MINPOOLSIZE=1;MAXPOOLSIZE=20;COMMANDTIMEOUT=20";
        public void Eliminar_sql(int id)/// este metodo hace un borrado logico ya que solo cabia el estatus a eliminado si borrar el registro de la base de datos

        {
            using (NpgsqlConnection db = new NpgsqlConnection(CadenaConexion))
            {
                db.Open();
                var cadena = @"UPDATE tickes
				                    SET est_id=:estado
			                    WHERE tk_id=:id;";

                using (NpgsqlCommand cmd = new NpgsqlCommand(cadena, db))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue(":id", id);
                    cmd.Parameters.AddWithValue(":estado", 4);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public ViewTiket Listar_Tickets(List<ViewTiket> list, ViewTiket oTiket)
        {
            using (NpgsqlConnection db = new NpgsqlConnection(CadenaConexion))
            {
                string cadena = @"select tk_id as No_Tiket, u.usu_nick as Usuario, u2.urg_desc as Nivel_Urgencia, t.tk_asu as Asunto, t.tk_desc as Descripcion, tk_fchalt as Fecha
	                            from tickes t 
		                            inner join usuarios u 
			                            on t.usu_id = u.usu_id 
		                            inner join urgencia u2 
			                            on t.urg_id = u2.urg_id
                               
                                order by t.tk_fchalt";

                                // where est_id = 1
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
                            oTiket.NombreUsuario = dr["Usuario"].ToString();
                            oTiket.nivUrgencia = dr["Nivel_Urgencia"].ToString();
                            oTiket.Asunto = dr["Asunto"].ToString();
                            oTiket.Descripcion = dr["Descripcion"].ToString();
                            oTiket.FechadeTiket = (DateTime)dr["Fecha"];

                            list.Add(oTiket);
                        }
                    }
                }
            }
            return oTiket;
        }
        public ViewTiket Detalles_Tiket(int id, List<ViewTiket> list, ViewTiket oTiket)/// Muestra detalles del Ticket lo manda a llamar el controlador TicketController en el metodo Details
        {
            using (NpgsqlConnection db = new NpgsqlConnection(CadenaConexion))
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
                            //ViewBag.NoTiket = oTiket.No_Tiket;
                            list.Add(oTiket);
                        }
                    }
                }
            }

            return oTiket;
        }

        public void Agrear_Ticket(Tiket model)//este metodo hace un Update a la base de datos y lo llama el TicketController en el metodo Add
        {
            using (NpgsqlConnection db = new NpgsqlConnection(CadenaConexion))
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
    }
}
