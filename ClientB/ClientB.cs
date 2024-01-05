using App_clienntA;
using Somiod.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using Application = Somiod.Models.Application;
using Container = Somiod.Models.Container;
using Subscription = Somiod.Models.Subscription;

namespace ClientB
{
    public partial class ClientB : Form
    {

        public static string baseUrl = @"http://localhost:44382/api/somiod";
        MqttClient client = new MqttClient("127.0.0.1");
        string[] topics = { "asdasdas" };
        public ClientB()
        {
            InitializeComponent();
        }

        private void comboBoxTopics_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
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
            listBoxApps.Items.Remove("Switch");
            if (listBoxApps.SelectedIndex>0)
            {
                listBoxApps.SelectedIndex = 0;
            }
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
        private int GETAllDatasList(string appName, string containerName)
        {
            int count = 0;
            XmlDocument doc = Network.GET(Network.baseUrl + "/" + appName + "/" + containerName + "/data");
            // Get the content within the <name> tags
            if (doc == null) { return 0; }
            XmlNodeList nameNodes = doc.GetElementsByTagName("Name");
            List<string> names = new List<string>();
            foreach (XmlNode node in nameNodes)
            {
                names.Add(node.InnerText);
            }
            count = names.Count;
            // Display the names in the rich text box
            return count;
        }
        private void populateContainers()
        {
            listBoxContainers.Enabled = false;
            listBoxContainers.Items.Clear();

            // Check if an item is selected in listBoxApps
            if (listBoxApps.SelectedIndex != -1)
            {
                string name = listBoxApps.Items[listBoxApps.SelectedIndex].ToString();
                List<string> containers = GETAllContainerList(name);

                foreach (string container in containers)
                {
                    listBoxContainers.Items.Add(container);
                }

                listBoxContainers.Items.Remove("Switch");

                if (listBoxContainers.Items.Count > 0)
                {
                    listBoxContainers.SelectedIndex = 0;
                }
            }

            listBoxContainers.Enabled = true;
        }
        private void ClientB_Load(object sender, EventArgs e)
        {
            Application app = new Application
            {
                Name = "Switch",
            };
            Container container = new Container
            {
                Name = "Switch",
            };
            if (Network.GET(baseUrl+"/Switch") is null)
            {
                Network.POST(Network.baseUrl, app);
                Network.POST(Network.baseUrl+ "/Switch", container);
            }
            populateApps();
            populateContainers();
            
        }
        private void Client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            string msg = Encoding.UTF8.GetString(e.Message);
            string topic = e.Topic;
            //MessageBox.Show($"Recebi em {topic} --> {msg}");

            this.Invoke((MethodInvoker)delegate
            {
                
            });


        }
        private void FormSub_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (client.IsConnected)
            {
                client.Unsubscribe(topics);
                client.Disconnect();
            }
        }

        private void listBoxApps_SelectedIndexChanged(object sender, EventArgs e)
        {
            populateContainers();
        }

        private void richTextBoxMessages_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonSubscribe_Click(object sender, EventArgs e)
        {
            client.Connect(Guid.NewGuid().ToString());
            if (!client.IsConnected)
            {
                MessageBox.Show("Não foi possivel ligar ao broker");
            }

            client.MqttMsgPublishReceived += Client_MqttMsgPublishReceived;

            byte[] qosLevels = { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE };
            client.Subscribe(topics, qosLevels);
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            string appName = listBoxApps.Items[listBoxApps.SelectedIndex].ToString();
            string containerName = listBoxContainers.Items[listBoxContainers.SelectedIndex].ToString();
            int number = GETAllDatasList(appName, containerName);
            Random rnd = new Random();
            int num = rnd.Next();
            Data data = new Data
            {
                Content = "Open",
                Name = "Open" + appName + number + num
            };

            Network.POST($"{Network.baseUrl}/{appName}/{containerName}/data", data);
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            string appName = listBoxApps.Items[listBoxApps.SelectedIndex].ToString();
            string containerName = listBoxContainers.Items[listBoxContainers.SelectedIndex].ToString();
            int number = GETAllDatasList(appName, containerName);
            Random rnd = new Random();
            int num = rnd.Next();
            Data data = new Data
            {
                Content = "Close",
                Name = "Close" + appName + number + num
            };

            Network.POST($"{Network.baseUrl}/{appName}/{containerName}/data", data);
        }
    }
}
