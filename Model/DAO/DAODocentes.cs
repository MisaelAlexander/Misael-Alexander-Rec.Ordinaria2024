using Refuerzo2024.Model.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Refuerzo2024.Model.DAO
{
    internal class DAODocentes : DTODocentes
    {
        SqlConnection con = obtenerConexion();
        public DataSet Docentes()
        {
            try
            {
                string query = "Select*From ViewDocentes";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds, "ViewDocentes");
                return ds;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                con.Close();
            }
        }
        public bool Agregar()
        {
            try
            {
                string query = "Insert Into Docentes Values (@param1 ,@param2,@param3)";
                SqlCommand cmdInsert = new SqlCommand(query, con);
                cmdInsert.Parameters.AddWithValue("param1",NombreDocente);
                cmdInsert.Parameters.AddWithValue("param2",ApellidoDocente);
                cmdInsert.Parameters.AddWithValue("param3",DUI1);
                cmdInsert.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
               
                return false;
            }
            finally
            {
                con.Close();
            }
        }
        public bool Actualizar()
        {
            try
            {
                string query = "Update Docentes SET nombreDocente = @param1, apellidoDocente = @param2, dui = @param3 Where idDocente = @param0";
                SqlCommand cmdInsert = new SqlCommand(query, con);
                cmdInsert.Parameters.AddWithValue("param1", NombreDocente);
                cmdInsert.Parameters.AddWithValue("param2", ApellidoDocente);
                cmdInsert.Parameters.AddWithValue("param3", DUI1);
                cmdInsert.Parameters.AddWithValue("param0", IdDocente);
                cmdInsert.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                con.Close();
            }
        }
        public bool Eliminar()
        {
            try
            {
                string query = "Delete From Docentes  Where idDocente = @param0";
                SqlCommand cmdInsert = new SqlCommand(query, con);
                cmdInsert.Parameters.AddWithValue("param0", IdDocente);
                cmdInsert.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                con.Close();
            }
        }
        public DataSet BuscarDocentes(string Valor)
        {
            try
            {
                string query = $"Select*From ViewDocentes Where ID Like '%{Valor}%' OR Nombre Like '%{Valor}%' OR Apellido Like '%{Valor}%' OR  DUI Like '%{Valor}%'";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds, "ViewDocentes");
                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                con.Close();
            }
        }
    }   
}
