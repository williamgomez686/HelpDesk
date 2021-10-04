using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDesk.Models.VistaParcial
{
    public class ViewTiket
    {
        public int No_Tiket { get; set; }
        public string NombreUsuario { get; set; }
        public string AreaInf { get; set; }
        public string nivUrgencia { get; set; }
        public string Estado { get; set; }
        public string Asunto { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechadeTiket { get; set; }
        public string TelExt { get; set; }
        public string solucion { get; set; }
    }
}
