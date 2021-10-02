using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDesk.Models
{
    public class Estado
    {
        public int Est_Id { get; set; }
        public string Est_Descripcion { get; set; }
        public DateTime Est_FchAlta { get; set; }
        public string Est_UsuAlta { get; set; }
        public string Est_Estado { get; set; }
    }
}
