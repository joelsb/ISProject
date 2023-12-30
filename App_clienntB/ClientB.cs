using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.IO; //Stream
using System.Linq;
using System.Net; //HttpWebRequest
using System.Runtime.ConstrainedExecution;
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

namespace App_clienntB
{
    public partial class ClientB : Form
    {
        string baseURL = @"http://localhost:44382/api/somiod";

        const String CHANNEL_NAME = "/application/content";

        List<Application> applications = new List<Application>();
        Application app;

        List<Container> containers = new List<Container>();
        Container container;

        List<Subscription> subscriptions = new List<Subscription>();
        Subscription subscription;

        
        MqttClient mClient = new MqttClient(IPAddress.Parse("127.0.0.1")); //OR use the broker hostname
        String[] mStrSubscription = { CHANNEL_NAME }; //Subscription


        public ClientB()
        {
            InitializeComponent();
        }



        private void POSTNewApp(object sender, EventArgs e)
        {

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Application));

            // Create a new Application object

            Application app = new Application
            {
                Name = textBoxName.Text,
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
            using (StringWriter writer = new StringWriter())
            {
                xmlSerializer.Serialize(writer, app);
                xmlContract = writer.ToString();
            }

            // Send the HTTP POST request
            POSTandPUT("POST", baseURL, xmlContract);
            richTextBoxShowApp.Text = app.Name + ":Adicionada com sucesso "; textBoxOutput.Text = "xnl contract: " + xmlContract + Environment.NewLine;      
            
                      
            /*
            System.Text.UTF8Encoding encoding = new UTF8Encoding();
            byte[] byteArray = encoding.GetBytes(xml);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseURI);
            request.Method = "post"; //add new
            request.ContentLength = byteArray.Length;
            request.ContentType = @"application/xnl";

            Stream data = request.GetRequestStream();
            data.Write(byteArray, 0, byteArray.Length);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            MessageBox.Show(response.ContentLength.ToString());
            response.Close();*/

        }

