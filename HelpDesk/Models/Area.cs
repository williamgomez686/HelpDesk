using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDesk.Models
{
    public class Area
    {
        public int AreaProblemaId { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }
        public DateTime Est_Fecha { get; set; }
        public string Est_UsuarioAlta { get; set; }
    }
}
