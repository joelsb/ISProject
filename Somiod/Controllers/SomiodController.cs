using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Somiod.Controllers
{
    public class SomiodController : ApiController
    {
        private static string strDataConn;
        public SomiodController()
        {
            // Dynamically calculate the project path
            string executablePath = AppDomain.CurrentDomain.BaseDirectory;
            string projectPath = Path.Combine(executablePath, @"..\..");

            // Set DataDirectory to the project path
            AppDomain.CurrentDomain.SetData("DataDirectory", projectPath);

            // Use |DataDirectory| in the connection string
            string relativePath = @"|DataDirectory|\App_Data\DBProds.mdf";
            strDataConn = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={relativePath};Integrated Security=True";
        }

        //String strDataConn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\EscolaJoel\ESTG5Ano\1Semestre\IS\Pratica\VisualStudio2023\3.ProductsAPI\ProductsAPI\App_Data\DBProds.mdf;Integrated Security = True";

        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}