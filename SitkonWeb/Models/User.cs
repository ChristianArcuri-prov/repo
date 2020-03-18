using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitkonWeb.Models
{
    public class User
    {
        
    }
    public class CustomerModel
    {
        public Int64 Cuit { get; set; }
        public string Sexo { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Edad { get; set; }
        public int Score { get; set; }
        public bool ValidaIdentidad { get; set; }
        public string LimiteMAX { get; set; }
        public string LimiteMIN { get; set; }
        
        public int TipoCliente { get; set; } //1 persona, 2 empresa.
        public string RazonSocial { get; set; }
        public int Situacion { get; set; }
        public decimal MontoDeuda { get; set; }
        public int ChequesRechazados { get; set; }
        public string MontoChequesRechazados { get; set; }

    }
}