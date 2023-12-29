using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL;
using System.Data;
using System.Data.SqlClient;
using ENTITIES;

namespace BOL
{
    public class TipoPago
    {
        DBAccess conn = new DBAccess();
        public DataTable Listar()
        {
            return conn.Listar("spu_listar_tipopago");
        }
    }
}
