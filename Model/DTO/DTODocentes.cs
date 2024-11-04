using Refuerzo2024.Model.Conexion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refuerzo2024.Model.DTO
{
    internal class DTODocentes:dbContext
    {
        int idDocente;
        string nombreDocente;
        string apellidoDocente;
        string DUI;

        public int IdDocente { get => idDocente; set => idDocente = value; }
        public string NombreDocente { get => nombreDocente; set => nombreDocente = value; }
        public string ApellidoDocente { get => apellidoDocente; set => apellidoDocente = value; }
        public string DUI1 { get => DUI; set => DUI = value; }
    }
}
