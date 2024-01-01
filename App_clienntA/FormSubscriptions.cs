using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Subscription = Somiod.Models.Subscription;

namespace App_clienntA
{
    public partial class FormSubscriptions : Form
    {
        public FormSubscriptions()
        {
            InitializeComponent();
        }
        //This means the selected app is being edited
        private bool editMode = false;
        private string appName = null;
        private string containerName = null;
        private string name = null;
        public FormSubscriptions(string appName, string containerName)
        {
            InitializeComponent();
            this.appName = appName;
            this.containerName = containerName;
        }
        //If the form is called this way it will be in edit mode
        public FormSubscriptions(string appName, string containerName, string name)
        {
            InitializeComponent();
            editMode = true;
            this.Text = "Edit Subscription";
            buttonCreate.Text = "Edit";
            textBoxName.Text = name;
            this.appName = appName;
            this.containerName= containerName;
            this.name = name;
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            string newName = textBoxName.Text;
            string newEvent = "";
            if (checkBoxCreate.Checked) newEvent += "1";
            if (checkBoxDelete.Checked) newEvent += "2";
            if (newEvent.Length <= 0)
            {
                MessageBox.Show("A subcription is obligated to notify for creation and/or deletion.");
                return;
            }
            if (editMode)
            {
                //Update
                Edit(newName, newEvent);
            }
            else
            {
                //Create
                Create(newName, newEvent);
            }
            this.Close();
        }

        private void Edit(string newName, string newEvent)
        {

            Subscription subscription = new Subscription
            {
                Name = newName,
                Event = newEvent
            };

            bool isValidText = !string.IsNullOrEmpty(newName) && char.IsLetter(newName[0]);
            // Check if app is null (this should not happen based on the above initialization)
            if (!isValidText)
            {
                // Log or display an error message
                MessageBox.Show("Error: Application update object is null or start with invalid character.");
                return; // exit the method to avoid further issues
            }
            Network.PUT($"{Network.baseUrl}/{appName}/{containerName}/subscription/{name}", subscription);
        }
        private void Create(string newName, string newEvent)
        {

            Subscription subscription = new Subscription
            {
                Event = newEvent,
                Name = newName
            };
            bool isValidText = !string.IsNullOrEmpty(newName) && char.IsLetter(newName[0]);
            // Check if app is null (this should not happen based on the above initialization)
            if (!isValidText)
            {
                // Log or display an error message
                MessageBox.Show("Error: Application update object is null or start with invalid character.");
                return; // exit the method to avoid further issues
            }
            Network.POST($"{Network.baseUrl}/{appName}/{containerName}/subscription", subscription);
        }
    }
}
