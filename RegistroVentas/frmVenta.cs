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
using RegistroVentas.Tools;


namespace RegistroVentas
{
    public partial class frmVenta : Form
    {
        Productos productos = new Productos();
        TipoPago tipopago = new TipoPago();
        Boleta boleta = new Boleta();
        ProductosElegidos productosElegidos = new ProductosElegidos();
        EBoleta entBoleta = new EBoleta();
        EProductosElegidos entProductosElegidos = new EProductosElegidos();
        EPago entPago = new EPago();
        Pago pago = new Pago();
        Registro registro = new Registro();
        DataView dv;
        int idproducto;
        bool tipopagoListo = false;
        public frmVenta()
        {
            InitializeComponent();
        }
        private void juegoBotones(bool estado)
        {
            btnAgregarProducto.Enabled = estado;
            numCantidad.Enabled = !estado;
            btnRegistrarProducto.Enabled = !estado;
            btnQuitarRegistro.Enabled = !estado;
        }

        private void frmVenta_Load(object sender, EventArgs e)
        {
            cboTipoPago.DataSource = tipopago.Listar();
            btnExportarPDF.Enabled = false;
            cboTipoPago.Enabled = false;
            cboTipoPago.DisplayMember = "tipopago";
            cboTipoPago.ValueMember = "idtipopago";
            actualizarProductos();
            juegoBotones(true);
            gridProductos.SelectionChanged += GridProductos_SelectionChanged;
            gridRegistro.Enabled = false;
            cboTipoPago.SelectedValue = 0;
            tipopagoListo = true;
        }

        private void GridProductos_SelectionChanged(object sender, EventArgs e)
        {
            if (gridProductos.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = gridProductos.SelectedRows[0];
                // Obtener valores de la fila seleccionada
                idproducto = Convert.ToInt32(selectedRow.Cells["idproducto"].Value);
                MessageBox.Show(Convert.ToString(idproducto));

            }
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
        }

        private void txtBuscar_KeyUp(object sender, KeyEventArgs e)
        {
            string filtro = txtBuscar.Text;

            dv.RowFilter = $"nomproducto LIKE '%{filtro}%'";
            gridProductos.DataSource = dv;
            gridProductos.Refresh();
            gridProductos.ClearSelection();
        }

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            if (gridProductos.SelectedRows.Count > 0)
            {
                juegoBotones(false);
            }
            else
            {
                MessageBox.Show("Eliga un producto primero");
            }
                

        }

        private void juegoBotonesBoleta(bool estado)
        {
            gridRegistro.Enabled = estado;
            btnRegistrar.Enabled = !estado;
            cboTipoPago.Enabled = estado;
            btnExportarPDF.Enabled = estado;
        }

        // ME QUEDE EN LA PARTE QUE YA CUANDO INGRESAS UN NUEVO BOLETA OBTIENES EL ULTIMO ID
        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            

            Random rnd = new Random();
            string rndNum = Convert.ToString(rnd.Next(1000, 9999));
            string codBoleta = $"N{rndNum}";

            if (Aviso.Preguntar("¿Estas seguro de registrar nueva boleta?") == DialogResult.Yes)
            {
                entBoleta.codboleta = codBoleta;

                if(boleta.Registrar(entBoleta) > 0)
                {
                    juegoBotonesBoleta(true);
                    lblCodigo.Text = codBoleta;
                    // Actualiza el DataGridView después de registrar el producto
                    actualizarProductos();
                    int ultimoid = boleta.obtenerUltimoId();
                    MessageBox.Show(Convert.ToString(ultimoid));
                    //COLOCAR UNA BANDERA
                }
            }
            else
            {
                juegoBotonesBoleta(false);
                Aviso.Informar("boleta no creada");
            }            
        }

        private void actualizarRegistros(int idboleta)
        {
            //ME QUEDE ACA
            gridRegistro.DataSource = productosElegidos.Listar(idboleta);
            gridRegistro.Refresh();
            gridRegistro.ClearSelection();
        }

        private void btnRegistrarProducto_Click(object sender, EventArgs e)
        {
            if(numCantidad.Value > 0) {
                if (gridRegistro.Enabled == true)
                {
                    Aviso.Informar("Producto registrado");
                    entProductosElegidos.idboleta = boleta.obtenerUltimoId();
                    entProductosElegidos.idproducto = idproducto;
                    entProductosElegidos.cantidad = (int)numCantidad.Value;


                    int resultadoPE = productosElegidos.Registrar(entProductosElegidos);
                    if (resultadoPE > 0)
                    {
                        actualizarRegistros(boleta.obtenerUltimoId());
                    }


                }
                else
                {
                    Aviso.Advertir("PRODUCTO NO REGISTRADO");

                }
            }
            else
            {
                Aviso.Advertir("Cantidad invalida");
            }
        }

        private void ExportarDatos(string extension)
        {
            SaveFileDialog sd = new SaveFileDialog();
            sd.Title = "Reporte de productos";
            sd.Filter = $"Archivos en formato {extension.ToUpper()}|*.{extension.ToLower()}";

            if (sd.ShowDialog() == DialogResult.OK)
            {
                //Creamos una version del reporet en formato PDF
                //1. instancia del objeto reporte (crystal report)
                Reporte.Boleta reporte = new Reporte.Boleta();

                //2. Asignar los datos al objeto reporte 
                reporte.SetDataSource(registro.Listar(boleta.obtenerUltimoId()));
                reporte.Refresh();

                //3. El reporte creado y en memoria se escribira en el storage
                if (extension.ToUpper() == "PDF")
                {
                    reporte.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, sd.FileName);
                }
                else if (extension.ToUpper() == "XLSX")
                {
                    reporte.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.ExcelWorkbook, sd.FileName);
                }

                //4. Notificar al usuario la creacion del archivo
                Aviso.Informar("Se ha creado el reporte correctamente");

            }
        }

        private void btnGenerarBoleta_Click(object sender, EventArgs e)
        {
            if (tipopagoListo)
            {
                if (cboTipoPago.SelectedValue != null)
                {
                    string tipoPagoSeleccionado = cboTipoPago.SelectedValue.ToString();


                    
                    entPago.idboleta = boleta.obtenerUltimoId();
                    entPago.idtipopago = (int)cboTipoPago.SelectedValue;
                    if (pago.Registrar(entPago) > 0)
                    {
                        ExportarDatos("PDF");
                        this.Refresh();
                    }
                    
                    
                    
                }
                else
                {
                    Aviso.Advertir("Seleccione un tipo de pago");
                }
            }
            else
            {
                Aviso.Advertir("Tipo pago no listo");
            }


        }

        private void btnResetear_Click(object sender, EventArgs e)
        {
            if(Aviso.Preguntar("¿Estas seguro de cancelar esta boleta?") == DialogResult.Yes)
            {
                juegoBotonesBoleta(false);
                gridRegistro.DataSource = null;
                lblCodigo.Text = "";
                this.Refresh();
            }
        }
    }
}
