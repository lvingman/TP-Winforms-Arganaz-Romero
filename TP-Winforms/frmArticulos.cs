using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TP_Winforms.dominio;
using TP_Winforms.negocio;

namespace TP_Winforms
{
    public partial class frmArticulos : Form
    {
        public frmArticulos()
        {
            InitializeComponent();
        }



        private void frmArticulos_Load(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            dgvArticulos.DataSource = negocio.listar();
        }
    }
}
