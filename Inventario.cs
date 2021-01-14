using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Diseño_Modelado
{
    public partial class Inventario : Form
    {
        //SqlConnection Conexion = new SqlConnection(@"Data Source=LAPTOP-0JPSC3F9\SQL;Initial Catalog= Cacahuate; integrated security=true");
        SqlConnection Conexion = new SqlConnection(@"Server=localhost;Database= Cacahuate; integrated security=true");
        public Inventario()
        {
            InitializeComponent();
            ActualizarTablaInventario();
        }

        public void ActualizarTablaInventario()
        {
            Conexion.Open();
            SqlDataAdapter adaptador = new SqlDataAdapter();
            SqlCommand comando = new SqlCommand("SELECT * from Inventario", Conexion);
            adaptador.SelectCommand = comando;
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            dataGridView1.DataSource = tabla;
            Conexion.Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Conexion.Open();
            SqlCommand inventarioAlta = new SqlCommand ("INSERT INTO Inventario (codigo,nombre,cantidad,precio) VALUES (@codigo,@nombre,@cantidad,@precio)", Conexion);
            inventarioAlta.Parameters.AddWithValue("codigo", textBox1.Text);
            inventarioAlta.Parameters.AddWithValue("nombre", textBox2.Text);
            inventarioAlta.Parameters.AddWithValue("cantidad", textBox3.Text);
            inventarioAlta.Parameters.AddWithValue("precio", textBox4.Text);
            inventarioAlta.ExecuteNonQuery();
            inventarioAlta.Dispose();
            Conexion.Close();

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            ActualizarTablaInventario();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Conexion.Open();
            SqlCommand inventarioBaja = new SqlCommand("DELETE FROM Inventario WHERE codigo = @codigo", Conexion);
            inventarioBaja.Parameters.AddWithValue("codigo", textBox1.Text);
            inventarioBaja.ExecuteNonQuery();
            inventarioBaja.Dispose();
            Conexion.Close();

            textBox1.Clear();
            ActualizarTablaInventario();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
