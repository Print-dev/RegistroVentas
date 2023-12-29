using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL;
using System.Data;
using System.Data.SqlClient;

namespace BOL
{
    public class Registro
    {
        DBAccess conn = new DBAccess();
    
        public DataTable Listar(int idboleta)
        {
            return conn.listarDatosVariable("spu_listar_registros","@idboleta", idboleta);
        }
    }
}
