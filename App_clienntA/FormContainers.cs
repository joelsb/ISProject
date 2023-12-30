using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Container = Somiod.Models.Container;

namespace App_clienntA
{
    public partial class FormContainers : Form
    {
        //This means the selected app is being edited
        private bool editMode = false;
        private string name = null;
        private string appName = null;
        public FormContainers(string appname)
        {
            InitializeComponent();
            this.appName = appname;
        }
        //If the form is called this way it will be in edit mode
        public FormContainers(string appname, string name)
        {
            InitializeComponent();
            editMode = true;
            this.name = name;
            this.Text = "Edit App";
            buttonCreate.Text = "Edit";
            textBoxName.Text = name;
            this.appName = appname;
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

            Container container = new Container
            {
                Name = newName,
            };

            bool isValidText = !string.IsNullOrEmpty(newName) && char.IsLetter(newName[0]);
            // Check if app is null (this should not happen based on the above initialization)
            if (!isValidText)
            {
                // Log or display an error message
                MessageBox.Show("Error: Application update object is null or start with invalid character.");
                return; // exit the method to avoid further issues
            }
            Network.PUT($"{Network.baseUrl}/{appName}/{name}", container);
        }
        private void Create(string newName)
        {

            Container container = new Container
            {
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
            Network.POST($"{Network.baseUrl}/{appName}", container);
        }
    }
}
