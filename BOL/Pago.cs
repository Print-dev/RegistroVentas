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
    public class Pago
    {
        DBAccess conn = new DBAccess();
        public int Registrar(EPago entidad)
        {
            int totalRegistros = 0;
            SqlCommand comando = new SqlCommand("spu_registrar_pago", conn.getConexion());
            comando.CommandType = CommandType.StoredProcedure;
            conn.abrirConexion();

            try
            {
                comando.Parameters.AddWithValue("@idboleta", entidad.idboleta);
                comando.Parameters.AddWithValue("@idtipopago", entidad.idtipopago);

                //Executenonquery solo ejecuta y devuelve 1 si es true y 0 si es false
                totalRegistros = comando.ExecuteNonQuery();
            }
            catch
            {
                totalRegistros = -1;
            }
            finally
            {
                conn.cerrarConexion();
            }
            return totalRegistros;
        }
    }
}
