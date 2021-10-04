using HelpDesk.Models;
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
        public void Eliminar_sql(int id)
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

        public void Agrear_Ticket(Tiket model)
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
