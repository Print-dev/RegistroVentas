using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using BOL;
using ENTITIES;
namespace RegistroVentas
{
    public partial class frmProducto : Form
    {
        Productos productos = new Productos();
        Categorias categoria = new Categorias();
        EProducto entProducto = new EProducto();

        DataView dv;
        public frmProducto()
        {
            InitializeComponent();
        }

        private void frmProducto_Load(object sender, EventArgs e)
        {
            Categorias categorias = new Categorias();
            
            cboCategoria.DataSource = categoria.Listar();
            cboCategoria.DisplayMember = "categoria";
            cboCategoria.ValueMember = "idcategoria";
            actualizarProductos();
            juegoBotones(true);
            gridProductos.SelectionChanged += GridProductos_SelectionChanged;
            
            

        }
        private void GridProductos_SelectionChanged(object sender, EventArgs e)
        {
            if (gridProductos.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = gridProductos.SelectedRows[0];
                // Obtener valores de la fila seleccionada
                string nomProducto = Convert.ToString(selectedRow.Cells["nomproducto"].Value);
                int cantidad = Convert.ToInt32(selectedRow.Cells["cantidad"].Value);
                double precio = Convert.ToDouble(selectedRow.Cells["precio"].Value);

                // Asignar valores a los controles
                cboCategoria.Text = "Seleccione categoria";
                txtProducto.Text = nomProducto;
                numCantidad.Value = cantidad;
                numPrecio.Value = Convert.ToDecimal(precio);

                juegoBotones(false);

            }
        }

        private void juegoBotones(bool estado) // true
        {
                btnAgregar.Enabled = estado; //true
                btnEditar.Enabled = !estado; // false
                btnCancelar.Enabled = !estado; //false
        }

        private void actualizarProductos()
        {
            dv = new DataView(productos.Listar());

            gridProductos.DataSource = dv;
            gridProductos.Refresh();

            gridProductos.Columns[0].Visible = false;

            gridProductos.Columns[1].Visible = false;
            gridProductos.Columns[2].Width = 110;
            gridProductos.Columns[3].Width = 110;
            gridProductos.Columns[4].Width = 110;
            gridProductos.ClearSelection();
            juegoBotones(true);
        }

        private void txtBuscar_KeyUp(object sender, KeyEventArgs e)
        {
            string filtro = txtBuscar.Text;

            dv.RowFilter = $"nomproducto LIKE '%{filtro}%'";
            gridProductos.DataSource = dv;
            gridProductos.Refresh();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            int idCategoria = ((ECategoria)cboCategoria.SelectedItem).idcategoria;


            entProducto.idcategoria = idCategoria;
            entProducto.nomproducto = txtProducto.Text;
            entProducto.cantidad = (int)numCantidad.Value;
            entProducto.precio = (double)numPrecio.Value;
            
            int resultado = productos.Registrar(entProducto);
            if (!string.IsNullOrEmpty(txtProducto.Text.Trim()))
            {
                if (resultado > 0)
                {
                    MessageBox.Show("Producto registrado correctamente");

                    // Actualiza el DataGridView después de registrar el producto
                    actualizarProductos();
                }
                else
                {
                    MessageBox.Show("Error al registrar el producto");
                }
            }
            else
            {
                {
                    MessageBox.Show("ingrese nombre al producto producto");
                }
            }
                
            
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            DataGridViewRow selectedRow = gridProductos.SelectedRows[0];
            // Obtener valores de la fila seleccionada
            int idProducto = Convert.ToInt32(selectedRow.Cells["idproducto"].Value);
            EProducto productoActualizar = new EProducto
            {
                idproducto = idProducto,
                idcategoria = ((ECategoria)cboCategoria.SelectedItem).idcategoria,
                nomproducto = txtProducto.Text,
                cantidad = (int)numCantidad.Value,
                precio = (double)numPrecio.Value
            };

            int resultado = productos.Actualizar(productoActualizar);
            if (resultado > 0)
            {
                MessageBox.Show("Producto actualizado correctamente");

                // Actualiza el DataGridView después de registrar el producto
                actualizarProductos();
            }
            else
            {
                MessageBox.Show("Error al actualizar el producto");
            }
            // Desactivar botones de agregar y activar botones de editar/cancelar
            juegoBotones(true);

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            juegoBotones(true);
            gridProductos.ClearSelection();
        }

        private void btnRegistrarCompra_Click(object sender, EventArgs e)
        {
            frmVenta vntVenta = new frmVenta();
            vntVenta.Show();
            this.Hide();
        }
    }
}
