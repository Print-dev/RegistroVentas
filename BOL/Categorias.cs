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
    public class Categorias
    {
        DBAccess conn = new DBAccess();
        

        public List<ECategoria> Listar()
        {
            conn.abrirConexion();
            SqlCommand comando = new SqlCommand("spu_listar_categorias", conn.getConexion());
            comando.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = comando.ExecuteReader();
            List<ECategoria> listaCategorias = new List<ECategoria>();
            while (reader.Read())
            {
                // Crear una instancia de Categoria y llenarla con los datos del reader
                ECategoria categoria = new ECategoria
                {
                    // Supongamos que tienes una propiedad llamada IdCategoria y otra llamada Nombre
                    idcategoria = Convert.ToInt32(reader["idcategoria"]),
                    categoria = reader["categoria"].ToString()
                };

                // Agregar la categoría a la lista
                listaCategorias.Add(categoria);
            }
            // Cerrar la conexión después de obtener los datos
            conn.cerrarConexion();

            // Retornar la lista de categorías
            return listaCategorias;

        }
    }
}
