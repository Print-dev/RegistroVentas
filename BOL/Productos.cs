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

    public class Productos
    {
        DBAccess conn = new DBAccess();
        public DataTable Listar()
        {
            return conn.Listar("spu_listar_productos");
        }

        public int Registrar(EProducto entidad)
        {
            int totalRegistros = 0;
            SqlCommand comando = new SqlCommand("spu_registrar_producto", conn.getConexion());
            comando.CommandType = CommandType.StoredProcedure;
            conn.abrirConexion();

            try
            {
                comando.Parameters.AddWithValue("@idcategoria", entidad.idcategoria);
                comando.Parameters.AddWithValue("@nomproducto", entidad.nomproducto);
                comando.Parameters.AddWithValue("@cantidad", entidad.cantidad);
                comando.Parameters.AddWithValue("@precio", entidad.precio);

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

        public int Actualizar(EProducto entidad)
        {
            int totalRegistros = 0;
            SqlCommand comando = new SqlCommand("spu_actualizar_producto", conn.getConexion());
            comando.CommandType = CommandType.StoredProcedure;
            conn.abrirConexion();

            try
            {
                comando.Parameters.AddWithValue("@idproducto", entidad.idproducto);
                comando.Parameters.AddWithValue("@idcategoria", entidad.idcategoria);
                comando.Parameters.AddWithValue("@nomproducto", entidad.nomproducto);
                comando.Parameters.AddWithValue("@cantidad", entidad.cantidad);
                comando.Parameters.AddWithValue("@precio", entidad.precio);

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

        //public int Inhabilitar(EProducto entidad)
        //{
        //    int prodInhabilitado = 0;
        //    SqlCommand comando = new SqlCommand("spu_inhabilitar_producto", conn.getConexion());
        //    comando.CommandType = CommandType.StoredProcedure;
        //    conn.abrirConexion();

        //    try
        //    {
        //        comando.Parameters.AddWithValue("@idcategoria", entidad.idcategoria);
        //        prodInhabilitado = comando.ExecuteNonQuery();
        //    }
        //    catch
        //    {
        //        prodInhabilitado = -1;
        //    }
        //    finally
        //    {
        //        conn.cerrarConexion();
        //    }
        //    return prodInhabilitado; 
        //}
    }
}
