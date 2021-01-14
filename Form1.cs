using System;
using System.Windows.Forms;

namespace Diseño_Modelado
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Venta venta = new Venta();
            venta.Show();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Inventario inventario = new Inventario();
            inventario.Show();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
