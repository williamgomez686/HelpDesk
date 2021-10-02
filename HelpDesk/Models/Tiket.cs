using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDesk.Models
{
    public class Tiket
    {
        public int Tk_Id { get; set; }
        public int Usu_Id { get; set; }
        public int Ar_Pro_Id { get; set; }
        public int Urg_Id { get; set; }
        public int Est_Id { get; set; }
        public string Tk_Asu { get; set; }
        public string Tk_Desc { get; set; }
        public DateTime Tk_FchAlt { get; set; }
        public string Tk_TelCom { get; set; }
        public string Tk_Sol { get; set; }
        public DateTime ?Tk_FchSol { get; set; }
        public DateTime ?Tk_FchMod { get; set; }
        public string Tk_UsuTec { get; set; }
    }
}