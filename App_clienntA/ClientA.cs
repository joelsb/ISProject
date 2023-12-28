using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO; //Stream
using System.Linq;
using System.Net; //HttpWebRequest
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using Application = Somiod.Models.Application;
using Container = Somiod.Models.Container;
using Subscription = Somiod.Models.Subscription;

namespace App_clienntA
{
    public partial class ClientA : Form
    {
        string baseURL = @"http://localhost:44382/api/somiod";

        List<Application> applications = new List<Application>();
        Application app;

        List<Container> containers = new List<Container>();
        Container container;

        List<Subscription> subscriptions = new List<Subscription>();
        Subscription subscription;

        const String STR_CHANNEL_NAME = "";

        MqttClient mClient = new MqttClient(IPAddress.Parse("127.0.0.1")); //OR use the broker hostname
        String[] mStrSubscription = { STR_CHANNEL_NAME }; //Subscription


        public ClientA()
        {
            InitializeComponent();
        }


        public class Utf8StringWriter : StringWriter
        {
            public override Encoding Encoding => Encoding.UTF8;
        }

        // ...

        private void POSTNewApp(object sender, EventArgs e)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Application), "http://schemas.datacontract.org/2004/07/Somiod.Models");
            
            // Create a new Application object

            Application app = new Application
            {
                Name = textBoxAppName.Text,
            };
            string name_aux = app.Name;
            bool atendeRequisitos = !string.IsNullOrEmpty(name_aux) && char.IsLetter(name_aux[0]);


            // Check if app is null (this should not happen based on the above initialization)
            if (!atendeRequisitos)
            {
                // Log or display an error message
                richTextBoxShowApp.Text = "Error: Application object is null or start with invalid character.";
                return; // exit the method to avoid further issues
            }

            string xmlContract;

            using (StringWriter stringWriter = new Utf8StringWriter()) // Use Utf8StringWriter instead of StringWriter
            {
                // Use an XmlSerializerNamespaces to specify the namespace
                XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                
                xmlSerializer.Serialize(stringWriter, app);
                
                xmlContract = stringWriter.ToString();
                
            }
            
            

            // Send the HTTP POST request
            POSTandPUT("POST", baseURL, xmlContract);

            richTextBoxShowApp.Text =app.Name + ":Adicionada com sucesso ";
            comboBoxParent.DataSource = GETAllAppList();
        }
        private void UPDATEApp(object sender, EventArgs e)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Application), "http://schemas.datacontract.org/2004/07/Somiod.Models");
            Application app = new Application
            {
                Name = textBoxAppName.Text,
            };
            string name_aux = app.Name;
            bool atendeRequisitos = !string.IsNullOrEmpty(name_aux) && char.IsLetter(name_aux[0]);


            // Check if app is null (this should not happen based on the above initialization)
            if (!atendeRequisitos)
            {
                // Log or display an error message
                richTextBoxShowApp.Text = "Error: Application object is null or start with invalid character.";
                return; // exit the method to avoid further issues
            }
            Application appUpdate = new Application
            {
                Name = textBoxUpdateNameApp.Text,
            };
            name_aux = appUpdate.Name;
            atendeRequisitos = !string.IsNullOrEmpty(name_aux) && char.IsLetter(name_aux[0]);


            // Check if app is null (this should not happen based on the above initialization)
            if (!atendeRequisitos)
            {
                // Log or display an error message
                richTextBoxShowApp.Text = "Error: Application update object is null or start with invalid character.";
                return; // exit the method to avoid further issues
            }
            string xmlContract;

            using (StringWriter stringWriter = new Utf8StringWriter()) // Use Utf8StringWriter instead of StringWriter
            {
                // Use an XmlSerializerNamespaces to specify the namespace
                XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();

                xmlSerializer.Serialize(stringWriter, appUpdate);

                xmlContract = stringWriter.ToString();

            }

            POSTandPUT("PUT", baseURL + "/" + app.Name, xmlContract);
            comboBoxParent.DataSource = GETAllAppList();

            richTextBoxShowApp.Text = "xnl contract: " + xmlContract + Environment.NewLine;

        }
        private void DELApp(object sender, EventArgs e)
        {
            Application app = new Application { Name = textBoxAppName.Text };
            if (app == null)
            {
                // Log or display an error message
                richTextBoxShowApp.Text = "Error: Application object is null.";
                return; // exit the method to avoid further issues
            }
            
            
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseURL +"/"+ app.Name);
            request.Method = "DELETE";
            request.ContentType = @"application/xml";
            try
            {
                long length = 0;
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    length = response.ContentLength;
                }
                comboBoxParent.DataSource = GETAllAppList();
            }
            catch (WebException ex)
            {
                // Log exception and throw as for GET example above
                MessageBox.Show(ex.Message +  "App does not exists");
            }
        }
        
        static string ShowApplicationInfo(Application app)
        {
            return string.Format("{0} : \t{1} \t{2} \t{3}", app.Id, app.Name, app.CreationDt);
        }


        private void GETAllApp(object sender, EventArgs e)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseURL);
            request.Method = "GET";
            request.ContentType = @"application/xml";
            
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.Load(responseStream);

                        // Get the content within the <name> tags
                        XmlNodeList nameNodes = xmlDoc.GetElementsByTagName("Name");
                        List<string> names = new List<string>();
                        foreach (XmlNode node in nameNodes)
                        {
                            names.Add(node.InnerText);
                            
                        }
                        // Display the names in the rich text box
                        richTextBoxShowApp.Text = string.Join(",\n", names);
                        
                    }
                }
            }
            catch (WebException ex)
            {
                // Log exception and throw as for GET example above
                MessageBox.Show(ex.Message + "Não existem apps");
            }

        }

        private void POSTNewContainer(object sender, EventArgs e)
        {

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Container), "http://schemas.datacontract.org/2004/07/Somiod.Models");

            // Create a new Application object

            Container container = new Container()
            {
                Name = textBoxContainerName.Text
            };

            // Check if app is null (this should not happen based on the above initialization)
            string name_aux = container.Name;
            bool atendeRequisitos = !string.IsNullOrEmpty(name_aux) && char.IsLetter(name_aux[0]);
            if (!atendeRequisitos)
            {
                // Log or display an error message
                richTextShowContainer.Text = "Error: Container object is null or start with invalid character.";
                return; // exit the method to avoid further issues
            }

            string xmlContract;

            using (StringWriter stringWriter = new Utf8StringWriter()) // Use Utf8StringWriter instead of StringWriter
            {
                // Use an XmlSerializerNamespaces to specify the namespace
                XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();

                xmlSerializer.Serialize(stringWriter, container);

                xmlContract = stringWriter.ToString();

            }

            

            // Send the HTTP POST request
            POSTandPUT("POST", baseURL+"/"+comboBoxParent.Text, xmlContract);
            richTextShowContainer.Text = container.Name + ":Adicionada com sucesso ";
        }

        //private void UPDATEContainer(object sender, EventArgs e)
        //{
        //    Container container = new Container();
        //    container.Id = -1;
        //        textBoxContainerName.Text = container.Name;
        //    container.CreationDt = DateTime.Now;
        //    app.Id = container.Parent;

        //    var bodyStream = new StreamReader(HttpContext.Current.Request.InputStream);
        //    string xmlContract = bodyStream.ReadToEnd();

        //    POSTandPUT("PUT", baseURL + $"/{container.Id}", xmlContract);
        //    richTextShowContainer.Text = "xnl contract: " + xmlContract + Environment.NewLine;
        //}

        private void DELContainer(object sender, EventArgs e)
        {
            Container container = new Container { Name = textBoxContainerName.Text };
            if (container == null)
            {
                // Log or display an error message
                richTextShowContainer.Text = "Error: Application object is null.";
                return; // exit the method to avoid further issues
            }


            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseURL + "/" + comboBoxParent.Text + "/" + container.Name);
            request.Method = "DELETE";
            request.ContentType = @"application/xml";
            try
            {
                long length = 0;
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    length = response.ContentLength;
                }
                
            }
            catch (WebException ex)
            {
                // Log exception and throw as for GET example above
                MessageBox.Show(ex.Message + "Container does not exists");
            }
        }
        
        

        static string ShowContainernInfo(Container container)
        {
            return string.Format("{0} : \t{1} \t{2} \t{3} \t{4}", container.Id, container.Name, container.CreationDt, container.Parent);
        }

        private void GETAllContainer(object sender, EventArgs e)
        {
            if (comboBoxParent.Text == string.Empty)
            {
                richTextShowContainer.Text = "Error: Application object is null.";
            }
            else
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseURL+"/"+ comboBoxParent.Text);
                request.Method = "GET";
                request.ContentType = @"application/xml";
                try
                {
                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        using (Stream responseStream = response.GetResponseStream())
                        {
                            XmlDocument xmlDoc = new XmlDocument();
                            xmlDoc.Load(responseStream);

                            // Get the content within the <name> tags
                            XmlNodeList nameNodes = xmlDoc.GetElementsByTagName("Name");
                            List<string> names = new List<string>();
                            foreach (XmlNode node in nameNodes)
                            {
                                names.Add(node.InnerText);

                            }
                            // Display the names in the rich text box
                            richTextShowContainer.Text = string.Join(",\n", names);

                        }
                    }
                }
                catch (WebException ex)
                {
                    // Log exception and throw as for GET example above
                    MessageBox.Show(ex.Message + "Não existem containers nessa app");
                }
            }
        }


        private void POSTNewSubcription(object sender, EventArgs e)
        {

            Subscription subscription = new Subscription();
            subscription.Id = 7;
            subscription.Name = textBoxAppName.Text;
            subscription.CreationDt = DateTime.Now;
            subscription.Parent = container.Id;



            var bodyStream = new StreamReader(HttpContext.Current.Request.InputStream);
            string xmlContract = bodyStream.ReadToEnd();


            POSTandPUT("POST", baseURL, xmlContract);

            SubscribeTextBox.Text = "xnl contract: " + xmlContract + Environment.NewLine;

        }


        private void POSTandPUT(string method, string url, string xmlContract)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = method; //"POST" or "PUT"

            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();

            // Adiciona a declaração XML manualmente ao início do XMLContract
            string xmlWithDeclaration = $"{xmlContract}";

            Byte[] byteArray = encoding.GetBytes(xmlWithDeclaration);
            request.ContentLength = byteArray.Length;
            request.ContentType = @"application/xml";
            
            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
                
            }

            long length = 0;

            try
            {
                
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    length = response.ContentLength;
                }
            }
            catch (WebException ex)
            {
                // Log exception and throw as for GET example above
                MessageBox.Show(ex.Message);
            }
        }


        private string Get(string URL)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            
            string content = string.Empty;
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader sr = new StreamReader(stream))
                {
                    content = sr.ReadToEnd();
                }
            }
            return content;
        }

        void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            SubscribeTextBox.BeginInvoke((MethodInvoker)delegate
            {
                SubscribeTextBox.AppendText(Encoding.UTF8.GetString(e.Message) + Environment.NewLine);
            });

        }

        private void Subscribe(object sender, EventArgs e)
        {
            mClient.Connect(Guid.NewGuid().ToString());
            if (!mClient.IsConnected)
            {
                MessageBox.Show("Error connecting to broker...");
                return;
            }

            //This client's subscription operation id done 
            mClient.MqttMsgPublishReceived += client_MqttMsgPublishReceived;


            //Subscribe 
            byte[] qosLevels = { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE };//QoS
            mClient.Subscribe(mStrSubscription, qosLevels);


            if (mClient.IsConnected)
                lblStatusA.Text = "Connected";
            else
                lblStatusA.Text = "Disconnected";

        }

        private void Send(object sender, EventArgs e)
        {

            if (!mClient.IsConnected)
            {
                MessageBox.Show("Application is disconnected");
                return;
            }



            /*if (!ValidateUserInfo())
            {
                MessageBox.Show("Invalid User Info");
                return;
            }*/


            string Name = textBoxAppName.Text;
            DateTime DateTime = DateTime.Now;

            String strMsg = SubscribeTextBox.Text;
            if (strMsg.Trim().Length <= 0)
            {
                MessageBox.Show("Invalid message");
                return;
            }

            String strMsgToSend = Name + "|" + DateTime + "|" + strMsg;


            mClient.Publish(STR_CHANNEL_NAME, Encoding.UTF8.GetBytes(strMsgToSend));

            textBoxAppName.Text = "";
            textBoxAppName.Focus();

        }

        
        private List<string> GETAllAppList()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseURL);
            request.Method = "GET";
            request.ContentType = @"application/xml";
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.Load(responseStream);

                        // Get the content within the <name> tags
                        XmlNodeList nameNodes = xmlDoc.GetElementsByTagName("Name");
                        List<string> names = new List<string>();
                        foreach (XmlNode node in nameNodes)
                        {
                            names.Add(node.InnerText);

                        }
                        // Display the names in the rich text box
                        return names;

                    }
                }
            }
            catch (WebException ex)
            {
                // Log exception and throw as for GET example above
                return null;
            }

        }
        private List<string> GETAllContainerList(string parent)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseURL+ "/" + parent);
            request.Method = "GET";
            request.ContentType = @"application/xml";
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.Load(responseStream);

                        // Get the content within the <name> tags
                        XmlNodeList nameNodes = xmlDoc.GetElementsByTagName("Name");
                        List<string> names = new List<string>();
                        foreach (XmlNode node in nameNodes)
                        {
                            names.Add(node.InnerText);

                        }
                        // Display the names in the rich text box
                        return names;

                    }
                }
            }
            catch (WebException ex)
            {
                // Log exception and throw as for GET example above
                return null;
            }

        }

        private void ClientA_Load(object sender, EventArgs e)
        {
            comboBoxParent.DataSource = GETAllAppList();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void UpdateContainer_Click(object sender, EventArgs e)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Container), "http://schemas.datacontract.org/2004/07/Somiod.Models");
            Application app = new Application()
            {
                Name = comboBoxParent.Text,
            };
            Container container = new Container
            {
                Name = textBoxContainerName.Text,
            };
            string name_aux = container.Name;
            bool atendeRequisitos = !string.IsNullOrEmpty(name_aux) && char.IsLetter(name_aux[0]);


            // Check if app is null (this should not happen based on the above initialization)
            if (!atendeRequisitos)
            {
                // Log or display an error message
                richTextShowContainer.Text = "Error: Container object is null or start with invalid character.";
                return; // exit the method to avoid further issues
            }
            Container containerUpdate = new Container
            {
                Name = textBoxUpdateNameContainer.Text,
            };
            name_aux = containerUpdate.Name;
            atendeRequisitos = !string.IsNullOrEmpty(name_aux) && char.IsLetter(name_aux[0]);


            // Check if app is null (this should not happen based on the above initialization)
            if (!atendeRequisitos)
            {
                // Log or display an error message
                richTextShowContainer.Text = "Error: Application update object is null or start with invalid character.";
                return; // exit the method to avoid further issues
            }
            string xmlContract;

            using (StringWriter stringWriter = new Utf8StringWriter()) // Use Utf8StringWriter instead of StringWriter
            {
                // Use an XmlSerializerNamespaces to specify the namespace
                XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();

                xmlSerializer.Serialize(stringWriter, containerUpdate);

                xmlContract = stringWriter.ToString();

            }

            POSTandPUT("PUT", baseURL + "/" + app.Name + "/"+ container.Name, xmlContract);
            comboBoxParent.DataSource = GETAllAppList();

            
        }

        private void comboBoxParentChange_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
