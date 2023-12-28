namespace App_clienntA
{
    partial class ClientA
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.Button button2;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientA));
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxAppName = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.richTextShowContainer = new System.Windows.Forms.RichTextBox();
            this.button9 = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.button10 = new System.Windows.Forms.Button();
            this.SubscribeTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblStatusA = new System.Windows.Forms.Label();
            this.button11 = new System.Windows.Forms.Button();
            this.comboBoxParent = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.richTextBoxShowApp = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxContainerName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxUpdateNameApp = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxUpdateNameContainer = new System.Windows.Forms.TextBox();
            this.UpdateContainer = new System.Windows.Forms.Button();
            button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button2
            // 
            resources.ApplyResources(button2, "button2");
            button2.Name = "button2";
            button2.UseVisualStyleBackColor = true;
            button2.Click += new System.EventHandler(this.UPDATEApp);
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.POSTNewApp);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // textBoxAppName
            // 
            resources.ApplyResources(this.textBoxAppName, "textBoxAppName");
            this.textBoxAppName.Name = "textBoxAppName";
            // 
            // button3
            // 
            resources.ApplyResources(this.button3, "button3");
            this.button3.Name = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.POSTNewContainer);
            // 
            // button4
            // 
            resources.ApplyResources(this.button4, "button4");
            this.button4.Name = "button4";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.POSTNewSubcription);
            // 
            // button6
            // 
            resources.ApplyResources(this.button6, "button6");
            this.button6.Name = "button6";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.DELApp);
            // 
            // button7
            // 
            resources.ApplyResources(this.button7, "button7");
            this.button7.Name = "button7";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.GETAllApp);
            // 
            // button8
            // 
            resources.ApplyResources(this.button8, "button8");
            this.button8.Name = "button8";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.DELContainer);
            // 
            // richTextShowContainer
            // 
            resources.ApplyResources(this.richTextShowContainer, "richTextShowContainer");
            this.richTextShowContainer.Name = "richTextShowContainer";
            // 
            // button9
            // 
            resources.ApplyResources(this.button9, "button9");
            this.button9.Name = "button9";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.GETAllContainer);
            // 
            // lblStatus
            // 
            resources.ApplyResources(this.lblStatus, "lblStatus");
            this.lblStatus.Name = "lblStatus";
            // 
            // button10
            // 
            resources.ApplyResources(this.button10, "button10");
            this.button10.Name = "button10";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.Subscribe);
            // 
            // SubscribeTextBox
            // 
            resources.ApplyResources(this.SubscribeTextBox, "SubscribeTextBox");
            this.SubscribeTextBox.Name = "SubscribeTextBox";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // lblStatusA
            // 
            resources.ApplyResources(this.lblStatusA, "lblStatusA");
            this.lblStatusA.Name = "lblStatusA";
            // 
            // button11
            // 
            resources.ApplyResources(this.button11, "button11");
            this.button11.Name = "button11";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.Send);
            // 
            // comboBoxParent
            // 
            this.comboBoxParent.FormattingEnabled = true;
            resources.ApplyResources(this.comboBoxParent, "comboBoxParent");
            this.comboBoxParent.Name = "comboBoxParent";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // richTextBoxShowApp
            // 
            resources.ApplyResources(this.richTextBoxShowApp, "richTextBoxShowApp");
            this.richTextBoxShowApp.Name = "richTextBoxShowApp";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // textBoxContainerName
            // 
            resources.ApplyResources(this.textBoxContainerName, "textBoxContainerName");
            this.textBoxContainerName.Name = "textBoxContainerName";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // textBoxUpdateNameApp
            // 
            resources.ApplyResources(this.textBoxUpdateNameApp, "textBoxUpdateNameApp");
            this.textBoxUpdateNameApp.Name = "textBoxUpdateNameApp";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // textBoxUpdateNameContainer
            // 
            resources.ApplyResources(this.textBoxUpdateNameContainer, "textBoxUpdateNameContainer");
            this.textBoxUpdateNameContainer.Name = "textBoxUpdateNameContainer";
            this.textBoxUpdateNameContainer.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // UpdateContainer
            // 
            resources.ApplyResources(this.UpdateContainer, "UpdateContainer");
            this.UpdateContainer.Name = "UpdateContainer";
            this.UpdateContainer.UseVisualStyleBackColor = true;
            this.UpdateContainer.Click += new System.EventHandler(this.UpdateContainer_Click);
            // 
            // ClientA
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.UpdateContainer);
            this.Controls.Add(this.textBoxUpdateNameContainer);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxUpdateNameApp);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxContainerName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.richTextBoxShowApp);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBoxParent);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.lblStatusA);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.SubscribeTextBox);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.richTextShowContainer);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(button2);
            this.Controls.Add(this.textBoxAppName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Name = "ClientA";
            this.Load += new System.EventHandler(this.ClientA_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxAppName;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.RichTextBox richTextShowContainer;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.TextBox SubscribeTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblStatusA;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.ComboBox comboBoxParent;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox richTextBoxShowApp;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxContainerName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxUpdateNameApp;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxUpdateNameContainer;
        private System.Windows.Forms.Button UpdateContainer;
    }
}

