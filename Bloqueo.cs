using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diseño_Modelado
{
    public partial class Bloqueo : Form
    {
        string Contraseña = "cacahuate";
        public Bloqueo()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == Contraseña)
            {
                textBox1.Clear();
                Form1 form1 = new Form1();
                form1.Show();
            }
            else
            {
                textBox1.Clear();
                MessageBox.Show("Contraseña Incorreta");
                textBox1.Focus();
                return;
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
