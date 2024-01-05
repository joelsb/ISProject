using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static App_clienntA.ClientA;
using System.Xml.Serialization;
using Application = Somiod.Models.Application;
using System.Net;

namespace App_clienntA
{
    public partial class FormApps : Form
    {
        //This means the selected app is being edited
        private bool editMode = false;
        private string name = null;

        public FormApps()
        {
            InitializeComponent();
        }
        //If the form is called this way it will be in edit mode
        public FormApps(string name)
        {
            InitializeComponent();
            editMode = true;
            this.name = name;
            this.Text = "Edit App";
            buttonCreate.Text = "Edit";
            textBoxName.Text = name;
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            string newName = textBoxName.Text;
            if (editMode)
            {
                //Update
                Edit(newName);
            }
            else
            {
                //Create
                Create(newName);
            }
            this.Close();
        }

        private void Edit(string newName)
        {
            
            Application appUpdate = new Application
            {
                Name = newName,
            };
            if (appUpdate.Name == "Switch")
            {
                MessageBox.Show("Error: Application name can not be Switch ");
                return;
            }
            bool isValidText = !string.IsNullOrEmpty(newName) && char.IsLetter(newName[0]);
            // Check if app is null (this should not happen based on the above initialization)
            if (!isValidText)
            {
                // Log or display an error message
                MessageBox.Show("Error: Application update object is null or start with invalid character.");
                return; // exit the method to avoid further issues
            }
            Network.PUT($"{Network.baseUrl}/{name}", appUpdate);
        }
        private void Create(string newName)
        {

            Application app = new Application
            {
                Name = newName,
            };

            bool isValidText = !string.IsNullOrEmpty(newName) && char.IsLetter(newName[0]);
            // Check if app is null (this should not happen based on the above initialization)
            if (app.Name== "Switch")
            {
                MessageBox.Show("Error: Application name can not be Switch ");
                return;
            }
            if (!isValidText)
            {
                // Log or display an error message
                MessageBox.Show("Error: Application update object is null or start with invalid character.");
                return; // exit the method to avoid further issues
            }
            Network.POST(Network.baseUrl, app);
        }

        private void FormApps_Load(object sender, EventArgs e)
        {

        }
        //private void POSTNewApp(object sender, EventArgs e)
        //{
        //    XmlSerializer xmlSerializer = new XmlSerializer(typeof(Application), "http://schemas.datacontract.org/2004/07/Somiod.Models");

        //    // Create a new Application object

        //    Application app = new Application
        //    {
        //        Name = textBoxAppName.Text,
        //    };
        //    string name_aux = app.Name;
        //    bool atendeRequisitos = !string.IsNullOrEmpty(name_aux) && char.IsLetter(name_aux[0]);


        //    // Check if app is null (this should not happen based on the above initialization)
        //    if (!atendeRequisitos)
        //    {
        //        // Log or display an error message
        //        richTextBoxShowApp.Text = "Error: Application object is null or start with invalid character.";
        //        return; // exit the method to avoid further issues
        //    }

        //    string xmlContract;

        //    using (StringWriter stringWriter = new Utf8StringWriter()) // Use Utf8StringWriter instead of StringWriter
        //    {
        //        // Use an XmlSerializerNamespaces to specify the namespace
        //        XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();

        //        xmlSerializer.Serialize(stringWriter, app);

        //        xmlContract = stringWriter.ToString();

        //    }



        //    // Send the HTTP POST request
        //    POSTandPUT("POST", baseURL, xmlContract);

        //    richTextBoxShowApp.Text = app.Name + ":Adicionada com sucesso ";
        //    comboBoxParent.DataSource = GETAllAppList();
        //}

    }
}
