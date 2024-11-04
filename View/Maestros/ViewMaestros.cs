using Refuerzo2024.Controller.Docentes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Refuerzo2024.View.Maestros
{
    public partial class ViewMaestros : Form
    {
        public ViewMaestros()
        {
            InitializeComponent();
            ControllerDocentes doc = new ControllerDocentes(this);
        }
    }
}
