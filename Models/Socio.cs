using System;
using System.Collections.Generic;
using System.Text;

namespace alanchi.Examen.Models
{
    public class Socio
    {
      
            public string Nombre { get; set; } = "";
            public string Apellido { get; set; } = "";
            public int Edad { get; set; }
            public DateTime FechaInicio { get; set; }
            public string Plan { get; set; } = "";
            public bool Casillero { get; set; }
            public double MontoInicial { get; set; }
            public double PagoMensual { get; set; }
            public double PagoTotal => MontoInicial + PagoMensual * 5;
        
    }
}
