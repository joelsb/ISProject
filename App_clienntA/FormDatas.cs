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
using Data = Somiod.Models.Data;

namespace App_clienntA
{
    public partial class FormDatas : Form
    {
        public FormDatas()
        {
            InitializeComponent();
        }
        private bool editMode = false;
        private string appName = null;
        private string containerName = null;
        private string name = null;
        public FormDatas(string appName, string containerName)
        {
            InitializeComponent();
            this.appName = appName;
            this.containerName = containerName;
        }
        //If the form is called this way it will be in edit mode
        public FormDatas(string appName, string containerName, string name)
        {
            InitializeComponent();
            editMode = true;
            this.Text = "Edit Subscription";
            buttonCreate.Text = "Edit";
            textBoxName.Text = name;
            this.appName = appName;
            this.containerName = containerName;
            this.name = name;
        }

        private void Edit(string newName, string newContent)
        {

            Data data = new Data
            {
                Name = newName,
                Content = newContent
            };

            bool isValidText = !string.IsNullOrEmpty(newName) && char.IsLetter(newName[0]);
            // Check if app is null (this should not happen based on the above initialization)
            if (!isValidText)
            {
                // Log or display an error message
                MessageBox.Show("Error: Application update object is null or start with invalid character.");
                return; // exit the method to avoid further issues
            }
            Network.PUT($"{Network.baseUrl}/{appName}/{containerName}/data/{name}", data);
        }
        private void Create(string newName, string newEvent)
        {

            Data data = new Data
            {
                Content = newEvent,
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
            Network.POST($"{Network.baseUrl}/{appName}/{containerName}/data", data);
        }
        private void buttonCreate_Click(object sender, EventArgs e)
        {
            string newName = textBoxName.Text;
            string newContent = textBoxContent.Text;
            
            if (editMode)
            {
                //Update
                Edit(newName, newContent);
            }
            else
            {
                //Create
                Create(newName, newContent);
            }
            this.Close();
        }
    }
}
