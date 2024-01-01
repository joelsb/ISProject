using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO; //Stream
using System.Linq;
using System.Net; //HttpWebRequest
using System.Net.NetworkInformation;
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

        //private string Get(string URL)
        //{
        //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
        //    HttpWebResponse response = (HttpWebResponse)request.GetResponse();

        //    string content = string.Empty;
        //    using (Stream stream = response.GetResponseStream())
        //    {
        //        using (StreamReader sr = new StreamReader(stream))
        //        {
        //            content = sr.ReadToEnd();
        //        }
        //    }
        //    return content;
        //}

        //void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        //{
        //    SubscribeTextBox.BeginInvoke((MethodInvoker)delegate
        //    {
        //        SubscribeTextBox.AppendText(Encoding.UTF8.GetString(e.Message) + Environment.NewLine);
        //    });

        //}

        //private void Subscribe(object sender, EventArgs e)
        //{
        //    mClient.Connect(Guid.NewGuid().ToString());
        //    if (!mClient.IsConnected)
        //    {
        //        MessageBox.Show("Error connecting to broker...");
        //        return;
        //    }

        //    //This client's subscription operation id done 
        //    mClient.MqttMsgPublishReceived += client_MqttMsgPublishReceived;


        //    //Subscribe 
        //    byte[] qosLevels = { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE };//QoS
        //    mClient.Subscribe(mStrSubscription, qosLevels);


        //    if (mClient.IsConnected)
        //        lblStatusA.Text = "Connected";
        //    else
        //        lblStatusA.Text = "Disconnected";

        //}

        //private void Send(object sender, EventArgs e)
        //{

        //    if (!mClient.IsConnected)
        //    {
        //        MessageBox.Show("Application is disconnected");
        //        return;
        //    }



        //    /*if (!ValidateUserInfo())
        //    {
        //        MessageBox.Show("Invalid User Info");
        //        return;
        //    }*/


        //    string Name = textBoxAppName.Text;
        //    DateTime DateTime = DateTime.Now;

        //    String strMsg = SubscribeTextBox.Text;
        //    if (strMsg.Trim().Length <= 0)
        //    {
        //        MessageBox.Show("Invalid message");
        //        return;
        //    }

        //    String strMsgToSend = Name + "|" + DateTime + "|" + strMsg;


        //    mClient.Publish(STR_CHANNEL_NAME, Encoding.UTF8.GetBytes(strMsgToSend));

        //    textBoxAppName.Text = "";
        //    textBoxAppName.Focus();

        //}

        private List<string> GETAllAppList()
        {
            XmlDocument doc = Network.GET(Network.baseUrl);
            // Get the content within the <name> tags
            XmlNodeList nameNodes = doc.GetElementsByTagName("Name");
            List<string> names = new List<string>();
            foreach (XmlNode node in nameNodes)
            {
                names.Add(node.InnerText);
            }
            // Display the names in the rich text box
            return names;

        }
        private void populateApps()
        {
            listBoxApps.Enabled = false;
            listBoxApps.Items.Clear();
            List<string> apps = GETAllAppList();
            foreach (string app in apps)
            {
                listBoxApps.Items.Add(app);
            }
            listBoxApps.SelectedIndex = 0;
            listBoxApps.Enabled = true;
        }
        private List<string> GETAllContainerList(string name)
        {
            XmlDocument doc = Network.GET(Network.baseUrl + "/" + name);
            // Get the content within the <name> tags
            if (doc == null) { return new List<string>(); }
            XmlNodeList nameNodes = doc.GetElementsByTagName("Name");
            List<string> names = new List<string>();
            foreach (XmlNode node in nameNodes)
            {
                names.Add(node.InnerText);
            }
            // Display the names in the rich text box
            return names;

        }
        private void populateContainers()
        {
            listBoxContainers.Enabled = false;
            listBoxContainers.Items.Clear();
            string name = listBoxApps.Items[listBoxApps.SelectedIndex].ToString();
            List<string> containers = GETAllContainerList(name);
            foreach (string container in containers)
            {
                listBoxContainers.Items.Add(container);
            }
            if (listBoxContainers.Items.Count > 0)
            {
                listBoxContainers.SelectedIndex = 0;
            }
            listBoxContainers.Enabled = true;
        }
        private List<string> GETAllSubscriptionList(string appName, string containerName)
        {
            XmlDocument doc = Network.GET($"{Network.baseUrl}/{appName}/{containerName}/subscription");
            // Get the content within the <name> tags
            if (doc == null) { return new List<string>(); }
            XmlNodeList nameNodes = doc.GetElementsByTagName("Name");
            List<string> names = new List<string>();
            foreach (XmlNode node in nameNodes)
            {
                names.Add(node.InnerText);
            }
            // Display the names in the rich text box
            return names;

        }
        private void populateSubscriptions()
        {
            listBoxSubscriptions.Enabled = false;
            listBoxSubscriptions.Items.Clear();
            string appName = listBoxApps.Items[listBoxApps.SelectedIndex].ToString();
            string containerName = listBoxContainers.Items[listBoxContainers.SelectedIndex].ToString();
            List<string> subscriptions = GETAllSubscriptionList(appName, containerName);
            foreach (string subscription in subscriptions)
            {
                listBoxSubscriptions.Items.Add(subscription);
            }
            if (listBoxSubscriptions.Items.Count > 0)
            {
                listBoxSubscriptions.SelectedIndex = 0;
            }
            listBoxSubscriptions.Enabled = true;
        }
        private List<string> GETAllDataList(string appName, string containerName)
        {
            XmlDocument doc = Network.GET($"{Network.baseUrl}/{appName}/{containerName}/data");
            // Get the content within the <name> tags
            if (doc == null) { return new List<string>(); }
            XmlNodeList nameNodes = doc.GetElementsByTagName("Name");
            List<string> names = new List<string>();
            foreach (XmlNode node in nameNodes)
            {
                names.Add(node.InnerText);
            }
            // Display the names in the rich text box
            return names;

        }
        private void populateDatas()
        {
            listBoxSubscriptions.Enabled = false;
            listBoxSubscriptions.Items.Clear();
            string appName = listBoxApps.Items[listBoxApps.SelectedIndex].ToString();
            string containerName = listBoxContainers.Items[listBoxContainers.SelectedIndex].ToString();
            List<string> datas = GETAllDataList(appName, containerName);
            foreach (string data in datas)
            {
                listBoxData.Items.Add(data);
            }
            if (listBoxData.Items.Count > 0)
            {
                listBoxData.SelectedIndex = 0;
            }
            listBoxData.Enabled = true;
        }
        private void ClientA_Load(object sender, EventArgs e)
        {
            populateApps();
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            populateApps();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name = listBoxApps.Items[listBoxApps.SelectedIndex].ToString();
            FormApps formApps = new FormApps(name);
            formApps.ShowDialog();
            populateApps();
        }

        private void listBoxApps_SelectedIndexChanged(object sender, EventArgs e)
        {
            populateContainers();
        }

        private void buttonCreateApps_Click(object sender, EventArgs e)
        {
            FormApps formApps = new FormApps();
            formApps.ShowDialog();
            populateApps();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name = listBoxApps.Items[listBoxApps.SelectedIndex].ToString();
            Network.DELETE($"{Network.baseUrl}/{name}");
            populateApps();
        }

        private void refreshToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            populateContainers();
        }

        private void editToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string name = listBoxContainers.Items[listBoxContainers.SelectedIndex].ToString();
            string appName = listBoxApps.Items[listBoxApps.SelectedIndex].ToString();
            FormContainers formContainers = new FormContainers(appName, name);
            formContainers.ShowDialog();
            populateContainers();
        }

        private void listBoxContainers_SelectedIndexChanged(object sender, EventArgs e)
        {
            populateSubscriptions();
            populateDatas();
        }

        private void buttonCreateContainer_Click(object sender, EventArgs e)
        {
            string appName = listBoxApps.Items[listBoxApps.SelectedIndex].ToString();
            FormContainers formContainers = new FormContainers(appName);
            formContainers.ShowDialog();
            populateContainers();
        }

        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string name = listBoxContainers.Items[listBoxContainers.SelectedIndex].ToString();
            string appName = listBoxApps.Items[listBoxApps.SelectedIndex].ToString();
            Network.DELETE($"{Network.baseUrl}/{appName}/{name}");
            populateApps();
        }

        private void buttonCreateSubscription_Click(object sender, EventArgs e)
        {
            string appName = listBoxApps.Items[listBoxApps.SelectedIndex].ToString();
            string containerName = listBoxContainers.Items[listBoxContainers.SelectedIndex].ToString();
            FormSubscriptions formSubscriptions = new FormSubscriptions(appName, containerName);
            formSubscriptions.ShowDialog();
            populateSubscriptions();
        }

        private void refreshToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            populateSubscriptions();
        }

        private void deleteToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            string name = listBoxSubscriptions.Items[listBoxSubscriptions.SelectedIndex].ToString();
            string containerName = listBoxContainers.Items[listBoxContainers.SelectedIndex].ToString();
            string appName = listBoxApps.Items[listBoxApps.SelectedIndex].ToString();
            Network.DELETE($"{Network.baseUrl}/{appName}/{containerName}/subscription/{name}");
            populateSubscriptions();

        }

        private void refreshToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            populateDatas();
        }

        private void deleteToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            string name = listBoxData.Items[listBoxData.SelectedIndex].ToString();
            string containerName = listBoxContainers.Items[listBoxContainers.SelectedIndex].ToString();
            string appName = listBoxApps.Items[listBoxApps.SelectedIndex].ToString();
            Network.DELETE($"{Network.baseUrl}/{appName}/{containerName}/data/{name}");
            populateDatas();
        }

        private void buttonData_Click(object sender, EventArgs e)
        {
            string appName = listBoxApps.Items[listBoxApps.SelectedIndex].ToString();
            string containerName = listBoxContainers.Items[listBoxContainers.SelectedIndex].ToString();
            FormDatas formDatas = new FormDatas(appName, containerName);
            formDatas.ShowDialog();
            populateDatas();
        }
    }
}
