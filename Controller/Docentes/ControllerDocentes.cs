using Refuerzo2024.Model.DAO;
using Refuerzo2024.View.Maestros;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Refuerzo2024.Controller.Docentes
{
    internal class ControllerDocentes
    {
        ViewMaestros ObjMaestros;
          public ControllerDocentes(ViewMaestros Vista) 
        {
            ObjMaestros = Vista;
            Vista.Load += new EventHandler(Carga);
            Vista.btnGuardar.Click += new EventHandler(Agregar);
            Vista.dvgMaestros.CellClick += new DataGridViewCellEventHandler(CargarDatos);
            Vista.btnActualizar.Click += new EventHandler(Actualizar);
            Vista.btnEliminar.Click += new EventHandler(Eliminar);
            Vista.btnBuscar.Click += new EventHandler(Buscar);
            Vista.txtNombre.KeyPress += new KeyPressEventHandler(MaximoNombre);
            Vista.txtApellido.KeyPress += new KeyPressEventHandler(MaximoApellido);
        }
        public void Carga(object sender, EventArgs e) 
        {
            CargarDatagridView();
            ObjMaestros.btnActualizar.Visible = false;
            ObjMaestros.btnEliminar.Visible = false;
        }
        public void CargarDatagridView() 
        {
            DAODocentes DAO = new DAODocentes();
            DataSet ds = DAO.Docentes();
            ObjMaestros.dvgMaestros.DataSource= ds.Tables["ViewDocentes"];
        }
        public void Agregar(object sender, EventArgs e)
        {
            DAODocentes DAO = new DAODocentes();
            if(!(string.IsNullOrEmpty(ObjMaestros.txtApellido.Text.Trim()) ||
                string.IsNullOrEmpty(ObjMaestros.txtNombre.Text.Trim()) ||
                string.IsNullOrEmpty(ObjMaestros.mskDUI.Text.Trim())))
            {
                DAO.NombreDocente = ObjMaestros.txtNombre.Text.Trim();
                DAO.ApellidoDocente = ObjMaestros.txtApellido.Text.Trim();
                DAO.DUI1 = ObjMaestros.mskDUI.Text.Trim();
                if (DAO.Agregar() == true)
                {
                    CargarDatagridView();
                    LimpiarCampos();
                    MessageBox.Show("Datos Ingresados", "Proceso Completo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Datos No Ingresados", "Proceso No Completo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Campos vacios", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public void CargarDatos(object sender, DataGridViewCellEventArgs e)
        {
            int pos = ObjMaestros.dvgMaestros.CurrentRow.Index;
            ObjMaestros.txtID.Text = ObjMaestros.dvgMaestros[0,pos].Value.ToString();
            ObjMaestros.txtNombre.Text = ObjMaestros.dvgMaestros[1, pos].Value.ToString();
            ObjMaestros.txtApellido.Text = ObjMaestros.dvgMaestros[2, pos].Value.ToString();
            ObjMaestros.mskDUI.Text = ObjMaestros.dvgMaestros[3, pos].Value.ToString();
            ObjMaestros.btnActualizar.Visible = true;
            ObjMaestros.btnEliminar.Visible = true;
            ObjMaestros.btnGuardar.Visible = false;
        }
        public void Actualizar(object sender, EventArgs e)
        {
            DAODocentes DAO = new DAODocentes();
            if (!(string.IsNullOrEmpty(ObjMaestros.txtApellido.Text.Trim()) ||
                string.IsNullOrEmpty(ObjMaestros.txtNombre.Text.Trim()) ||
                string.IsNullOrEmpty(ObjMaestros.mskDUI.Text.Trim()) ||
                string.IsNullOrEmpty(ObjMaestros.txtID.Text.Trim())))
            {
                DAO.IdDocente = int.Parse(ObjMaestros.txtID.Text.Trim());
                DAO.NombreDocente = ObjMaestros.txtNombre.Text.Trim();
                DAO.ApellidoDocente = ObjMaestros.txtApellido.Text.Trim();
                DAO.DUI1 = ObjMaestros.mskDUI.Text.Trim();
                if (DAO.Actualizar() == true)
                {
                    CargarDatagridView();
                    LimpiarCampos();
                    MessageBox.Show("Datos Actualizados", "Proceso Completo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Datos No Actualizados", "Proceso No Completo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Campos vacios", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public void Eliminar(object sender, EventArgs e)
        {
            DAODocentes DAO = new DAODocentes();
            if (!string.IsNullOrEmpty(ObjMaestros.txtID.Text.Trim()))
            {
                DAO.IdDocente = int.Parse(ObjMaestros.txtID.Text.Trim());
                int pos = ObjMaestros.dvgMaestros.CurrentRow.Index;
               string Nombre = ObjMaestros.dvgMaestros[1, pos].Value.ToString();
                string Apellido = ObjMaestros.dvgMaestros[2, pos].Value.ToString();
                if(MessageBox.Show($"¿Desea eliminar los datos del maestro {Nombre} {Apellido}?", "Consulta", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    if (DAO.Eliminar() == true)
                    {
                        CargarDatagridView();
                        LimpiarCampos();
                        MessageBox.Show("Datos Eliminados", "Proceso Completo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        LimpiarCampos();
                        MessageBox.Show("Datos No Eliminados", "Proceso No Completo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    LimpiarCampos();
                    MessageBox.Show("Acccion Cancelada", "Proceso Completo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Campos vacios", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public void Buscar(object sender, EventArgs e)
        {
            DAODocentes DAO = new DAODocentes();
            DataSet ds = DAO.BuscarDocentes(ObjMaestros.txtBuscar.Text.Trim());
            ObjMaestros.dvgMaestros.DataSource = ds.Tables["ViewDocentes"];
        }
        public void LimpiarCampos()
        {
            ObjMaestros.txtID.Clear();
            ObjMaestros.txtNombre.Clear();
            ObjMaestros.txtApellido.Clear();
            ObjMaestros.mskDUI.Clear();
            ObjMaestros.btnActualizar.Visible = false;
            ObjMaestros.btnEliminar.Visible = false;
            ObjMaestros.btnGuardar.Visible = true;
        }
        public void MaximoNombre(object sender, KeyPressEventArgs e)
        {
            if(ObjMaestros.txtNombre.TextLength > 15 )
            {
                MessageBox.Show("Maximo de caracteres en nombre alcanzado", "Error", MessageBoxButtons.OK,MessageBoxIcon.Error);
                ObjMaestros.txtNombre.MaxLength = 15;
            }
         
        }
        public void MaximoApellido(object sender, KeyPressEventArgs e)
        {
            if (ObjMaestros.txtApellido.TextLength > 15)
            {
                MessageBox.Show("Maximo de caracteres en nombre alcanzado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ObjMaestros.txtApellido.MaxLength = 15;
            }

        }
    }
}
