using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Winforms.dominio
{
    class Articulo
    {
        public string Codigo { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        //Revisar Marca y Categoria

        public string URLImagen { get; set; }

        public float Precio { get; set; }

    }
}
