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
    public class ProductosElegidos
    {
        DBAccess conn = new DBAccess();

        public DataTable Listar(int idboleta)
        {
            return conn.listarDatosVariable("spu_listar_productoselegidos", "@idboleta", idboleta);
        }
        public int Registrar(EProductosElegidos entidad)
        {
            int totalRegistros = 0;
            SqlCommand comando = new SqlCommand("spu_registrar_productoselegidos", conn.getConexion());
            comando.CommandType = CommandType.StoredProcedure;
            conn.abrirConexion();

            try
            {
                comando.Parameters.AddWithValue("@idboleta", entidad.idboleta);
                comando.Parameters.AddWithValue("@idproducto", entidad.idproducto);
                comando.Parameters.AddWithValue("@cantidad", entidad.cantidad);
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
