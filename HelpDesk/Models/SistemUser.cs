using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDesk.Models
{
    public class SistemUser
    {
        public int Usuarioid { get; set; }
        public int Empleadoid { get; set; }
        public string  Nikname { get; set; }
        public string Password { get; set; }
        public int NivelUsuario { get; set; }
        public string estado { get; set; }

    }
}
