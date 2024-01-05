using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Somiod.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Services.Description;
using System.Windows.Forms;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using Application = Somiod.Models.Application;
using Container = Somiod.Models.Container;
using Subscription = Somiod.Models.Subscription;

namespace Somiod.Controllers
{
    public class SomiodController : ApiController
    {
        private static string strDataConn;
        private static string projectPath;
        private static string executablePath;
        private static string appPath;
        MqttClient mClient = new MqttClient(IPAddress.Parse("127.0.0.1"));
        public SomiodController()
        {
            // Dynamically calculate the project path
            executablePath = AppDomain.CurrentDomain.BaseDirectory;

            string relativePath = $@"{executablePath}App_Data\DBSomiod.mdf";
            strDataConn = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={relativePath};Integrated Security=True";
        }
        #region Application
        //APPLICATION
        //GET Application
        [Route("api/somiod")]
        [HttpGet]
        public IHttpActionResult GetApplication()
        {
            //Only the Application names
            if (Request.Headers.Contains("somiod-discover") && Request.Headers.GetValues("somiod-discover").FirstOrDefault() == "application")
            {
                List<string> applicationNames = new List<string>();
                SqlConnection conn = null;

                try
                {
                    conn = new SqlConnection(strDataConn);
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT Name FROM Applications ORDER BY Id", conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        applicationNames.Add(reader.GetString(0));
                    }
                }
                catch (Exception e)
                {
                    if (conn.State == System.Data.ConnectionState.Open) conn.Close();
                    Console.WriteLine(e.Message);
                    return InternalServerError(e);
                }
                if (applicationNames.Count == 0)
                {
                    Console.WriteLine("There is no apps in somiod yet");
                    return NotFound();
                }
                return Ok(applicationNames);
            }
            //All the content in somiod
            else
            {
                List<Application> applications = new List<Application>();
                SqlConnection conn = null;

                try
                {
                    conn = new SqlConnection(strDataConn);
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Applications ORDER BY Id", conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Application app = new Application
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            CreationDt = reader.GetDateTime(2)
                        };
                        applications.Add(app);
                    }
                }
                catch (Exception e)
                {
                    if (conn.State == System.Data.ConnectionState.Open) conn.Close();
                    Console.WriteLine(e.Message);
                    return InternalServerError(e);
                }
                if (applications.Count == 0)
                {
                    Console.WriteLine("There is no apps in somiod yet");
                    return NotFound();
                }
                return Ok(applications);
            }
        }
        //POST Application
        [Route("api/somiod")]
        [HttpPost]
        public IHttpActionResult PostApplication([FromBody] Application app)
        {
            
            //Post of an application
            if (!ModelState.IsValid)
            {
                // Log ModelState errors
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }

                return BadRequest(ModelState);
            }
            SqlConnection conn = null;
            string requestBody = Request.Content.ReadAsStringAsync().Result;
            
            app.CreationDt = DateTime.Now;
            try
            {
                conn = new SqlConnection(strDataConn);
                conn.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO Applications values(@name,@creation_dt)", conn);

                //string formattedDate = app.Creation_dt.ToString("yyyy-MM-dd HH:mm:ss");
                //TODO: check if the date is in the correct format

                cmd.Parameters.AddWithValue("@name", app.Name);
                cmd.Parameters.Add("@creation_dt", SqlDbType.DateTime).Value = app.CreationDt;

                cmd.CommandType = System.Data.CommandType.Text;

                int nrows = cmd.ExecuteNonQuery();
                conn.Close();

                if (nrows > 0) return Ok(app);
                else return NotFound();
            }
            catch (Exception e)
            {
                if (conn.State == System.Data.ConnectionState.Open) conn.Close();
                Console.WriteLine(e.Message);
                MessageBox.Show("Não foi possivel criar a Aplicação");
                return InternalServerError(e);
            }
        }
        //PUT Application
        [Route("api/somiod/{appName:maxlength(50)}")]
        [HttpPut]
        public IHttpActionResult PutApplication(string appName,[FromBody] Application app)
        {
            SqlConnection conn = null;
            
            app.CreationDt=DateTime.Now;
            
            try
            {
                conn = new SqlConnection(strDataConn);
                conn.Open();

                SqlCommand cmd = new SqlCommand("Update Applications SET Name=@name, Creation_dt=@creation_dt WHERE Name=@nameOrigin", conn);

                cmd.Parameters.AddWithValue("@nameOrigin", appName);
                cmd.Parameters.AddWithValue("@name", app.Name);
                cmd.Parameters.AddWithValue("@creation_dt", app.CreationDt);

                cmd.CommandType = System.Data.CommandType.Text;

                int nrows = cmd.ExecuteNonQuery();
                conn.Close();

                if (nrows > 0) return Ok(app);
                else return NotFound();
            }
            catch (Exception e)
            {
                if (conn.State == System.Data.ConnectionState.Open) conn.Close();
                Console.WriteLine(e.Message);
                return InternalServerError(e);
            }
        }
        
        //DELETE Application
        [Route("api/somiod/{appName:maxlength(50)}")]
        [HttpDelete]
        public IHttpActionResult DeleteApplication(string appName)
        {
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(strDataConn);
                conn.Open();

                SqlCommand cmd = new SqlCommand("DELETE FROM Applications WHERE Name=@name", conn);
                cmd.Parameters.AddWithValue("@name", appName);
                
                cmd.CommandType = System.Data.CommandType.Text;

                int nrows = cmd.ExecuteNonQuery();
                if (nrows > 0) 
                    return Ok("Deleted: " + appName);
                else
                    return BadRequest("App does not exists");
            }
            catch (Exception e)
            {

                MessageBox.Show("Can not delete this Aplication");
                if (conn.State == System.Data.ConnectionState.Open) conn.Close();
                Console.WriteLine(e.Message);
                return InternalServerError(e);
            }
        }
        #endregion
        #region Container
        //CONTAINER
        //GET Container
        [Route("api/somiod/{appName:maxlength(50)}")]
        [HttpGet]
        public IHttpActionResult GetContainer(string appName)
        {
            //Only the containers names
            if (Request.Headers.Contains("somiod-discover") && Request.Headers.GetValues("somiod-discover").FirstOrDefault() == "container")
            {
                List<string> containerNames = new List<string>();
                SqlConnection conn = null;

                try
                {
                    conn = new SqlConnection(strDataConn);
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT Name FROM Containers WHERE Parent = (SELECT Id FROM Applications WHERE Name = @appName) ORDER BY Id", conn);
                    cmd.Parameters.AddWithValue("@appName", appName);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        containerNames.Add(reader.GetString(0));
                    }
                }
                catch (Exception e)
                {
                    if (conn.State == System.Data.ConnectionState.Open) conn.Close();
                    Console.WriteLine(e.Message);
                    return InternalServerError(e);
                }
                if (containerNames.Count == 0)
                {
                    Console.WriteLine("There is no containers in this app yet");
                    return NotFound();
                }
                return Ok(containerNames);
            }
            //All the content in the application - Containers
            else
            {
                List<Container> containers = new List<Container>();
                SqlConnection conn = null;

                try
                {
                    conn = new SqlConnection(strDataConn);
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Containers WHERE Parent = (SELECT Id FROM Applications WHERE Name = @appName) ORDER BY Id", conn);
                    cmd.Parameters.AddWithValue("@appName", appName);
                    SqlDataReader reader = cmd.ExecuteReader();
                    

                    while (reader.Read())
                    {
                        Container container = new Container
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            CreationDt = reader.GetDateTime(2),
                            Parent = reader.GetInt32(3)
                        };
                        containers.Add(container);
                    }
                }
                catch (Exception e)
                {
                    if (conn.State == System.Data.ConnectionState.Open) conn.Close();
                    Console.WriteLine(e.Message);
                    return InternalServerError(e);
                }
                if (containers.Count == 0)
                {
                    Console.WriteLine("There is no containers in this app yet");
                    return NotFound();
                }
                return Ok(containers);
            }
        }
        //POST Container
        [Route("api/somiod/{appName:maxlength(50)}")]
        [HttpPost]
        public IHttpActionResult PostContainer(string appName,[FromBody] Container container)
        {

            //Post of an application
            if (!ModelState.IsValid )
            {
                // Log ModelState errors
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }

                return BadRequest(ModelState);
            }
            SqlConnection conn = null;
            string requestBody = Request.Content.ReadAsStringAsync().Result;
            SqlConnection conn2 = null;
            container.CreationDt = DateTime.Now;
            try
            {
                conn = new SqlConnection(strDataConn);
                conn.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO Containers values(@name,@creation_dt,@parent)", conn);

                //string formattedDate = app.Creation_dt.ToString("yyyy-MM-dd HH:mm:ss");
                //TODO: check if the date is in the correct format

                cmd.Parameters.AddWithValue("@name", container.Name);
                cmd.Parameters.Add("@creation_dt", SqlDbType.DateTime).Value = container.CreationDt;

                //get the container parent id in the database applications table using name = appName
                //------------------
                int parentId = 0;
                try
                {
                    conn2 = new SqlConnection(strDataConn);
                    conn2.Open();
                    SqlCommand cmd2 = new SqlCommand("SELECT Id FROM Applications WHERE Name = @appName", conn2);
                    cmd2.Parameters.AddWithValue("@appName", appName);
                    SqlDataReader reader = cmd2.ExecuteReader();
                    while (reader.Read())
                    {
                        parentId = reader.GetInt32(0);
                    }
                    conn2.Close();
                }catch(Exception e1)
                {
                    if (conn2.State == System.Data.ConnectionState.Open) conn2.Close();
                    Console.WriteLine(e1.Message);
                    return null;
                }
                finally
                {
                    cmd.Parameters.AddWithValue("@parent", parentId);
                }
                //------------------

                cmd.CommandType = System.Data.CommandType.Text;

                int nrows = cmd.ExecuteNonQuery();
                conn.Close();

                if (nrows > 0) return Ok(container);
                else return NotFound();
            }
            catch (Exception e)
            {
                if (conn.State == System.Data.ConnectionState.Open) conn.Close();
                Console.WriteLine(e.Message);
                MessageBox.Show("Não foi possivel criar o Container");
                return InternalServerError(e);
            }
        }

        //PUT Container
        [Route("api/somiod/{appName:maxlength(50)}/{containerName:maxlength(50)}")]
        [HttpPut]
        public IHttpActionResult PutContainer(string appName, string containerName,[FromBody] Container container)
        {
            if (!ModelState.IsValid)
            {
                // Log ModelState errors
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }

                return BadRequest(ModelState);
            }
            SqlConnection conn = null;
            string requestBody = Request.Content.ReadAsStringAsync().Result;
            SqlConnection conn2 = null;
            container.CreationDt = DateTime.Now;
            try
            {
                conn = new SqlConnection(strDataConn);
                conn.Open();

                SqlCommand cmd = new SqlCommand("Update Containers SET Name=@name, Creation_dt=@creation_dt, Parent=@parent WHERE Name=@nameOrigin", conn);

                //string formattedDate = app.Creation_dt.ToString("yyyy-MM-dd HH:mm:ss");
                //TODO: check if the date is in the correct format
                cmd.Parameters.AddWithValue("@nameOrigin", containerName);
                cmd.Parameters.AddWithValue("@name", container.Name);
                cmd.Parameters.Add("@creation_dt", SqlDbType.DateTime).Value = container.CreationDt;

                //get the container parent id in the database applications table using name = appName
                //------------------
                int parentId = 0;
                try
                {
                    conn2 = new SqlConnection(strDataConn);
                    conn2.Open();
                    SqlCommand cmd2 = new SqlCommand("SELECT Id FROM Applications WHERE Name = @appName", conn2);
                    cmd2.Parameters.AddWithValue("@appName", appName);
                    SqlDataReader reader = cmd2.ExecuteReader();
                    while (reader.Read())
                    {
                        parentId = reader.GetInt32(0);
                    }
                    conn2.Close();
                }
                catch (Exception e1)
                {
                    if (conn2.State == System.Data.ConnectionState.Open) conn2.Close();
                    Console.WriteLine(e1.Message);
                    return null;
                }
                finally
                {
                    cmd.Parameters.AddWithValue("@parent", parentId);
                }
                //------------------

                cmd.CommandType = System.Data.CommandType.Text;

                int nrows = cmd.ExecuteNonQuery();
                conn.Close();

                if (nrows > 0) return Ok(container);
                else return NotFound();
            }
            catch (Exception e)
            {
                if (conn.State == System.Data.ConnectionState.Open) conn.Close();
                Console.WriteLine(e.Message);
                return InternalServerError(e);
            }
        }

        //DELETE Container
        [Route("api/somiod/{appName:maxlength(50)}/{containerName:maxlength(50)}")]
        [HttpDelete]
        public IHttpActionResult DeleteContainer(string appName, string containerName)
        {
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(strDataConn);
                conn.Open();

                SqlCommand cmd = new SqlCommand("DELETE FROM Containers WHERE Name=@name", conn);
                cmd.Parameters.AddWithValue("@name", containerName);

                cmd.CommandType = System.Data.CommandType.Text;

                int nrows = cmd.ExecuteNonQuery();
                if (nrows > 0) return Ok("Deleted: " + containerName);
                else return NotFound();
            }
            catch (Exception e)
            {
                MessageBox.Show("Can not delete this Container");
                if (conn.State == System.Data.ConnectionState.Open) conn.Close();
                Console.WriteLine(e.Message);
                return InternalServerError(e);
            }
        }
        //GET Subscribe and Data names
        //Get all the content in the container
        [Route("api/somiod/{appName:maxlength(50)}/{containerName:maxlength(50)}")]
        [HttpGet]
        public IHttpActionResult GetDataAndSubscriptions(string appName, string containerName)
        {
            //Only the data names
            if (Request.Headers.Contains("somiod-discover") && Request.Headers.GetValues("somiod-discover").FirstOrDefault() == "data")
            {
                List<string> dataNames = new List<string>();
                SqlConnection conn = null;

                try
                {
                    conn = new SqlConnection(strDataConn);
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT Name FROM Data WHERE Parent = (SELECT Id FROM Containers WHERE Name = @containerName AND Parent = (SELECT Id FROM Applications WHERE Name = @appName)) ORDER BY Id", conn);
                    cmd.Parameters.AddWithValue("@appName", appName);
                    cmd.Parameters.AddWithValue("@containerName", containerName);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        dataNames.Add(reader.GetString(0));
                    }
                }
                catch (Exception e)
                {
                    if (conn.State == System.Data.ConnectionState.Open) conn.Close();
                    Console.WriteLine(e.Message);
                    return InternalServerError(e);
                }
                if (dataNames.Count == 0)
                {
                    Console.WriteLine("There is no data in this container yet");
                    return NotFound();
                }
                return Ok(dataNames);
            }
            //Only the subscription names
            if (Request.Headers.Contains("somiod-discover") && Request.Headers.GetValues("somiod-discover").FirstOrDefault() == "subscription")
            {
                List<string> subscriptionNames = new List<string>();
                SqlConnection conn = null;

                try
                {
                    conn = new SqlConnection(strDataConn);
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT Name FROM Subscriptions WHERE Parent = (SELECT Id FROM Containers WHERE Name = @containerName AND Parent = (SELECT Id FROM Applications WHERE Name = @appName)) ORDER BY Id", conn);
                    cmd.Parameters.AddWithValue("@appName", appName);
                    cmd.Parameters.AddWithValue("@containerName", containerName);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        subscriptionNames.Add(reader.GetString(0));
                    }
                }
                catch (Exception e)
                {
                    if (conn.State == System.Data.ConnectionState.Open) conn.Close();
                    Console.WriteLine(e.Message);
                    return InternalServerError(e);
                }
                if (subscriptionNames.Count == 0)
                {
                    Console.WriteLine("There is no subscriptions in this container yet");
                    return NotFound();
                }
                return Ok(subscriptionNames);
            }
            //TODO: all the content in the container (Data and subscription)
            else
            {
                return Ok();
            }
        }
        #endregion
        #region Data
        //DATA TODO
        //GET containers datas
        [Route("api/somiod/{appName:maxlength(50)}/{containerName:maxlength(50)}/data")]
        [HttpGet]
        public IHttpActionResult GetDatas(string appName, string containerName)
        {
            //All the content in the Container
            List<Data> datas = new List<Data>();
            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(strDataConn);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Data WHERE Parent = (SELECT Id FROM Containers WHERE Name = @containerName AND Parent = (SELECT Id FROM Applications WHERE Name = @appName)) ORDER BY Id", conn);
                cmd.Parameters.AddWithValue("@containerName", containerName); // Add the parameter here
                cmd.Parameters.AddWithValue("@appName", appName); // Add the parameter here
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    Data data = new Data
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Content = reader.GetString(2),
                        CreationDt = reader.GetDateTime(3),
                        Parent = reader.GetInt32(4),

                    };
                    datas.Add(data);
                }
            }
            catch (Exception e)
            {
                if (conn.State == System.Data.ConnectionState.Open) conn.Close();
                Console.WriteLine(e.Message);
                return InternalServerError(e);
            }
            finally
            {
                conn.Close();
            }

            if (datas.Count == 0)
            {
                Console.WriteLine("There is no datas in this container yet");
                return NotFound();
            }

            return Ok(datas);

        }
        //GET containers datas
        [Route("api/somiod/{appName:maxlength(50)}/{containerName:maxlength(50)}/data/{dataName:maxlength(50)}")]
        [HttpGet]
        public IHttpActionResult GetData(string appName, string containerName, string dataName)
        {
            //All the content in the Container
            List<Data> datas = new List<Data>();
            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(strDataConn);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Data WHERE Parent = (SELECT Id FROM Containers WHERE Name = @containerName AND Parent = (SELECT Id FROM Applications WHERE Name = @appName)) AND Name = @dataName ORDER BY Id", conn);
                cmd.Parameters.AddWithValue("@containerName", containerName); // Add the parameter here
                cmd.Parameters.AddWithValue("@dataName", dataName); // Add the parameter here
                cmd.Parameters.AddWithValue("@appName", appName); // Add the parameter here
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    Data data = new Data
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Content = reader.GetString(2),
                        CreationDt = reader.GetDateTime(3),
                        Parent = reader.GetInt32(4),

                    };
                    datas.Add(data);
                }
            }
            catch (Exception e)
            {
                if (conn.State == System.Data.ConnectionState.Open) conn.Close();
                Console.WriteLine(e.Message);
                return InternalServerError(e);
            }
            finally
            {
                conn.Close();
            }

            if (datas.Count == 0)
            {
                Console.WriteLine("There is no datas in this container yet");
                return NotFound();
            }

            return Ok(datas);

        }
        //POST Data
        [Route("api/somiod/{appName:maxlength(50)}/{containerName:maxlength(50)}/data")]
        [HttpPost]
        public IHttpActionResult PostData(string containerName, [FromBody] Data data)
        {
            data.CreationDt = DateTime.Now;
            List<string> datas = new List<string>();
            using (SqlConnection conn = new SqlConnection(strDataConn))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("INSERT INTO Data (Name, Creation_dt, Parent, Content) VALUES (@name, @creation_dt, @parent,@content); SELECT SCOPE_IDENTITY();", conn))
                {
                    cmd.Parameters.AddWithValue("@name", data.Name);
                    cmd.Parameters.AddWithValue("@creation_dt", data.CreationDt);
                    cmd.Parameters.AddWithValue("@content", data.Content);

                    int parentId = 0;
                    string topic = "";
                    using (SqlConnection conn2 = new SqlConnection(strDataConn))
                    {
                        conn2.Open();

                        using (SqlCommand cmd2 = new SqlCommand("SELECT Id FROM Containers WHERE Name = @containerName", conn2))
                        {
                            cmd2.Parameters.AddWithValue("@containerName", containerName);

                            using (SqlDataReader reader = cmd2.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    parentId = reader.GetInt32(0);
                                }
                            }
                        }
                    }

                    cmd.Parameters.AddWithValue("@parent", parentId);

                    try
                    {
                        cmd.CommandType = CommandType.Text;
                        // Retrieve the last inserted ID using ExecuteScalar
                        int insertedId = Convert.ToInt32(cmd.ExecuteScalar());

                        if (insertedId > 0)
                        {
                            //TODO: Publish MQTT event on creation subscriptions
                            using (SqlConnection conn2 = new SqlConnection(strDataConn))
                            {
                                conn2.Open();

                                using (SqlCommand cmd2 = new SqlCommand("SELECT Name FROM Subscriptions WHERE Event LIKE '%1%' And Parent = @parent", conn2))
                                {
                                    cmd2.Parameters.AddWithValue("@parent", parentId);
                                    using (SqlDataReader reader = cmd2.ExecuteReader())
                                    {
                                        while (reader.Read())
                                        {
                                            topic = reader.GetString(0);
                                            datas.Add(topic);
                                        }
                                    }
                                }
                            }
                            byte[] msg = Encoding.UTF8.GetBytes(data.Content);
                            mClient.Connect(Guid.NewGuid().ToString());

                            if (!mClient.IsConnected)
                            {
                                MessageBox.Show("Não foi possivel ligar ao broker");
                                return NotFound();
                            }
                            if (datas.Count==1)
                            {
                                mClient.Publish(datas[0], msg);
                            }
                            else
                            {
                                mClient.Publish(datas[0], msg);
                                mClient.Publish(datas[1], msg);
                            }
                            return Ok(data);
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                        Console.WriteLine(e.Message);
                        return InternalServerError(e);
                    }
                }
            }
        }
        //DELETE Data
        [Route("api/somiod/{appName:maxlength(50)}/{containerName:maxlength(50)}/data/{dataName:maxlength(50)}")]
        [HttpDelete]
        public IHttpActionResult DeleteData(string containerName, string dataName)
        {
            SqlConnection conn = null;
            try
            {
                List<string> datas = new List<string>();
                conn = new SqlConnection(strDataConn);
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Data WHERE Name=@name", conn);
                cmd.Parameters.AddWithValue("@name", dataName);

                cmd.CommandType = System.Data.CommandType.Text;

                int nrows = cmd.ExecuteNonQuery();
                if (nrows > 0)
                {
                    int parentId = 0;
                    string topic = "";
                    using (SqlConnection conn2 = new SqlConnection(strDataConn))
                    {
                        conn2.Open();

                        using (SqlCommand cmd2 = new SqlCommand("SELECT Id FROM Containers WHERE Name = @containerName", conn2))
                        {
                            cmd2.Parameters.AddWithValue("@containerName", containerName);

                            using (SqlDataReader reader = cmd2.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    parentId = reader.GetInt32(0);
                                }
                            }
                        }
                    }

                    cmd.Parameters.AddWithValue("@parent", parentId);
                    //TODO: Publish MQTT event on delete subscriptions
                    using (SqlConnection conn2 = new SqlConnection(strDataConn))
                    {
                        conn2.Open();

                        using (SqlCommand cmd2 = new SqlCommand("SELECT Name FROM Subscriptions WHERE Event LIKE '%2%' And Parent = @parent", conn2))
                        {
                            cmd2.Parameters.AddWithValue("@parent", parentId);
                            using (SqlDataReader reader = cmd2.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    topic = reader.GetString(0);
                                    datas.Add(topic);
                                }
                            }
                        }
                    }
                    byte[] msg = Encoding.UTF8.GetBytes("Deleted:" + dataName);
                    mClient.Connect(Guid.NewGuid().ToString());

                    if (!mClient.IsConnected)
                    {
                        MessageBox.Show("Não foi possivel ligar ao broker");
                        return NotFound();
                    }
                    if (datas.Count == 1)
                    {
                        mClient.Publish(datas[0], msg);
                    }
                    else
                    {
                        mClient.Publish(datas[0], msg);
                        mClient.Publish(datas[1], msg);
                    }
                    return Ok("Deleted: " + dataName);
                }
                else return NotFound();
            }
            catch (Exception e)
            {
                if (conn.State == System.Data.ConnectionState.Open) conn.Close();
                Console.WriteLine(e.Message);
                return InternalServerError(e);
            }
        }
        #endregion
        #region Subscription
        //SUBSCRIPTION
        //GET SUBSCRIPTION
        [Route("api/somiod/{appName:maxlength(50)}/{containerName:maxlength(50)}/subscription/{subscriptionName:maxlength(50)}")]
        [HttpGet]
        public IHttpActionResult GetSubscription(string appName, string containerName, string subscriptionName)
        {
            List<Subscription> subscriptions = new List<Subscription>();
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(strDataConn);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Subscriptions WHERE Parent = (SELECT Id FROM Containers WHERE Name = @containerName AND Parent = (SELECT Id FROM Applications WHERE Name = @appName)) AND Name = @subscriptionName ORDER BY Id", conn);
                cmd.Parameters.AddWithValue("@appName", appName);
                cmd.Parameters.AddWithValue("@containerName", containerName);
                cmd.Parameters.AddWithValue("@subscriptionName", subscriptionName);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Subscription subscription = new Subscription
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        CreationDt = reader.GetDateTime(2),
                        Parent = reader.GetInt32(3),
                        Event = reader.GetString(4),
                        Endpoint = reader.GetString(5)
                    };
                    subscriptions.Add(subscription);
                }
            }
            catch (Exception e)
            {
                if (conn.State == System.Data.ConnectionState.Open) conn.Close();
                Console.WriteLine(e.Message);
                return InternalServerError(e);
            }
            if (subscriptions.Count == 0)
            {
                Console.WriteLine("There are no subscriptions in this container yet");
                return NotFound();
            }
            return Ok(subscriptions);
            
        }

        //GET ALL SUBSCRIPTIONS
        [Route("api/somiod/{appName:maxlength(50)}/{containerName:maxlength(50)}/subscription")]
        [HttpGet]
        public IHttpActionResult GetAllSubscription(string appName, string containerName)
        {  
            List<Subscription> subscriptions = new List<Subscription>();
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(strDataConn);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Subscriptions WHERE Parent = (SELECT Id FROM Containers WHERE Name = @containerName AND Parent = (SELECT Id FROM Applications WHERE Name = @appName)) ORDER BY Id", conn);
                cmd.Parameters.AddWithValue("@appName", appName);
                cmd.Parameters.AddWithValue("@containerName", containerName);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Subscription subscription = new Subscription
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        CreationDt = reader.GetDateTime(2),
                        Parent = reader.GetInt32(3),
                        Event = reader.GetString(4),
                        Endpoint = reader.GetString(5)
                    };
                    subscriptions.Add(subscription);
                }
            }
            catch (Exception e)
            {
                if (conn.State == System.Data.ConnectionState.Open) conn.Close();
                Console.WriteLine(e.Message);
                return InternalServerError(e);
            }
            if (subscriptions.Count == 0)
            {
                Console.WriteLine("There are no subscriptions in this container yet");
                return NotFound();
            }
            return Ok(subscriptions);            
        }

        // POST Subscription
        [Route("api/somiod/{appName:maxlength(50)}/{containerName:maxlength(50)}/subscription")]
        [HttpPost]
        public IHttpActionResult PostSubscription(string appName, string containerName, [FromBody] Subscription subscription)
        {
            subscription.CreationDt = DateTime.Now;
            subscription.Endpoint = "mqtt://127.0.0.1/";

            using (SqlConnection conn = new SqlConnection(strDataConn))
            {
                conn.Open();
                int count = 0;
                using (SqlCommand cmd = new SqlCommand("Select Id From Subscriptions where Event = @event And Parent = @parent;", conn))
                {
                    int parentId = 0;

                    // Retrieve Parent ID from Containers table
                    using (SqlCommand cmd2 = new SqlCommand("SELECT Id FROM Containers WHERE Name = @containerName", conn))
                    {
                        cmd2.Parameters.AddWithValue("@containerName", containerName);

                        using (SqlDataReader reader = cmd2.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                parentId = reader.GetInt32(0);
                            }
                        }
                    }

                    // Set @parent parameter considering DBNull.Value
                    cmd.Parameters.AddWithValue("@parent", parentId == 0 ? DBNull.Value : (object)parentId);
                    cmd.Parameters.AddWithValue("@event", subscription.Event);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            count = count +1;
                        }
                    }
                }
                if (count>0)
                {
                    MessageBox.Show("Cant create another operation with that type");
                    return BadRequest("Cant create another operation with that type");
                }
                // Insert into Subscriptions table
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Subscriptions (Name, Creation_dt, Endpoint, Event, Parent) VALUES (@name, @creation_dt, @endpoint, @event, @parent); SELECT SCOPE_IDENTITY();", conn))
                {
                    cmd.Parameters.AddWithValue("@name", containerName + subscription.Name);
                    cmd.Parameters.AddWithValue("@creation_dt", subscription.CreationDt);
                    cmd.Parameters.AddWithValue("@endpoint", subscription.Endpoint);
                    cmd.Parameters.AddWithValue("@event", subscription.Event);

                    int parentId = 0;

                    // Retrieve Parent ID from Containers table
                    using (SqlCommand cmd2 = new SqlCommand("SELECT Id FROM Containers WHERE Name = @containerName", conn))
                    {
                        cmd2.Parameters.AddWithValue("@containerName", containerName);

                        using (SqlDataReader reader = cmd2.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                parentId = reader.GetInt32(0);
                            }
                        }
                    }

                    // Set @parent parameter considering DBNull.Value
                    cmd.Parameters.AddWithValue("@parent", parentId == 0 ? DBNull.Value : (object)parentId);

                    try
                    {
                        cmd.CommandType = CommandType.Text;

                        // Retrieve the last inserted ID using ExecuteScalar
                        int insertedId = Convert.ToInt32(cmd.ExecuteScalar());

                        if (insertedId > 0)
                        {
                            return Ok(subscription);
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                    catch (Exception e)
                    {
                        // Log the exception

                        MessageBox.Show("Não foi possivel criar a Subscrição");
                        Console.WriteLine(e.Message);
                        return InternalServerError(e);
                    }
                }
            }
        }
        /*    try
            {
                conn = new SqlConnection(strDataConn);
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Subscriptions (Name, Parent, Creation_dt, Event, Endpoint) VALUES (@name, (SELECT Id FROM Containers WHERE Name = @containerName AND Parent = (SELECT Id FROM Applications WHERE Name = @appName)), @creation_dt, @event, @endpoint)", conn);
                cmd.Parameters.AddWithValue("@name", subscription.Name);
                cmd.Parameters.AddWithValue("@appName", appName);
                cmd.Parameters.AddWithValue("@creation_dt", subscription.CreationDt);
                cmd.Parameters.AddWithValue("@containerName", containerName);
                cmd.Parameters.AddWithValue("@event", subscription.Event);
                cmd.Parameters.AddWithValue("@endpoint", subscription.Endpoint);

                int nrows = cmd.ExecuteNonQuery();
                if (nrows > 0) return Ok("Created: " + subscription.Name);
                else return NotFound();
            }
            catch (Exception e)
            {
                if (conn.State == System.Data.ConnectionState.Open) conn.Close();
                Console.WriteLine(e.Message);
                return InternalServerError(e);
            }*/
    

        // DELETE Subscription
        [Route("api/somiod/{appName:maxlength(50)}/{containerName:maxlength(50)}/subscription/{subscriptionName:maxlength(50)}")]
        [HttpDelete]
        public IHttpActionResult DeleteSubscription(string appName, string containerName, string subscriptionName)
        {
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(strDataConn);
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Subscriptions WHERE Name = @subscriptionName AND Parent = (SELECT Id FROM Containers WHERE Name = @containerName AND Parent = (SELECT Id FROM Applications WHERE Name = @appName))", conn);
                cmd.Parameters.AddWithValue("@subscriptionName", subscriptionName);
                cmd.Parameters.AddWithValue("@appName", appName);
                cmd.Parameters.AddWithValue("@containerName", containerName);
                int nrows = cmd.ExecuteNonQuery();
                if (nrows > 0) return Ok("Deleted: " + subscriptionName);
                else return NotFound();
            }
            catch (Exception e)
            {
                if (conn.State == System.Data.ConnectionState.Open) conn.Close();
                Console.WriteLine(e.Message);
                return InternalServerError(e);
            }
        }
        #endregion
    }
}