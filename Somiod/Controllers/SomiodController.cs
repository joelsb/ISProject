using Somiod.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
        private static string projectPath;
        private static string executablePath;
        private static string appPath;
        public SomiodController()
        {
            // Dynamically calculate the project path
            executablePath = AppDomain.CurrentDomain.BaseDirectory;

            string relativePath = $@"{executablePath}App_Data\DBSomiod.mdf";
            strDataConn = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={relativePath};Integrated Security=True";
        }
        //APPLICATION
        //GET Application
        [Route("api/somiod")]
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
        public IHttpActionResult PostApplication(Application app)
        {
            //Post of an application
            SqlConnection conn = null;
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
                return InternalServerError(e);
            }
        }
        //PUT Application
        [Route("api/somiod/{appName:maxlength(50)}")]
        public IHttpActionResult PutApplication(string appName, Application app)
        {
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(strDataConn);
                conn.Open();

                SqlCommand cmd = new SqlCommand("Update Applications SET Name=@name, Creation_dt=@creation_dt WHERE Id=@id", conn);

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
                if (nrows > 0) return Ok("Deleted: " + appName);
                else return NotFound();
            }
            catch (Exception e)
            {
                if (conn.State == System.Data.ConnectionState.Open) conn.Close();
                Console.WriteLine(e.Message);
                return InternalServerError(e);
            }
        }
        //CONTAINER
        //GET Container
        [Route("api/somiod/{appName:maxlength(50)}")]
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
        public IHttpActionResult PostContainer(string appName, Container container)
        {
            //Post of an application
            SqlConnection conn = null;
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
                return InternalServerError(e);
            }
        }

        //PUT Container
        [Route("api/somiod/{appName:maxlength(50)}/{containerName:maxlength(50)}")]
        public IHttpActionResult PutContainer(string appName, string containerName, Container container)
        {
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(strDataConn);
                conn.Open();

                SqlCommand cmd = new SqlCommand("Update Containers SET Name=@name, Creation_dt=@creation_dt, Parent=@parent WHERE Id=@id", conn);

                cmd.Parameters.AddWithValue("@id", container.Id);
                cmd.Parameters.AddWithValue("@name", container.Name);
                cmd.Parameters.AddWithValue("@creation_dt", container.CreationDt);
                cmd.Parameters.AddWithValue("@parent", container.Parent);

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
                if (conn.State == System.Data.ConnectionState.Open) conn.Close();
                Console.WriteLine(e.Message);
                return InternalServerError(e);
            }
        }
        //GET Subscribe and Data names
        //Get all the content in the container
        [Route("api/somiod/{appName:maxlength(50)}/{containerName:maxlength(50)}")]
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
        //DATA TODO
        //GET containers datas
        [Route("api/somiod/{appName:maxlength(50)}/{containerName:maxlength(50)}/data")]
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
        public IHttpActionResult PostData(string containerName, Data data)
        {
            data.CreationDt = DateTime.Now;

            using (SqlConnection conn = new SqlConnection(strDataConn))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("INSERT INTO Data (Name, Creation_dt, Parent, Content) VALUES (@name, @creation_dt, @parent,@content); SELECT SCOPE_IDENTITY();", conn))
                {
                    cmd.Parameters.AddWithValue("@name", data.Name);
                    cmd.Parameters.AddWithValue("@creation_dt", data.CreationDt);
                    cmd.Parameters.AddWithValue("@content", data.Content);

                    int parentId = 0;

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
                            return Ok(data);
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        return InternalServerError(e);
                    }
                }
            }
        }
        //PUT Data
        [Route("api/somiod/{appName:maxlength(50)}/{containerName:maxlength(50)}/data/{dataName:maxlength(50)}")]
        public IHttpActionResult PutData(string containerName, string dataName, Data data)
        {
            data.CreationDt = DateTime.Now;
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(strDataConn);
                conn.Open();

                SqlCommand cmd = new SqlCommand("Update Data SET Name=@name, Creation_dt=@creation_dt,  Content=@content WHERE Name=@dataName", conn);

                cmd.Parameters.AddWithValue("@name", data.Name);
                cmd.Parameters.AddWithValue("@creation_dt", data.CreationDt);

                cmd.Parameters.AddWithValue("@content", data.Content);
                cmd.Parameters.AddWithValue("@dataName", dataName);

                cmd.CommandType = System.Data.CommandType.Text;

                int nrows = cmd.ExecuteNonQuery();
                conn.Close();

                if (nrows > 0)
                {
                    return Ok("Updated: " + dataName);
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
        //DELETE Data
        [Route("api/somiod/{appName:maxlength(50)}/{containerName:maxlength(50)}/data/{dataName:maxlength(50)}")]
        public IHttpActionResult DeleteData(string appName, string dataName)
        {
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(strDataConn);
                conn.Open();

                SqlCommand cmd = new SqlCommand("DELETE FROM Data WHERE Name=@name", conn);
                cmd.Parameters.AddWithValue("@name", dataName);

                cmd.CommandType = System.Data.CommandType.Text;

                int nrows = cmd.ExecuteNonQuery();
                if (nrows > 0) return Ok("Deleted: " + dataName);
                else return NotFound();
            }
            catch (Exception e)
            {
                if (conn.State == System.Data.ConnectionState.Open) conn.Close();
                Console.WriteLine(e.Message);
                return null;
            }
        }
        //SUBSCRIPTION
        //GET SUBSCRIPTION
        [Route("api/somiod/{appName:maxlength(50)}/{containerName:maxlength(50)}/subscription/{subscriptionName:maxlength(50)}")]
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
                        Parent = reader.GetInt32(3)
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
                        Parent = reader.GetInt32(3)
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
        public IHttpActionResult PostSubscription(string appName, string containerName, Subscription subscription)
        {
            //SqlConnection conn = null;
            subscription.CreationDt = DateTime.Now;

            using (SqlConnection conn = new SqlConnection(strDataConn))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("INSERT INTO Subscriptions (Name, Creation_dt, Endpoint, Event, Parent) VALUES (@name, @creation_dt, @endpoint, @event, @parent); SELECT SCOPE_IDENTITY();", conn))
                {
                    cmd.Parameters.AddWithValue("@name", subscription.Name);
                    cmd.Parameters.AddWithValue("@creation_dt", subscription.CreationDt);
                    cmd.Parameters.AddWithValue("@endpoint", subscription.Endpoint);
                    cmd.Parameters.AddWithValue("@event", subscription.Event);

                    int parentId = 0;

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
                        int insertedId = cmd.ExecuteNonQuery();

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
                        Console.WriteLine(e.Message);
                        return InternalServerError(e);
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
        }


        // PUT Subscription
        [Route("api/somiod/{appName:maxlength(50)}/{containerName:maxlength(50)}/subscription/{subscriptionName:maxlength(50)}")]
        public IHttpActionResult PutSubscription(string appName, string containerName, string subscriptionName, Subscription subscription)
        {
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(strDataConn);
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Subscriptions SET Name = @newName WHERE Name = @subscriptionName AND Parent = (SELECT Id FROM Containers WHERE Name = @containerName AND Parent = (SELECT Id FROM Applications WHERE Name = @appName))", conn);
                cmd.Parameters.AddWithValue("@newName", subscription.Name);
                cmd.Parameters.AddWithValue("@subscriptionName", subscriptionName);
                cmd.Parameters.AddWithValue("@appName", appName);
                cmd.Parameters.AddWithValue("@containerName", containerName);
                int nrows = cmd.ExecuteNonQuery();
                if (nrows > 0) return Ok("Updated: " + subscriptionName);
                else return NotFound();
            }
            catch (Exception e)
            {
                if (conn.State == System.Data.ConnectionState.Open) conn.Close();
                Console.WriteLine(e.Message);
                return InternalServerError(e);
            }
        }
        // DELETE Subscription
        [Route("api/somiod/{appName:maxlength(50)}/{containerName:maxlength(50)}/subscription/{subscriptionName:maxlength(50)}")]
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
    }
}