        private void UPDATEApp(object sender, EventArgs e)
        {

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Application));
            Application app = new Application
            {
                Name = textBoxName.Text,
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
            using (StringWriter writer = new StringWriter())
            {
                xmlSerializer.Serialize(writer, app);
                xmlContract = writer.ToString();
            }

            
            POSTandPUT("PUT", baseURL + $"/{app.Name}", xmlContract);
            textBoxOutput.Text = "xnl contract: " + xmlContract + Environment.NewLine;

        }
        private void DELApp(object sender, EventArgs e)
        {
            Application app = new Application
            { Name = textBoxName.Text };
            if (app == null)
            {
                // Log or display an error message
                richTextBoxShowApp.Text = "Error: Application object is null.";
                return; // exit the method to avoid further issues
            }


            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseURL + "/" + app.Name);
            request.Method = "DELETE";
            request.ContentType = @"application/xml";
            try
            {
                long length = 0;
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    length = response.ContentLength;
                }
                //comboBoxParent.DataSource = GETAllApp();
            }
            catch (WebException ex)
            {
                // Log exception and throw as for GET example above
                MessageBox.Show(ex.Message + "App does not exists");
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

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Container));

            // Create a new container object

            Container container = new Container
            {
                Id = -1,
                Name = textBoxNameContainer.Text,
                CreationDt = DateTime.Now,
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

            using (StringWriter writer = new StringWriter())
            {
                xmlSerializer.Serialize(writer, container);
                xmlContract = writer.ToString();
            }

            POSTandPUT("POST", baseURL, xmlContract);
            textBoxOutput.Text = "xnl contract: " + xmlContract + Environment.NewLine;
        }

        private void UPDATEContainer(object sender, EventArgs e)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Container));
            Container container = new Container
            {
                Id = -1,
                Name = textBoxNameContainer.Text,
                CreationDt = DateTime.Now,
            };
            string name_aux = container.Name;
            bool atendeRequisitos = !string.IsNullOrEmpty(name_aux) && char.IsLetter(name_aux[0]);


            // Check if Container  is null (this should not happen based on the above initialization)
            if (!atendeRequisitos)
            {
                // Log or display an error message
                richTextBoxShowApp.Text = "Error: Container object is null or start with invalid character.";
                return; // exit the method to avoid further issues
            }

            string xmlContract;
            using (StringWriter writer = new StringWriter())
            {
                xmlSerializer.Serialize(writer, container);
                xmlContract = writer.ToString();
            }


            POSTandPUT("POST", baseURL + $"/{container.Id}", xmlContract);
            textBoxOutput.Text = "xnl contract: " + xmlContract + Environment.NewLine;
        }

        private void DELContainer(object sender, EventArgs e)
        {
            Container container = new Container { Name = textBoxName.Text };
            if (container == null)
            {
                // Log or display an error message
                richTextShowContainer.Text = "Error: Container object is null.";
                return; // exit the method 
            }


            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseURL + "/" + container.Id + "/" + container.Name);
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
            Container container = new Container();
            if (container.Id == 0)
            {
                richTextShowContainer.Text = "Error: container  object is null.";
            }
            else
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseURL + "/" + container.Id);
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

                            // Get the content within the <name> tagsz
                            XmlNodeList nameNodes = xmlDoc.GetElementsByTagName("Id");
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

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Subscription));
            Subscription subscription = new Subscription {
            Id = -1,
            Name = textBoxName.Text,
            CreationDt = DateTime.Now,
            Parent = container.Id,
        }; 
            string name_aux = subscription.Name;
            bool atendeRequisitos = !string.IsNullOrEmpty(name_aux) && char.IsLetter(name_aux[0]);


            // Check if app is null (this should not happen based on the above initialization)
            if (!atendeRequisitos)
            {
                // Log or display an error message
                richTextBoxShowApp.Text = "Error: subscription object is null or start with invalid character.";
                return; // exit the method to avoid further issues
            }


            string xmlContract;
            using (StringWriter writer = new StringWriter())
            {
                xmlSerializer.Serialize(writer, subscription);
                xmlContract = writer.ToString();
            }


            POSTandPUT("POST", baseURL, xmlContract);
            textBoxOutput.Text = "xnl contract: " + xmlContract + Environment.NewLine;

        }


        private void POSTandPUT(string method, string url, string xmlContract)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = method; //"POST" or "PUT"

            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            Byte[] byteArray = encoding.GetBytes(xmlContract);

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
                    MessageBox.Show(length.ToString());
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

            request.Headers.Add("applications", "somiod-discover");
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

           
            mClient.MqttMsgPublishReceived += client_MqttMsgPublishReceived;


            //Subscribe 
            byte[] qosLevels = { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE };//QoS
            mClient.Subscribe(mStrSubscription, qosLevels);

             SubscribeTextBox.Text = "Ola TIAGO";

            if (mClient.IsConnected)
                lblStatusA.Text = "Connected";
            else
                lblStatusA.Text = "Disconnected";

        }

/*       private void Send(object sender, EventArgs e)
        {

            if (!mClient.IsConnected)
            {
                MessageBox.Show("Application is disconnected");
                return;
            }



            if (!ValidateUserInfo())
            {
                MessageBox.Show("Invalid User Info");
                return;
            }


            string Name = textBoxName.Text;
            DateTime DateTime = DateTime.Now;

            String strMsg = SubscribeTextBox.Text;
            if (strMsg.Trim().Length <= 0)
            {
                MessageBox.Show("Invalid message");
                return;
            }

            String strMsgToSend = Name + "|" + DateTime + "|" + strMsg;


            mClient.Publish(CHANNEL_NAME, Encoding.UTF8.GetBytes(strMsgToSend));

            textBoxName.Text = "";
            textBoxName.Focus();

        }


        private Boolean ValidateUserInfo()
        {
            String strTemp = textBoxName.Text;
            if (strTemp.Trim().Length <= 0)
            {
                return false;
            }
            if (!File.Exists(strTemp))
            {
                return false;
            }

            return true;
        }*/


    }
}
