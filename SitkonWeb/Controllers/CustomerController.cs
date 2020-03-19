using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SitkonWeb.Models;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Web;

namespace SitkonWeb.Controllers
{
    public class CustomerController : ApiController
    {
        private string jsonFilePer = System.Web.Configuration.WebConfigurationManager.AppSettings["PATH_JSON_PER"];
        private string jsonFileEmp = System.Web.Configuration.WebConfigurationManager.AppSettings["PATH_JSON_EMP"];
        // GET: api/Customer
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Customer/5
        public string Get(int id)
        {
            return "value";
        }

        
        [HttpGet]
        [Route("api/Customer/GetByCustomer/{cuit}/{sexo}")]
        public IHttpActionResult GetByCustomer(Int64 cuit, string sexo)
        {
            
            if (cuit == null || cuit==0)
            {
                return NotFound();
            }
            
            CustomerModel cusModel = new CustomerModel();            
            cusModel.TipoCliente = (cuit.ToString().Length == 11) ? 2 : 1;

            //Buscando con Linq
            if(cusModel.TipoCliente==1) //Persona fisica
            {
                var jsonText = File.ReadAllText(HttpContext.Current.Server.MapPath(jsonFilePer));
                JObject json = JObject.Parse(jsonText);
                var personasArrary = json["personas"]
                    .Where(x => x.Value<Int64>("cuit") == cuit && x.Value<String>("sexo") == sexo)
                    .ToList();
                if (personasArrary.Count > 0)
                {
                    cusModel.Nombre = (string)personasArrary[0]["nombre"];
                    cusModel.Apellido = (string)personasArrary[0]["apellido"];
                    cusModel.Sexo = (string)personasArrary[0]["sexo"];
                    cusModel.Edad = (int)personasArrary[0]["edad"];
                    cusModel.Cuit = (Int64)personasArrary[0]["cuit"];
                    cusModel.LimiteMIN = (string)personasArrary[0]["limitemin"];
                    cusModel.LimiteMAX = (string)personasArrary[0]["limitemax"];
                    cusModel.Score = (int)personasArrary[0]["score"];
                }
            }
            else //Empresa o persona juridica
            {
                //string path = server
                
                var jsonText = File.ReadAllText(HttpContext.Current.Server.MapPath(jsonFileEmp));
                JObject json = JObject.Parse(jsonText);
                var empresaArrary = json["empresas"]
                    .Where(x => x.Value<Int64>("cuit") == cuit).ToList();
                if (empresaArrary.Count > 0)
                {
                    cusModel.Score = (int)empresaArrary[0]["score"];
                    cusModel.Cuit = (Int64)empresaArrary[0]["cuit"];
                    cusModel.RazonSocial = (string)empresaArrary[0]["razon_social"];
                    cusModel.Situacion = (int)empresaArrary[0]["situacion"];
                    cusModel.MontoDeuda = (decimal)empresaArrary[0]["monto_deuda"];
                    cusModel.ChequesRechazados = (int)empresaArrary[0]["cheques_rechazados"];
                    cusModel.MontoChequesRechazados = (string)empresaArrary[0]["monto_cheques_rechazados"];
                }
            }
               
            return Ok(cusModel);            
        }

        // POST: api/Customer
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Customer/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Customer/5
        public void Delete(int id)
        {

        }
    }
}
