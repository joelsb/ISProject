using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using Application = Somiod.Models.Application;
using Container = Somiod.Models.Container;
using Subscription = Somiod.Models.Subscription;

namespace App_clienntA
{
    public partial class ClientA : Form
    {
        string baseURL = @"http://localhost:50611/api/somiod";

        List<Application> applications = new List<Application>();
        Application app;

        List<Container> containers = new List<Container>();
        Container container;

        List<Subscription> subscriptions = new List<Subscription>();
        Subscription subscription;

        MqttClient mClient = new MqttClient(IPAddress.Parse("127.0.0.1")); //OR use the broker hostname
        String[] mStrSubscription = { "TEST" }; //Subscription


        public ClientA()
        {
            InitializeComponent();
        }


        private void ClientA_Load(object sender, EventArgs e)
        {

            mClient.Connect(Guid.NewGuid().ToString());
            if (!mClient.IsConnected)
            {
                MessageBox.Show("Error connecting to broker...");
                return;
            }

            //This client's subscription operation id done 
            mClient.MqttMsgSubscribed += null;


            //Subscribe 
            byte[] qosLevels = { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE };//QoS
            mClient.Subscribe(mStrSubscription, qosLevels);
        }

        private void POSTNewApp(object sender, EventArgs e)
        {
            Application app = new Application();
            app.Id = -1;
            app.Name = textBoxName.Text;
            app.CreationDt = DateTime.Now;


            var bodyStream = new StreamReader(HttpContext.Current.Request.InputStream);
            string xmlContract = bodyStream.ReadToEnd();


            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlContract);

            POSTandPUT("POST", baseURL, xmlContract);

            textBoxOutput.Text = "xnl contract: " + xmlContract + Environment.NewLine;
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
            Application app = new Application();
            textBoxName.Text = app.Name;
            app.CreationDt = DateTime.Now;


            var bodyStream = new StreamReader(HttpContext.Current.Request.InputStream);
            string xmlContract = bodyStream.ReadToEnd();



            POSTandPUT("PUT", baseURL + $"/{app.Name}", xmlContract);

            textBoxOutput.Text = "xnl contract: " + xmlContract + Environment.NewLine;

        }
        private void DELApp(object sender, EventArgs e)
        {

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(app.Name);
            request.Method = "delete";
            request.ContentType = @"application/xml";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            MessageBox.Show(response.StatusCode.ToString());
            response.Close();
        }

        static string ShowApplicationInfo(Application app)
        {
            return string.Format("{0} : \t{1} \t{2} \t{3}", app.Id, app.Name, app.CreationDt);
        }


        private void GETAllApp(object sender, EventArgs e)
        {
            string content = Get(baseURL);
            richTextBoxShowApp.Text = "";

            applications = applications;
            foreach (Application item in applications)
            {
                richTextBoxShowApp.AppendText(ShowApplicationInfo(item) + Environment.NewLine);
            }

        }

        private void POSTNewContainer(object sender, EventArgs e)
        {

            Container container = new Container();
            container.Id = -1;
            container.Name = textBoxName.Text;
            container.CreationDt = DateTime.Now;
            container.Parent = app.Id;



            var bodyStream = new StreamReader(HttpContext.Current.Request.InputStream);
            string xmlContract = bodyStream.ReadToEnd();


            POSTandPUT("POST", baseURL, xmlContract);

            textBoxOutput.Text = "xnl contract: " + xmlContract + Environment.NewLine;
        }

        private void UPDATEContainer(object sender, EventArgs e)
        {
            Container container = new Container();
            container.Id = -1;
            textBoxName.Text = container.Name;
            container.CreationDt = DateTime.Now;
            app.Id = container.Parent;

            var bodyStream = new StreamReader(HttpContext.Current.Request.InputStream);
            string xmlContract = bodyStream.ReadToEnd();

            POSTandPUT("PUT", baseURL + $"/{container.Id}", xmlContract);
            textBoxOutput.Text = "xnl contract: " + xmlContract + Environment.NewLine;
        }

        private void DELContainer(object sender, EventArgs e)
        {

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(container.Name);
            request.Method = "delete";
            request.ContentType = @"application/xml";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            MessageBox.Show(response.StatusCode.ToString());
            response.Close();
        }

        static string ShowContainernInfo(Container container)
        {
            return string.Format("{0} : \t{1} \t{2} \t{3} \t{4}", container.Id, container.Name, container.CreationDt,container.Parent);
        }

        private void GETAllContainer(object sender, EventArgs e)
        {
            string content = Get(baseURL);
            richTextShowContainer.Text = "";

            //Container = Containers;
            foreach (Container item in containers)
            {
                richTextShowContainer.AppendText(ShowContainernInfo(item) + Environment.NewLine);
            }

        }


        private void POSTNewSubcription(object sender, EventArgs e)
        {

            Subscription subscription = new Subscription();
            subscription.Id = -1;
            subscription.Name = textBoxName.Text;
            subscription.CreationDt = DateTime.Now;
            subscription.Parent = container.Id;



            var bodyStream = new StreamReader(HttpContext.Current.Request.InputStream);
            string xmlContract = bodyStream.ReadToEnd();


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

    }
}
