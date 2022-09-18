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
    public partial class frmAltaArticulo : Form
    {
        private Articulo articulo = null;
        private void frmAltaArticulo_Load(object sender, EventArgs e)
        {
            CategoriaNegocio catNegocio = new CategoriaNegocio();
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            try
            {
                cboCategoria.DataSource = catNegocio.listar();
                cboCategoria.ValueMember = "ID";
                cboCategoria.DisplayMember = "Descripcion";
                cboMarca.DataSource = marcaNegocio.listar();
                cboMarca.ValueMember = "ID";
                cboMarca.DisplayMember = "Descripcion";

                if (articulo != null)
                {
                    txtCodigo.Text = articulo.Codigo;
                    txtNombre.Text = articulo.Nombre;
                    txtDescripcion.Text = articulo.Descripcion;
                    txtImagen.Text = articulo.URLImagen;
                    txtPrecio.Text = articulo.Precio.ToString();
                    cboCategoria.SelectedValue = articulo.Categoria.ID;
                    cboMarca.SelectedValue = articulo.Marca.ID;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
       
        public frmAltaArticulo()
        {
            InitializeComponent();
            Text = "Nuevo Articulo";
        }
        public frmAltaArticulo(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
            Text = "Modificar Articulo";
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private bool soloNumeros(string cadena)
        {
            foreach (char caracter in cadena)
            {
                if (!(char.IsNumber(caracter)))
                    return false;
            }
            return true;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {       
            ArticuloNegocio negocio = new ArticuloNegocio();


            bool validar = false;

            if (String.IsNullOrEmpty(txtCodigo.Text))
            {
                txtCodigo.BackColor = Color.LightCoral;
                validar = true;
            }
            else
                txtCodigo.BackColor = Color.White;
            if (String.IsNullOrEmpty(txtNombre.Text))
            {
                txtNombre.BackColor = Color.LightCoral;
                validar = true;
            }
            else
                txtNombre.BackColor = Color.White;
            if (String.IsNullOrEmpty(txtDescripcion.Text))
            {
                txtDescripcion.BackColor = Color.LightCoral;
                validar = true;
            }
            else
                txtDescripcion.BackColor = Color.White;
            
            if(soloNumeros(txtPrecio.Text) == false) {                           
                txtPrecio.Text = string.Empty;
                txtPrecio.BackColor = Color.LightCoral;
                validar = true;
            }
            else
                txtPrecio.BackColor = Color.White;
            



            if (String.IsNullOrEmpty(txtPrecio.Text))
            {
                txtPrecio.BackColor = Color.LightCoral;
                validar = true;
            }
            else
                txtPrecio.BackColor = Color.White;

            if (validar == false)
            {
                try
            {
                if (articulo == null)
                    articulo = new Articulo();

                articulo.Codigo = txtCodigo.Text;
                articulo.Nombre = txtNombre.Text;
                articulo.Descripcion = txtDescripcion.Text;
                articulo.Marca = (Marca)cboMarca.SelectedItem;
                articulo.Categoria = (Categoria)cboCategoria.SelectedItem;
                articulo.URLImagen = txtImagen.Text;
                articulo.Precio = Decimal.Parse(txtPrecio.Text);
            


               

                if (articulo.Id != 0)
                {                  
                    negocio.modificar(articulo);
                    MessageBox.Show("Modificado Exitosamente");
                }
                else
                {
                    negocio.agregar(articulo);
                    MessageBox.Show("Agregaste Exitosamente");
                }
                

                Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            }
        }

    }
}
