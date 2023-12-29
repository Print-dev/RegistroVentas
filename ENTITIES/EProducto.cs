using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITIES
{
    public class EProducto
    {
        public  int     idproducto { get; set; }
        public  int     idcategoria { get; set; }
        public  string  nomproducto { get; set; }
        public  int     cantidad { get; set; }
        public  double precio { get; set; }
        public  string  created_at { get; set; }

        public  string inactive_at { get; set; }
    }
}
