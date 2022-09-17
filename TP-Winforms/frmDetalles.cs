﻿using System;
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
    public partial class frmDetalles : Form
    {
        private Articulo articulo = null;


        public frmDetalles(Articulo articulo) 
        {
            InitializeComponent();
            this.articulo = articulo;

            txtCodigo.ReadOnly = true;
            txtNombre.ReadOnly = true;
            txtDescripcion.ReadOnly = true;
            txtMarca.ReadOnly = true;
            txtCategoria.ReadOnly = true;
            txtPrecio.ReadOnly = true;

        }

        private void frmDetalles_Load(object sender, EventArgs e)
        {
            txtCodigo.Text = articulo.Codigo;
            txtNombre.Text = articulo.Nombre;
            txtDescripcion.Text = articulo.Descripcion;
            txtMarca.Text = articulo.Marca.Descripcion;
            txtCategoria.Text = articulo.Categoria.Descripcion;
            txtPrecio.Text = articulo.Precio.ToString();
            cargarImagen(articulo.URLImagen);
        }

        private void cargarImagen(string imagen)
        {
            try
            {
                pbxArticulo.Load(imagen);
            }
            catch (Exception ex)
            {
                pbxArticulo.Load("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQXJq6u65-ZDLDMCQMHejY3TGV5Vbj-O343pyR1KoVE8lvmTet4TG319R9tPMgSgxKFgjY&usqp=CAU");
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
