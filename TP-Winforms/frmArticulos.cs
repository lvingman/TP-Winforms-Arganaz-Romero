using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio;
using negocio;

namespace TP_Winforms
{
    public partial class frmArticulos : Form
    {
        private List<Articulo> listaArticulos;
        public frmArticulos()
        {
            InitializeComponent();
        }



        private void frmArticulos_Load(object sender, EventArgs e)
        {
            cargar();
            ocultarColumnas();
            cboCampo.Items.Add("Precio");
            cboCampo.Items.Add("Nombre");
            cboCampo.Items.Add("Descripcion");
        }

        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvArticulos.CurrentRow != null)
            {
                Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                cargarImagen(seleccionado.URLImagen);
            }
        }

        private void ocultarColumnas()
        {
            dgvArticulos.Columns["URLImagen"].Visible = false;
        }

        private void cargarImagen(string imagen)
        {
            try
            {
                pbxArticulo.Load(imagen);
            }
            catch (Exception ex)
            {
                pbxArticulo.Load("https://upload.wikimedia.org/wikipedia/commons/thumb/3/3f/Placeholder_view_vector.svg/681px-Placeholder_view_vector.svg.png");
            }
        }

        private void cargar()
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
                listaArticulos = negocio.listar();
                dgvArticulos.DataSource = listaArticulos;
                dgvArticulos.Columns["Id"].Visible = false;

                cargarImagen(listaArticulos[0].URLImagen);
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("No hay articulos dados de alta hasta el momento", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        private void bntAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                frmAltaArticulo alta = new frmAltaArticulo();
                alta.ShowDialog();
                cargar();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
          
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                Articulo seleccionado;
                seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                frmAltaArticulo modificar = new frmAltaArticulo(seleccionado);
                modificar.ShowDialog();
                cargar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
          
        }

        private void btnFiltro_Click(object sender, EventArgs e)
        {
            try
            {
                List<Articulo> listaFiltrada;
                string filtro = txtFiltro.Text;

                dgvArticulos.DataSource = null;

                if (filtro != "")
                {
                    listaFiltrada = listaArticulos.FindAll(x => x.Nombre.ToUpper().Contains(filtro.ToUpper()) || x.Descripcion.ToUpper().Contains(filtro.ToUpper()));
                }
                else
                {

                    listaFiltrada = listaArticulos;

                }

                dgvArticulos.DataSource = listaFiltrada;
                ocultarColumnas();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            }

        private void cboCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string opcion = cboCampo.SelectedItem.ToString();
            if (opcion == "Precio")
            {
                cboCriterio.Items.Clear();
                cboCriterio.Items.Add("Mayor a");
                cboCriterio.Items.Add("Menor a");
                cboCriterio.Items.Add("Igual a");
            }
            else
            {
                cboCriterio.Items.Clear();
                cboCriterio.Items.Add("Comienza con");
                cboCriterio.Items.Add("Termina con");
                cboCriterio.Items.Add("Contiene");
            }
        }
       private bool validarFiltro()
        {           
                if(cboCampo.SelectedIndex < 0)
                {
                    MessageBox.Show("Por favor seleccione el campo para filtrar");
                    return true;
                }
                if (cboCriterio.SelectedIndex < 0)
                {
                    MessageBox.Show("Por favor seleccione un criterio para filtrar");
                    return true;
                }
                if (cboCampo.SelectedItem.ToString() == "Precio")
                {
                if (string.IsNullOrEmpty(txtFiltroAvanzado.Text))
                {
                    MessageBox.Show("Uups te olvidaste de cargar un precio...");
                    return true;
                }
                    if (!(soloNumeros(txtFiltroAvanzado.Text)))
                    {
                        MessageBox.Show("Solo numeros para filtrar por un campo númerico...");
                        return true;
                    }                   
                }
                return false;                     
        }

        private bool soloNumeros(string cadena)
        {
            foreach(char caracter in cadena)
            {
                if (!(char.IsNumber(caracter)))               
                    return false;                
            }
            return true;
        }

        private void btnFiltroAvanzado_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
                if (validarFiltro())
                return;

                string campo = cboCampo.SelectedItem.ToString();
                string criterio = cboCriterio.SelectedItem.ToString();
                string filtro = txtFiltroAvanzado.Text;
                dgvArticulos.DataSource = negocio.filtrar(campo, criterio, filtro);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString()) ;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio() ;
            Articulo seleccionado;
            try
            {
                DialogResult respuesta= MessageBox.Show("¿Estás seguro que quieres eliminar este registro?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (respuesta == DialogResult.Yes) {
                seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                negocio.eliminar(seleccionado.Id);             
                cargar();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void btnDetalle_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvArticulos.CurrentRow is null)
                {
                    MessageBox.Show("Seleccione un articulo por favor...");
                }
                else
                {
                    Articulo articuloSeleccionado;
                    articuloSeleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;

                    frmDetalles detalle = new frmDetalles(articuloSeleccionado);


                    detalle.ShowDialog();
                }
            }  
            catch (Exception ex)
            {

                throw ex;
            }
          
            }
        }
    }

