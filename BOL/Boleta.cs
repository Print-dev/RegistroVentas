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

    public class Boleta
    {
        DBAccess conn = new DBAccess();

        public int Registrar(EBoleta entidad)
        {
            int totalRegistros = 0;
            SqlCommand comando = new SqlCommand("spu_registrar_boleta", conn.getConexion());
            comando.CommandType = CommandType.StoredProcedure;
            conn.abrirConexion();

            try
            {
                comando.Parameters.AddWithValue("@codboleta", entidad.codboleta);
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

        public int obtenerUltimoId()
        {
            conn.abrirConexion();
            SqlCommand comando = new SqlCommand("select top 1 * from boleta order by idboleta desc", conn.getConexion());
            int ultimoId = Convert.ToInt32(comando.ExecuteScalar());
            return ultimoId;
        }
    }
}
