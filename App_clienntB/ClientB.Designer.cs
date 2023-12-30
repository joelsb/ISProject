namespace App_clienntB
{
    partial class ClientB
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
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.textBoxOutput = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.richTextBoxShowApp = new System.Windows.Forms.RichTextBox();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.richTextShowContainer = new System.Windows.Forms.RichTextBox();
            this.button9 = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.button10 = new System.Windows.Forms.Button();
            this.SubscribeTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblStatusA = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxNameContainer = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(19, 94);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(88, 25);
            this.button1.TabIndex = 0;
            this.button1.Text = "POSTNewApp";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.POSTNewApp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 70);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Name:";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(63, 68);
            this.textBoxName.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(164, 20);
            this.textBoxName.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 367);
            this.button2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(208, 29);
            this.button2.TabIndex = 3;
            this.button2.Text = "UPDATEApp";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.UPDATEApp);
            // 
            // textBoxOutput
            // 
            this.textBoxOutput.Location = new System.Drawing.Point(622, 351);
            this.textBoxOutput.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxOutput.Name = "textBoxOutput";
            this.textBoxOutput.Size = new System.Drawing.Size(149, 20);
            this.textBoxOutput.TabIndex = 4;
            this.textBoxOutput.Text = "textBoxOutput";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(241, 98);
            this.button3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(111, 24);
            this.button3.TabIndex = 5;
            this.button3.Text = "POSTNewContainer";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.POSTNewContainer);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(584, 423);
            this.button4.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(131, 30);
            this.button4.TabIndex = 6;
            this.button4.Text = "POSTNewSubcription";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.POSTNewSubcription);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(247, 367);
            this.button5.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(171, 30);
            this.button5.TabIndex = 7;
            this.button5.Text = "UPDATEContainer";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.UPDATEContainer);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(162, 93);
            this.button6.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(58, 28);
            this.button6.TabIndex = 8;
            this.button6.Text = "DELApp";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.DELApp);
            // 
            // richTextBoxShowApp
            // 
            this.richTextBoxShowApp.Location = new System.Drawing.Point(16, 126);
            this.richTextBoxShowApp.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.richTextBoxShowApp.Name = "richTextBoxShowApp";
            this.richTextBoxShowApp.Size = new System.Drawing.Size(210, 184);
            this.richTextBoxShowApp.TabIndex = 9;
            this.richTextBoxShowApp.Text = "richTextBoxShowApp";
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(19, 312);
            this.button7.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(208, 34);
            this.button7.TabIndex = 10;
            this.button7.Text = "GETAllApp";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.GETAllApp);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(380, 98);
            this.button8.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(79, 23);
            this.button8.TabIndex = 11;
            this.button8.Text = "DELContainer";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.DELContainer);
            // 
            // richTextShowContainer
            // 
            this.richTextShowContainer.Location = new System.Drawing.Point(241, 126);
            this.richTextShowContainer.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.richTextShowContainer.Name = "richTextShowContainer";
            this.richTextShowContainer.Size = new System.Drawing.Size(218, 184);
            this.richTextShowContainer.TabIndex = 12;
            this.richTextShowContainer.Text = "richTextShowContainer";
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(242, 314);
            this.button9.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(217, 32);
            this.button9.TabIndex = 13;
            this.button9.Text = "GETAllContainer";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.GETAllContainer);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(38, 244);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 13);
            this.lblStatus.TabIndex = 14;
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(622, 93);
            this.button10.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(61, 25);
            this.button10.TabIndex = 15;
            this.button10.Text = "Subscribe";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.Subscribe);
            // 
            // SubscribeTextBox
            // 
            this.SubscribeTextBox.Location = new System.Drawing.Point(463, 126);
            this.SubscribeTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.SubscribeTextBox.Multiline = true;
            this.SubscribeTextBox.Name = "SubscribeTextBox";
            this.SubscribeTextBox.Size = new System.Drawing.Size(222, 190);
            this.SubscribeTextBox.TabIndex = 16;
            this.SubscribeTextBox.Text = "Subscribe";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(269, 320);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 17);
            this.label2.TabIndex = 17;
            // 
            // lblStatusA
            // 
            this.lblStatusA.AutoSize = true;
            this.lblStatusA.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatusA.Location = new System.Drawing.Point(590, 321);
            this.lblStatusA.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblStatusA.Name = "lblStatusA";
            this.lblStatusA.Size = new System.Drawing.Size(106, 17);
            this.lblStatusA.TabIndex = 18;
            this.lblStatusA.Text = "Disconnected";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(244, 69);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 17);
            this.label3.TabIndex = 19;
            this.label3.Text = "Name:";
            // 
            // textBoxNameContainer
            // 
            this.textBoxNameContainer.Location = new System.Drawing.Point(296, 70);
            this.textBoxNameContainer.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxNameContainer.Name = "textBoxNameContainer";
            this.textBoxNameContainer.Size = new System.Drawing.Size(164, 20);
            this.textBoxNameContainer.TabIndex = 20;
            // 
            // ClientA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(827, 581);
            this.Controls.Add(this.textBoxNameContainer);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblStatusA);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.SubscribeTextBox);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.richTextShowContainer);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.richTextBoxShowApp);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.textBoxOutput);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "ClientA";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBoxOutput;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.RichTextBox richTextBoxShowApp;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.RichTextBox richTextShowContainer;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.TextBox SubscribeTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblStatusA;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxNameContainer;
    }
}

