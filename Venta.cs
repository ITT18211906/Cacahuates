using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Diseño_Modelado
{
    public partial class Venta : Form
    {
        //SqlConnection Conexion = new SqlConnection(@"Data Source=LAPTOP-0JPSC3F9\SQL;Initial Catalog= Cacahuate; integrated security=true");
        SqlConnection Conexion = new SqlConnection(@"Server=localhost;Database= Cacahuate; integrated security=true");

        float acumuladorPrecio = 0;

        public Venta()
        {
            InitializeComponent();
            LimpiezaCreacionVenta();
            ActualizarTablaVenta();
        }

        public void LimpiezaCreacionVenta()
        {
            Conexion.Open();
            SqlCommand limpieza = new SqlCommand("DROP TABLE IF EXISTS Venta", Conexion);
            limpieza.ExecuteNonQuery();
            limpieza.Dispose();
            SqlCommand creacion = new SqlCommand("CREATE TABLE Venta(codigo int PRIMARY KEY, nombre varchar(255), cantidad float, precio float)", Conexion);
            creacion.ExecuteNonQuery();
            creacion.Dispose();
            Conexion.Close();
        }

        public void ActualizarTablaVenta()
        {
            Conexion.Open();
            SqlDataAdapter adaptador = new SqlDataAdapter();
            SqlCommand comando = new SqlCommand("SELECT * FROM Venta", Conexion);
            adaptador.SelectCommand = comando;
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            dataGridView1.DataSource = tabla;
            Conexion.Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Conexion.Open();

            int codigoProducto = 0;
            float cantidadDisponible = 0;
            float cantidadSolicitada = 0;
            string nombre = "";
            float precio = 0;

            codigoProducto = int.Parse(textBox1.Text);

            SqlCommand buscarProducto = new SqlCommand("SELECT * FROM Inventario WHERE codigo = @codigo", Conexion);
            buscarProducto.Parameters.AddWithValue("codigo", codigoProducto);

            SqlDataReader leerBuscarProducto = buscarProducto.ExecuteReader();
            while (leerBuscarProducto.Read())
            {
                nombre = leerBuscarProducto.GetString(1);
                cantidadDisponible = leerBuscarProducto.GetFloat(2);
                precio = leerBuscarProducto.GetFloat(3);
            }
            leerBuscarProducto.Close();

            cantidadSolicitada = int.Parse(textBox2.Text);
            
            if (cantidadSolicitada < cantidadDisponible)
            {
                cantidadDisponible -= cantidadSolicitada;

                SqlCommand reducirProducto = new SqlCommand("UPDATE Inventario SET cantidad=@cantidad WHERE codigo=@codigo", Conexion);
                reducirProducto.Parameters.AddWithValue("cantidad", cantidadDisponible);
                reducirProducto.Parameters.AddWithValue("codigo", codigoProducto);
                reducirProducto.ExecuteNonQuery();
                reducirProducto.Dispose();

                precio *= cantidadSolicitada;

                SqlCommand registroVenta = new SqlCommand("INSERT INTO Venta (codigo,nombre,cantidad,precio) VALUES (@codigo,@nombre,@cantidad,@precio)", Conexion);
                registroVenta.Parameters.AddWithValue("codigo", codigoProducto);
                registroVenta.Parameters.AddWithValue("nombre", nombre);
                registroVenta.Parameters.AddWithValue("cantidad", cantidadSolicitada);
                registroVenta.Parameters.AddWithValue("precio", precio);
                registroVenta.ExecuteNonQuery();
                registroVenta.Dispose();

                acumuladorPrecio += precio;

                textBox3.Text = acumuladorPrecio.ToString();
                
                textBox1.Clear();
                textBox2.Clear();
            }
            else
            {
                MessageBox.Show("Cantidad Insuficiente de Producto");
            }
            
            Conexion.Close();
            ActualizarTablaVenta();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"El Total de la Venta es {acumuladorPrecio}");
            Close();
        }
    }
}
